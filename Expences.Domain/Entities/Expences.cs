using Expences.Domain.Core;


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

        public int LimiteGasto { get; set; }
        public int CategoryId { get; set; }

        public Users Users { get; set; }
        public Category Category { get; set; }
    }
}
