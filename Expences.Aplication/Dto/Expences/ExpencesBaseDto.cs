
using Expences.Aplication.Core;

namespace Expences.Aplication.Dto.Expences
{
    public record ExpencesBaseDto : BaseDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }


    }
}
