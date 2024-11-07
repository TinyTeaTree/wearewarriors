using Core;

namespace Game
{
    public class Wallet : BaseVisualFeature<WalletVisual>, IWallet
    {
        [Inject] public WalletRecord Record { get; set; }
    }
}