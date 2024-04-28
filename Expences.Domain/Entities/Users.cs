
using Expences.Domain.Core;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string UsuarioPassword { get; set; }

        public DateTime DateCreated { get; set; }

        public int LimiteDeGasto { get; set; }
        
        public List<Expences> Expences { get; set; }

    }
}
