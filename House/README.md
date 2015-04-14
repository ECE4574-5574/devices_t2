# House Public API

The House App provides the following API for interfacing with the rest of the system.
This API is a RESTful API, with all communication happening via JSON blobs of the appropriate data.

## Current Status

Currently, none of this is implemented. This is merely design.

# Device API

## Getting Device State

Given a configuration, a device can be queried by sending a GET to the following address:
```
http://house_address:house_port/device/{houseid}/{roomid}/{deviceid}
```

which will respond with a JSON blob representing the entire state of the device, if it exists. Otherwise, it will return the appropriate error code.

## Setting Device State

Setting the device state can be done by sending a POST command to the same address as above.
The POST payload should be a JSON blob with the key-value pairs that are desired to be changed.
Specific key-value pairs are specified as the properties listed in api/Devices.cs