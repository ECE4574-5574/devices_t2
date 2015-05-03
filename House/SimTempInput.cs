using System;
using api;
using Hats.SimWeather;
using System.Reflection;
using Hats.Time;

namespace House
{
public class SimTempInput : IDeviceInput, IDeviceOutput
{
	protected IWeather _weather;
	protected Temperature _current_temp;
	protected DateTime _last_time;
	protected bool _running;
	private double _thermal_rate;

	public SimTempInput(IWeather weather)
	{
		_weather = weather;
		_current_temp = new Temperature()
		{
			C = _weather.Temperature()
		};
		_last_time = DateTime.MinValue;
		_running = false;
		_thermal_rate = 1.0e-2;
	}

	public bool read(Device dev)
	{
		var thermo = (Thermostat)dev;
		if(thermo == null || _weather == null)
		{
			return false;
		}

		var now = dev.Frame.now();

		if(_last_time == DateTime.MinValue)
		{
			_last_time = dev.Frame.now();
			_current_temp.C = _weather.Temperature();
		}

		var time_diff = (now - _last_time).TotalSeconds;

		if(time_diff < 5e-3) //5 ms resolution on simulation for now
		{
			return true;
		}

		//hack hack hack to disable the IO temporarily.
		var input = dev.Input;
		var output = dev.Output;
		dev.resetIO();

		var val = typeof(Thermostat).GetProperty("Value");
		var temp = new Temperature();
		var outside_temp = _weather.Temperature(now);
		var plant_drive = UpdatePlant(thermo, _current_temp.C, outside_temp);
		var external_diff = outside_temp - _current_temp.C;
		_current_temp.C += (external_diff * _thermal_rate) * time_diff + plant_drive * time_diff;
		temp.C = _current_temp.C;

		val.SetValue(thermo, temp);
		_last_time = now;

		//hack hack hack to restore the IO
		dev.resetIO(input, output);
		return true;
	}

	public bool write(Device dev)
	{
		return true;
	}

	protected double UpdatePlant(Thermostat dev, double current, double outside_temp)
	{
		if(!dev.Enabled)
		{
			return 0.0;
		}
		var max_error = 1.0;
		if(_running)
		{
			max_error = 0.05; //once on, drive error low
		}

		var error = current - dev.SetPoint.C;

		var plant_drive = 0.0;
		if(Math.Abs(error) > max_error)
		{
			plant_drive = Math.Sign(error) * (outside_temp - current) * _thermal_rate * 2.0;
			_running = true;
		}
		else if(_running) //error is below, and running, shut off.
		{
			_running = false;
		}

		return plant_drive;
	}
}
}

