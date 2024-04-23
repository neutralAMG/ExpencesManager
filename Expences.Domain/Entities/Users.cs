
using Expences.Domain.Core;

namespace Expences.Domain.Entities
{
    public class Users : BaseEntity
    {
        public Users()
        {
            DateCreated = DateTime.Now;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public DateTime DateCreated { get; set; }

        public int LimiteGasto { get; set; }

        public List<Expences> Expences { get; set; }

    }
}
