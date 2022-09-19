using System.ComponentModel.DataAnnotations;

namespace CardStorageService.Core.Entities.Base
{
    public class BaseEntity<T>
    {
        [Required]
        public T Id { get; set; }
    }
}