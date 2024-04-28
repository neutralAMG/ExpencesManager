using Expences.Aplication.Core;


namespace Expences.Aplication.Dto.Users
{
    public record UsersBaseDto : BaseDto
    {
        public int LimiteGasto { get; set; }
    }
}
