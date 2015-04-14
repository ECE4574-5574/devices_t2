# HATS Device API

This repository contains the Device API for interfacing with devices as part of the HATS system. This library can be used from any part of the system to interface with devices.

# Building

In order to build this repository, it is best to use the solution present in the [Automation_System](https://github.com/ECE4574-5574/Automation_System) repository. This allows integrating the repository with the entire system through the class wide solution. This also helps ensure cross-platform building and compatibility.

# Handling Devices as a Group

One part of the Device API is the function calls which manage enumerating, registering, and requesting devices from some remote source. These functions are currently contained in api/DeviceAPI.cs, and handle these bulk operations. In these cases, instances of the Device class are used to represent Devices which are known to the system, and are the atomic unit of interacting with a particular device.

# Controlling a Device

Once a Device is instantiated using the above API, interfacing with devices is identical to interfacing with the Device object itself. Getting or setting the expected properties will handle communication transparently.

Note: If a setting is changed in a property, this is equivalent to attempting to execute that change on the device immediately. It is not guaranteed to succeed; the post condition for any set operation is that the communication was attempted. In order to check if a command succeeded, the property should be read back to verify it is in the expected state.

## Device Inheritance

Devices are passed around through the Device base class, but the particulars of the Device are handled through derived classes implementing specific interfaces. By casting an instance of Device to the relevant interfaces or derived classes, the logical capabilities of the device can be queried, and the relevant aspect of the API exposed. The API unit test shows some simple examples of this.
