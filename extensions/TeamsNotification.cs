using callRecords.Models;
using Microsoft.Bot.Connector;
using AdaptiveCards;
using AdaptiveCards.Templating;
using Microsoft.Bot.Connector.Teams.Models;
using Microsoft.Bot.Schema;

namespace callRecords.Extensions
{

public static class TeamsNotification
{
        [Obsolete]
        public static async Task SendAdaptiveCardWithTemplating(List<CallDetails> callDetails)
    {
        var templateJson = @"
                            {
                                ""type"": ""AdaptiveCard"",
                                ""version"": ""1.0"",
                                ""body"": [
                                    {
                                        ""type"": ""TextBlock"",
                                        ""text"": ""Plan Type: {{planTypeFriendlyName}}!""
                                    },
                                    {
                                        ""type"": ""TextBlock"",
                                        ""text"": ""Call Duration: {{callDurationTotal}} minutes""
                                    }
                                ]
                            }";
        // get te first element of the callDetails list
        // TODO: Change this Logic afterwards to see if templating supports passing in an array of objects
        var callDetails2 = callDetails[0];


        var cardJson = ReplaceTemplateData(templateJson, new { planTypeFriendlyName = callDetails2.planDetails.planTypeFriendlyName, callDurationTotal = callDetails2.callDurationTotal }); // Replace template data with actual values
        
        var card = new AdaptiveCards.AdaptiveCard();
        //card = AdaptiveCards.AdaptiveCard.FromJson(cardJson).Card;
        card = AdaptiveCard.FromJson(cardJson).Card;
        
        var attachment = new Attachment
        {
            ContentType = AdaptiveCard.ContentType,
            Content = card,
        };
        
        var client = new ConnectorClient(new Uri("https://microsoft.webhook.office.com/webhookb2/c86d09ef-012c-4eef-9acb-ddf8d9e473a5@72f988bf-86f1-41af-91ab-2d7cd011db47/IncomingWebhook/bad0c715e9b94e38a9ae307b0ae957e6/8c1f8e92-6d3d-49d5-8c5d-fc789b6a2375"));
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


    }

    private static string ReplaceTemplateData(string templateJson, object data)
    {
        var template = new AdaptiveCardTemplate(templateJson);
        return template.Expand(data);
    }
}

}