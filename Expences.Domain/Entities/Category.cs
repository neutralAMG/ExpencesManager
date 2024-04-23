

using Expences.Domain.Core;

namespace Expences.Domain.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
        }
        public int Id { get; set; }
        public string Description { get; set; } 
        public List<Expences> Expences { get; set; }
    }
}
