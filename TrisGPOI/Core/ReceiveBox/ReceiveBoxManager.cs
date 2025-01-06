using TrisGPOI.Core.ReceiveBox.Entities;
using TrisGPOI.Core.ReceiveBox.Exceptions;
using TrisGPOI.Core.ReceiveBox.Interfaces;
using TrisGPOI.Core.User.Exceptions;
using TrisGPOI.Core.User.Interfaces;

namespace TrisGPOI.Core.ReceiveBox
{
    public class ReceiveBoxManager : IReceiveBoxManager
    {
        private readonly IReceiveBoxRepository _receiveBoxRepository;
        private readonly IUserRepository _userRepository;
        public ReceiveBoxManager(IReceiveBoxRepository receiveBoxRepository, IUserRepository userRepository)
        {
            _receiveBoxRepository = receiveBoxRepository;
            _userRepository = userRepository;
        }
        public async Task<List<DBReceiveBox>> GetReceiveBox(string email)
        {
            return await _receiveBoxRepository.GetReceiveBox(email);
        }
        public async Task<bool> ExistUnreadMailBox(string email)
        {
            return await _receiveBoxRepository.ExistUnreadMailBox(email);
        }
        public async Task SendReceiveBox(string sender, string receiver, string title, string message)
        {
            if (! await _userRepository.ExistUser(receiver))
            {
                throw new NotExisitingEmailException();
            }
            await _receiveBoxRepository.SendReceiveBox(sender, receiver, title, message);
        }
        public async Task DeleteReceiveBox(int Id)
        {
            if (!await _receiveBoxRepository.ExistReceiveBox(Id))
            {
                throw new NotExistingReceiveBoxException();
            }
            await _receiveBoxRepository.DeleteReceiveBox(Id);
        }
        public async Task ReadReceiveBox(int Id)
        {
            if (!await _receiveBoxRepository.ExistReceiveBox(Id))
            {
                throw new NotExistingReceiveBoxException();
            }
            await _receiveBoxRepository.ReadReceiveBox(Id);
        }
        public async Task<bool> ExistReceiveBox(int Id)
        {
            return await _receiveBoxRepository.ExistReceiveBox(Id);
        }
    }
}
