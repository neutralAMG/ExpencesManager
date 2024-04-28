
namespace Expences.Aplication.Dto.Expences
{
    public record ExpencesSaveDto : ExpencesBaseDto
    {
        public DateTime DateIntroduce { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
