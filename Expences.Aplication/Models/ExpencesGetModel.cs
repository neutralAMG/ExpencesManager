
using Expences.Domain.Entities;

namespace Expences.Aplication.Models
{
    public class ExpencesGetModel
    {        
        public string Name { get; set; }

        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime DateIntroduce { get; set; }
        public decimal Amount { get; set; }

        public int CategoryId { get; set; }
        public int UserId { get; set; }

        public CategoryGetModel Category { get; set; }
    }
}
