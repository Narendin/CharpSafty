using CardStorageService.Core.Entities.Base;

namespace CardStorageService.Core.Entities
{
    public class Client : BaseEntity<Guid>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public virtual ICollection<Card> Cards { get; set; } = new HashSet<Card>();
    }
}