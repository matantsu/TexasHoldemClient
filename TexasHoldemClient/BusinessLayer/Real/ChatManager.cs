using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemClient.BusinessLayer.Models;

namespace TexasHoldemClient.BusinessLayer.Real
{
    public class ChatManager : IChatManager
    {        
        public ObservableCollection<ChatMessage> Messages = new ObservableCollection<ChatMessage>();

        ObservableCollection<ChatMessage> IChatManager.Messages => throw new NotImplementedException();

        public PublisherClient publisher { get; private set; }
        private TopicName gamecenterTopic;
        public ChatManager()
        {
            publisher = PublisherClient.Create();
            gamecenterTopic = new TopicName(Config.ProjectID, "gamecenter");
            publisher.CreateTopic(gamecenterTopic);

            SubscriberClient subscriber = SubscriberClient.Create();
            SubscriptionName subscriptionName = new SubscriptionName(Config.ProjectID, "ahfffhhfdhd");
            subscriber.CreateSubscription(subscriptionName, gamecenterTopic, pushConfig: null, ackDeadlineSeconds: 60);

            listen(subscriber, gamecenterTopic, (m) => { });
        }

        public async void listen(SubscriberClient client, TopicName topicName, Action<ChatMessage> action)
        {
            SubscriptionName subscriptionName = new SubscriptionName(Config.ProjectID, "ahfffhhfdhd");
            client.CreateSubscription(subscriptionName, topicName, pushConfig: null, ackDeadlineSeconds: 60);

            PullResponse response = client.Pull(subscriptionName, returnImmediately: true, maxMessages: 10);
            action(null);
        }

        public async Task Send(ChatMessage message)
        {
            PubsubMessage pubsubMessage = new PubsubMessage
            {
                // The data is any arbitrary ByteString. Here, we're using text.
                Data = ByteString.CopyFromUtf8("Hello, Pubsub"),
                // The attributes provide metadata in a string-to-string dictionary.
                Attributes =
                {
                    { "description", "Simple text message" }
                }
            };
            publisher.Publish(gamecenterTopic, new[] { pubsubMessage });
        }

        public async Task Send(Game game, ChatMessage message)
        {
            
        }

        IDictionary<Game, IDisposable> subs = new Dictionary<Game,IDisposable>();
        

        public ObservableCollection<ChatMessage> Subscribe(Game game)
        {
            ObservableCollection<ChatMessage> messages = new ObservableCollection<ChatMessage>();
            return messages;
        }

        public void Dispose(Game game)
        {
            
        }
    }
}
