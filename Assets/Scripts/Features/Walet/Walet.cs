using Core;

namespace Game
{
    public class Walet : BaseVisualFeature<WaletVisual>, IWalet
    {
        [Inject] public WaletRecord Record { get; set; }
    }
}