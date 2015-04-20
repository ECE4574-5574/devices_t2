using System;
using Owin; 
using System.Web.Http; 

namespace House
{
public class HouseStartup
{
	public void Configuration(IAppBuilder builder)
	{
		HttpConfiguration config = new HttpConfiguration();

		config.Routes.MapHttpRoute(name: "device",
			routeTemplate: "api/{controller}/{id}",
			defaults: new {id = RouteParameter.Optional});

		builder.UseWebApi(config);
	}
}
}

