using API.Models;
using API.Utilities.Handlers;

namespace API.DTOs.Accounts
{
    public class InsertAccountDto
    {
        public Guid Guid { get; set; }
        public int Otp { get; set; }
        public string Password { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }

        public static implicit operator Account(InsertAccountDto dto)
        {
            return new Account
            {
                Guid            = dto.Guid,
                Otp             = dto.Otp,
                Password        = HashingHandler.GenerateHash(dto.Password),
                IsUsed          = dto.IsUsed,
                ExpiredTime     = dto.ExpiredTime,
                CreatedDate     = DateTime.Now,
                ModifiedDate    = DateTime.Now,
            };
        }
        public static explicit operator InsertAccountDto(Account account)
        {
            return new InsertAccountDto
            {
                Guid        = account.Guid,
                Otp         = account.Otp,
                Password    = account.Password,
                IsUsed      = account.IsUsed,
                ExpiredTime = account.ExpiredTime,
            };
        }
    }
}
