using System.ComponentModel.DataAnnotations;

namespace DomainModels.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
