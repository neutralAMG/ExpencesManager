using Expences.Domain.Core;
using System.ComponentModel.DataAnnotations.Schema;


namespace Expences.Domain.Entities
{
    public class Expences : BaseEntity
    {
        public Expences()
        {
            DateIntroduce = DateTime.Now;
        }
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime DateIntroduce { get; set; }
        public decimal Amount { get; set; }

        public int CategoryId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Users Users { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
