namespace Listening.Main.WebAPI.Controllers.AI.ViewModel
{
    public class MessageModel
    {
        public class ChatRequestBody
        {
            public List<Message> Messages { get; set; } = new List<Message>();
            public ExtraParameters ExtraParameters { get; set; } = new ExtraParameters();
        }

        public class Message
        {
            public string Role { get; set; }
            public string Content { get; set; }
        }

        public class ExtraParameters
        {
            public bool UseKeyword { get; set; }
            public bool UseReference { get; set; }
        }
    }
}
