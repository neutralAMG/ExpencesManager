

namespace Expences.Aplication.Models
{
    public class UsersGetModel
    {       
  
        public string Name { get; set; }

        public int Id { get; set; }
        public string UserName { get; set; }

        public int LimiteGasto { get; set; }

        public List<ExpencesGetModel> Expences { get; set; }
    }
}
