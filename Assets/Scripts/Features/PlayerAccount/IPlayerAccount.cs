using System.Threading.Tasks;
using Core;

namespace Game
{
    public interface IPlayerAccount : IFeature
    {
        bool IsLoggedIn { get; }
        string PlayerId { get; }
        
        Task Login();
        Task Logout();
        
        //Task LinkCredentials(); 

        Task CreateNewPlayer();
        Task SyncPlayerData();
    }
}