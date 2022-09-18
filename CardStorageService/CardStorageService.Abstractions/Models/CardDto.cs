namespace CardStorageService.Abstractions.Models
{
    public class CardDto
    {
        public Guid Id { get; set; }
        public string CardNo { get; set; }
        public string Name { get; set; }
        public Guid ClientId { get; set; }
        public DateTime ExpDate { get; set; }
    }
}