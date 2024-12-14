using System.Threading.Tasks;
using Core;

namespace Game
{
    public class Wallet : BaseVisualFeature<WalletVisual>, IWallet
    {
        [Inject] public WalletRecord Record { get; set; }
        [Inject] public IPlayerAccount PlayerAccount { get; set; }
        [Inject] public IHud Hud { get; set; }
        public async Task LoadWallet()
        {
            await CreateVisual(Hud.NavBar);
            _visual.UpdateCoinUI();
        }

        public async Task AddToWallet(int amount)
        {
            Record.Coins += amount;
            _visual.UpdateCoinUI();
            await PlayerAccount.SyncPlayerData();
        }

        public async Task Pay(int amount)
        {
            if (Record.Coins >= amount)
            {
                Record.Coins -= amount;
                _visual.UpdateCoinUI();
                await PlayerAccount.SyncPlayerData();
            }
        }

        public float CheckWalletBalance()
        {
            return Record.Coins;
        }
    }
}