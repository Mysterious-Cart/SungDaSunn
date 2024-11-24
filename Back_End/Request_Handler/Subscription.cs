using System.Runtime.CompilerServices;
using Back_End.Model.Crust_db;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;


namespace Back_End.Request_Handler
{
    public class Subscription{

        [Subscribe(With = nameof(SubscribeToChannelAsync))]
        public Messages OnMessageReceive([EventMessage] Messages messages) => messages;
        public async IAsyncEnumerable<Messages> SubscribeToChannelAsync([Service]ITopicEventReceiver receiver,int GroupId) {
            var sourceStream = await receiver.SubscribeAsync<Messages>(GroupId.ToString());
            await foreach(Messages message in sourceStream.ReadEventsAsync()){
                yield return message;
            }
        }

    }   
}
