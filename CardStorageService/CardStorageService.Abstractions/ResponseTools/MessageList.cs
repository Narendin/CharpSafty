namespace CardStorageService.Abstractions.ResponseTools
{
    [Serializable]
    public class MessageList
    {
        private List<string> _messages;

        public MessageList()
        {
            _messages = new List<string>();
        }

        public bool HasMessages()
        {
            return _messages != null && _messages.Count > 0;
        }

        public override string ToString()
        {
            return string.Join(", ", _messages);
        }

        public IList<string> Messages => _messages;

        public MessageList Add(string error)
        {
            _messages.Insert(0, error);
            return this;
        }

        public MessageList AddRange(List<string> errors)
        {
            _messages.AddRange(errors);
            return this;
        }
    }
}