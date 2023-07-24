using API.Models;

namespace API.DTOs.Accounts
{
    public class GetViewAccountDto
    {
        public Guid Guid { get; set; }
        public int Otp { get; set; }
        public string Password { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }

        public static implicit operator Account(GetViewAccountDto dto)
        {
            return new Account
            {
                Guid            = dto.Guid,
                Otp             = dto.Otp,
                Password        = dto.Password,
                IsUsed          = dto.IsUsed,
                ExpiredTime     = dto.ExpiredTime,
                ModifiedDate    = DateTime.Now
            };
        }
        public static explicit operator GetViewAccountDto(Account account)
        {
            return new GetViewAccountDto
            {
                Guid            = account.Guid,
                Otp             = account.Otp,
                Password        = account.Password,
                IsUsed          = account.IsUsed,
                ExpiredTime     = account.ExpiredTime
            };
        }
    }
}
