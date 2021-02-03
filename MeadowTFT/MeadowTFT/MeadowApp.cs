using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;
using Meadow.Foundation.Leds;
using Meadow.Gateway.WiFi;
using System.Text.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace MeadowTFT
{
    public class MeadowApp : App<F7Micro, MeadowApp>
    {

        public string Roberrors { get; set; }
        public MeadowApp()
        {
            Initialize();
            ConnectToWifi();
            SendNotification();
        }

        void Initialize()
        {
 
 
        }

        void ConnectToWifi()
        {
            Device.InitWiFiAdapter().Wait();


            if (Device.WiFiAdapter.Connect("<WIFI Network SSID>", "<Wifi Password>").ConnectionStatus != ConnectionStatus.Success)
            {
                Roberrors = "could not connect to network";
                throw new Exception("Cannot connect to network, application halted.");

            }

            IPAddress[] IPS = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress ip in IPS)
            {

                Console.WriteLine("IP address: " + ip);

            }

        }


        public async Task<bool> SendNotification()
        {
            Console.WriteLine("Start Notification: " );
            try
            {
                string uri = "http://192.168.1.181:8002/api/MeadowLogs";

                Console.WriteLine("Build object: ");
                var data = new
                {LogData = "Meadow message delivered" };
                
                Console.WriteLine("Serialize Data: ");
               string httpContent = JsonSerializer.Serialize(data);
                
                Console.WriteLine("Build httpcontent: ");
                 var stringContent = new StringContent(httpContent);
                
                Console.WriteLine("Create http client: ");
                var client = new HttpClient();

                Console.WriteLine("Adding Headers: ");
                stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                
                Console.WriteLine("Sending Message: ");
                var response = await client.PostAsync(uri, stringContent).ConfigureAwait(false);
                
                var result = response.Content.ReadAsStringAsync();
                 if (response.IsSuccessStatusCode)
                 {
                  
                    return true;
                 }
                 else
                 {
                   Console.WriteLine(response.ReasonPhrase);
              
                     return false;
                 }  

  }
            catch (TaskCanceledException ex)
            {

                Console.WriteLine(ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }



    }
}
