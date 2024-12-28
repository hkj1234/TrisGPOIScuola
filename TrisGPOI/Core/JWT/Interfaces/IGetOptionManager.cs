using TrisGPOI.Core.JWT.Entities;

namespace TrisGPOI.Core.JWT.Interfaces
{
    public interface IGetOptionManager
    {
        public TokenOptions? GetTokenOptions();
    }
}
