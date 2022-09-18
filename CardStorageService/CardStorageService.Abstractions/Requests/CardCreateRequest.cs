namespace CardStorageService.Abstractions.Requests
{
    public class CardCreateRequest
    {
        public string CardNo { get; set; }
        public string Name { get; set; }
        public string CVV2 { get; set; }
        public DateTime ExpDate { get; set; }
        public Guid ClientId { get; set; }
    }
}