# MeadowJSON
sample code for the Meadow Microcontroller to create a JSON object and submit to an API.  Includes the API server as a .net console app (that stores the response in a postgres database, the client code running on the Meadow to create a JSON object to send to the API, and the SQL for the database table

https://www.wildernesslabs.co/developers

The sql script creates a table in postgres for the API server to connect to.  

For the API server, change the DB connection string in appsettings.json to reflect your postgres database.  It's just a simple bit of code, but the Meadow didn't support TLS when I built this.  So I needed a http only API server to test against

For the Meadow, you'll need to configure the settings for your wireless network, and the location of the API server.  
In MeadowApp.cs, in the ConnectToWifi() class, set your wifi SSID and password
In the SendNotification() class, change the IP address in the uri string to the IP address of the API server

If you run the code in debug the Meadow sends it's status out to the console.  It takes between 30 and 45 seconds on my network for the device to connect.  Then you should see an IP address printed (assuming it worked)
It'll pause for a while whilst it sends the JSON but then you should see the entry in your DB table.  
