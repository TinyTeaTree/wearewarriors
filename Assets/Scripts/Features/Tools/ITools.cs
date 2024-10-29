using System.Threading.Tasks;
using Core;
using UnityEngine;


namespace Game
{
    public interface ITools : IFeature
    {
        Task LoadTools();

        ToolAction[] GetToolAbilities(ToolsEnum tool);

        ToolVisual GetHoldingTool();

        ToolVisual GetClosestTool(Vector3 pos);

        void DropTool(ToolVisual tool);
        
        void PickUpTool(ToolVisual tool, Transform avatarTransform);
        
        void HighlightOff();
        
        void HighlightOn(ToolVisual closestTool);
    }
}