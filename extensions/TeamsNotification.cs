using callRecords.Models;
using AdaptiveCards;
using AdaptiveCards.Templating;
using Microsoft.Bot.Connector.Teams.Models;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System.Text;

namespace callRecords.Extensions
{

    public static class TeamsNotification
{
        public static async Task SendAdaptiveCardWithTemplating(List<CallDetails> callDetails)
        {

            var templateJson = File.ReadAllText(".\\extensions\\TeamsNotificationCard.json");
            var template = new AdaptiveCardTemplate(templateJson);
            var cardJson = template.Expand(new { callDetails = callDetails });
            var card = AdaptiveCards.AdaptiveCard.FromJson(cardJson).Card;
            
            var attachment = new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card,
            };
            
            var message = new Activity()
            {
                Type = ActivityTypes.Message,
                Attachments = new List<Attachment>() { attachment },
                ChannelData = new TeamsChannelData()
                {
                    Notification = new NotificationInfo()
                    {
                        Alert = true
                    }
                }
            };
        
            var json = JsonConvert.SerializeObject(message);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
            using (var HttpClient = new HttpClient())
            {
                var response = await HttpClient.PostAsync("https://microsoft.webhook.office.com/webhookb2/c86d09ef-012c-4eef-9acb-ddf8d9e473a5@72f988bf-86f1-41af-91ab-2d7cd011db47/IncomingWebhook/ad4cededac094484a53689e50e5b2837/8c1f8e92-6d3d-49d5-8c5d-fc789b6a2375", content).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to send message to Teams. Status code: {response.StatusCode}");
                } 
            }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send message to Teams. Exception: {ex.Message}");
            }

   
        }

    }

}