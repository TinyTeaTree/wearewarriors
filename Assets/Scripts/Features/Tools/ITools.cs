using Core;

namespace Game
{
    public interface ITools : IFeature
    {
        void LoadTools();

        void GetToolAbilities();

        void GetHoldingTool();

        void GetClosestTool();

        void DropTool();
        
        void PickUpTool();
    }
}