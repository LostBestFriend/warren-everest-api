using System.ComponentModel.DataAnnotations;

namespace DomainModels.Models
{
    public abstract class BaseModel
    {
        [Key]
        public long Id { get; set; }
    }
}
