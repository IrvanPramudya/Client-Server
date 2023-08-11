using API.DTOs.Accounts;
using API.Models;
using API.Utilities.Handlers;
using Client.Contracts;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories
{
    public class AccountRepository : TableRepository<Account, Guid>, IAccountRepository
    {
        private readonly HttpClient httpClient;
        private readonly string request;
        public AccountRepository(string request = "account/") : base(request)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7191/api/")
            };
            this.request = request;
        }
        public async Task<ResponseHandler<TokenDto>> Login(LoginAccountDto entity)
        {
            ResponseHandler<TokenDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "Login", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseHandler<TokenDto>>(apiResponse);
            }
            return entityVM;
        }
    }
}
