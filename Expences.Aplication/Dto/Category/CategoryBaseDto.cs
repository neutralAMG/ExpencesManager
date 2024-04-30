using Expences.Aplication.Core;


namespace Expences.Aplication.Dto.Category
{
    public record CategoryBaseDto : BaseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
