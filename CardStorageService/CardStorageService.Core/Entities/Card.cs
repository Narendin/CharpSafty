using CardStorageService.Core.Entities.Base;

namespace CardStorageService.Core.Entities
{
    public class Card : BaseEntity<Guid>
    {
        public string CardNo { get; set; }
        public string Name { get; set; }
        public string CVV2 { get; set; }
        public DateTime ExpDate { get; set; }
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}