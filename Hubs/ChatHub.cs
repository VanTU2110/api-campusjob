using Microsoft.AspNetCore.SignalR;

namespace apicampusjob.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessageToConversation(string ConversationUuid, string senderUuid, string content)
        {
            await Clients.Group(ConversationUuid.ToString()).SendAsync("ReceiveMessage", new
            {
                conversationUuid = ConversationUuid,
                senderUuid = senderUuid,
                Content = content,
                SentAt = DateTime.UtcNow
            });
        }

        public async Task JoinConversation(string ConversationUuid)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, ConversationUuid.ToString());
        }

        public async Task LeaveConversation(string ConversationUuid)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, ConversationUuid.ToString());
        }
    }
}
