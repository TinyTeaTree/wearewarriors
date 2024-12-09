using Core;

namespace Game
{
    public class Coins : BaseVisualFeature<CoinsVisual>, ICoins
    {
        [Inject] public CoinsRecord Record { get; set; }
    }
}