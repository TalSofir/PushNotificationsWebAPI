using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Expo.Server.Client;
using Expo.Server.Models;
using PushNotificationsWebAPI.Classes;
using System.Web.Http.Cors;
using System.Runtime.Remoting.Messaging;


namespace YourProjectNamespace.Controllers
{
    public class PushNotificationController : ApiController


    {
        [ EnableCors(origins: "*", headers: "*", methods: "*")]

        [HttpPost]
        [Route("api/pushnotification/send")]
        public async Task<IHttpActionResult> SendPushNotification()
        {
            try
            {
                PushTicketRequest pushTicketRequest = new PushTicketRequest
                {
                    PushTo = new List<string> { "ExponentPushToken[xBivkfP2OOsoTaDOhgxoKg]" }, // Replace with your recipients' Expo push tokens
                    PushTitle = "Notification Title from server side",
                    PushBody = "Notification Body from server side",
                };

                PushApiClient expoPushClient = new PushApiClient();
                await expoPushClient.PushSendAsync(pushTicketRequest);
                return Ok("Push notification sent successfully.");

                //PushTicketResponse pushTicketResponse = await Task.Run(() => expoPushClient.PushSendAsync(pushTicketRequest));

                //if (pushTicketResponse.PushTicketStatuses.Any(x => x.TicketStatus == "ok"))
                //{
                //    return Ok($"Push notification sent successfully. Ticket ID: {pushTicketResponse.PushTicketStatuses}");
                //}
                //else
                //{
                //    var errorMessage = pushTicketResponse.PushTicketErrors;
                //    return BadRequest($"Failed to send push notification. Error: {errorMessage}");
                //}
            }
            catch (Exception)
            {
                // Log your exception here
                return InternalServerError();
            }
        }


    }
}
