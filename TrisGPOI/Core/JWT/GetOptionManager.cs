using Microsoft.Extensions.Configuration;
using TrisGPOI.Core.JWT.Entities;
using TrisGPOI.Core.JWT.Interfaces;

namespace TrisGPOI.Core.JWT
{
    public class GetOptionManager : IGetOptionManager
    {
        private readonly IConfiguration _configuration;
        public GetOptionManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenOptions? GetTokenOptions()
        {
            return _configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }
    }
}
