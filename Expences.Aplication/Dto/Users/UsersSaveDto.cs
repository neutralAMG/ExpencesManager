

namespace Expences.Aplication.Dto.Users
{
    public record UsersSaveDto : UsersBaseDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
