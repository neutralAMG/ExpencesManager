using Expences.Aplication.Core;


namespace Expences.Aplication.Dto.Category
{
    public record CategoryBaseDto : BaseDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
