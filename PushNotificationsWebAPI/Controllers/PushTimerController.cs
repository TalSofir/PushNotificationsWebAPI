using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Http;
using Expo.Server.Client;
using Expo.Server.Models;
using PushNotificationsWebAPI.Classes;
using System.Web.Http.Cors;

namespace YourProjectNamespace.Controllers
{
    public class PushTimerController : ApiController
    {
        private Timer timer;
        private PushApiClient expoPushClient;
        private int counter;

        public PushTimerController()
        {
            expoPushClient = new PushApiClient();

            // Set up the timer
            timer = new Timer();
            timer.Interval = 5000; // 5 seconds interval
            timer.Elapsed += TimerElapsed;
            timer.AutoReset = true;
            counter = 0;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        [Route("api/pushnotification/sendTimer")]
        public IHttpActionResult SendPushNotificationTimer()
        {
            try
            {
                // Start the timer
                timer.Start();

                return Ok("Push notification timer started.");
            }
            catch (Exception)
            {
                // Log your exception here
                return InternalServerError();
            }
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            PushTicketRequest pushTicketRequest = new PushTicketRequest
            {
                PushTo = new List<string> { "ExponentPushToken[xBivkfP2OOsoTaDOhgxoKg]" }, // Replace with your recipients' Expo push tokens
                PushTitle = "Notification Title from server side seconds:"+counter.ToString(),
                PushBody = "Notification Body from server side",
            };

            // Send the push notification
            Task.Run(async () => await expoPushClient.PushSendAsync(pushTicketRequest));

            // Increment the counter
            counter++;

            // Check if the counter reaches 5, and stop the timer
            if (counter == 5)
            {
                timer.Stop();
            }
        }
    }
}
