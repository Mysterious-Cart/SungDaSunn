using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using Back_End.Model.Crust_db;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;


namespace Back_End.Request_Handler
{
    public class Subscription{

        [GraphQLDescription("Listen to a group given by the Id. Waiting for new messages.")]
        [Subscribe(With = nameof(SubscribeToChannelAsync))]
        public Messages OnMessageReceive([EventMessage] Messages messages) => messages;

        //Handling receiving delay text, by looping through the received text one by one from last to latest text that the user missed.
        //Also handling Subsciption to a specific topic.
        private async IAsyncEnumerable<Messages> SubscribeToChannelAsync([Service]ITopicEventReceiver receiver,ulong GroupId) {
            var sourceStream = await receiver.SubscribeAsync<Messages>(GroupId.ToString());
            await foreach(Messages message in sourceStream.ReadEventsAsync()){
                yield return message;
            }
        }

        [GraphQLDescription("Listen for new friend request for the given userId.")]
        [Subscribe(With =nameof(SubscibeToFriendRequestEvent))]
        public FriendRequest OnFriendRequestReceive([EventMessage] FriendRequest friendRequest) => friendRequest;
        public async IAsyncEnumerable<FriendRequest> SubscibeToFriendRequestEvent([Service] ITopicEventReceiver receiver, ulong UserId){
            var sourceStream = await receiver.SubscribeAsync<FriendRequest>(UserId.ToString());
            await foreach(FriendRequest friendRequest in sourceStream.ReadEventsAsync()){
                yield return friendRequest;
            }
        }

        [GraphQLDescription("Listen for any account that logged into the given session")]
        [Subscribe(With =nameof(SubscibeToLoggedIn))]
        public User OnLoginReceive([EventMessage] User User) => User;
        public async IAsyncEnumerable<User> SubscibeToLoggedIn([Service] ITopicEventReceiver receiver, ulong SessionId){
            var sourceStream = await receiver.SubscribeAsync<User>(SessionId.ToString());
            await foreach(User user in sourceStream.ReadEventsAsync()){
                yield return user;
            }
        }
        
        [Subscribe(With =nameof(SubscribeToRequestComfirm))]
        public ComfirmedFriend_Result OnFriendRequestComfirm([EventMessage] ComfirmedFriend_Result result) => result;
        public async IAsyncEnumerable<ComfirmedFriend_Result> SubscribeToRequestComfirm([Service] ITopicEventReceiver receiver, ulong UserId){
            var sourceStream = await receiver.SubscribeAsync<ComfirmedFriend_Result>(UserId.ToString());
            await foreach(ComfirmedFriend_Result user in sourceStream.ReadEventsAsync()){
                yield return user;
            }
        }

    }   
}
