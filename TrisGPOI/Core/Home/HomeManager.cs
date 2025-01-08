using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Core.Home.Interfaces;
using TrisGPOI.Core.Reward.Interfaces;
using TrisGPOI.Core.User.Exceptions;
using TrisGPOI.Core.User.Interfaces;

namespace TrisGPOI.Core.Home
{
    public class HomeManager : IHomeManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IGameManager _gameManager;
        private readonly IUserRewardRepository _userRewardRepository;
        internal static List<Tuple<string, Timer>> userTimers = new List<Tuple<string, Timer>>();
        public HomeManager(IUserRepository userRepository, IGameManager gameManager, IUserRewardRepository userRewardRepository)
        {
            _userRepository = userRepository;
            _gameManager = gameManager;
            _userRewardRepository = userRewardRepository;
        }
        public async Task SetOnlineTemperaly(string email)
        {
            await _userRepository.AddUserStatusNumber(email);
            if (await _gameManager.SearchPlayerPlayingGameAsync(email) != null)
            {
                await _userRepository.ChangeUserStatus(email, "Online");
            }
            else
            {
                await _userRepository.ChangeUserStatus(email, "Playing");
            }
            TimeSpan time = TimeSpan.FromSeconds(10);
            Tuple<string, Timer> temp = new Tuple<string, Timer>(email, new Timer(OnTimerFinished, email, time, Timeout.InfiniteTimeSpan));
            userTimers.Add(temp);
            await UpdateRewardNumberAndUpdateLastLogin(email);
        }
        public async Task UpdateRewardNumberAndUpdateLastLogin(string email)
        {
            DateTime lastLogin = await _userRepository.GetLastLogin(email);
            DateTime utcNow = DateTime.UtcNow;
            DateTime targetTime = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, 8, 0, 0, DateTimeKind.Utc);

            if (lastLogin < targetTime)
            {
                await _userRewardRepository.ResetRewardRemain(email);
            }

        }
        public async void OnTimerFinished(object ob)
        {
            string email = (string)ob;
            if (await _userRepository.GetUserStatusNumber(email) >= 0)
            {
                await _userRepository.SubUserStatusNumber(email);
            }
            if (await _userRepository.GetUserStatusNumber(email) == 0)
            {
                await SetOffline(email);
            }
        }
        public async Task SetOffline(string email)
        {
            await _userRepository.ChangeUserStatus(email, "Offline");
            await _userRepository.ResetUserStatusNumber(email);
            await UpdateRewardNumberAndUpdateLastLogin(email);
            userTimers.RemoveAll(t => t.Item1 == email);
        }
        public async Task SetPlaying(string email)
        {
            await _userRepository.ChangeUserStatus(email, "Playing");
        }
        public async Task ChangeUserStatus(string email, string status)
        {
            string[] possibleList = new string[]
            {
                "Online",
                "Offline",
                "Playing"
            };
            bool passato = false;
            for (int i = 0; i < possibleList.Length; i++)
            {
                if (possibleList[i] == status)
                {
                    passato = true;
                }
            }

            if (!passato)
            {
                throw new MalformedDataException();
            }

            await _userRepository.ChangeUserStatus(email, status);
        }

        public async Task<string> GetUserStatus(string email)
        {
            var user = await _userRepository.FirstOrDefaultActiveUser(email);
            if (user == null)
            {
                throw new NotExisitingEmailException();
            }

            return user.Status;
        }
    }
}
