using System.Threading.Tasks;
using Core;


namespace Game
{
    public interface ITools : IFeature
    {
        Task LoadTools();

        ToolAction GetToolAbilities();

        ToolVisual GetHoldingTool();

        ToolVisual GetClosestTool();

        void DropTool(ToolVisual tool);
        
        void PickUpTool(ToolVisual tool);
    }
}