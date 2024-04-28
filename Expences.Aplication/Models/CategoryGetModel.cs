

namespace Expences.Aplication.Models
{
    public class CategoryGetModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public List<ExpencesGetModel>? Expences { get; set; }
    }
}
