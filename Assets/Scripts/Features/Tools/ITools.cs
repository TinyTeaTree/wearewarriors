using System.Threading.Tasks;
using Core;
using UnityEngine;


namespace Game
{
    public interface ITools : IFeature
    {
        Task LoadTools();

        ToolAction[] GetToolAbilities(TTools toolEnum);

        ToolVisual GetHoldingTool();

        ToolVisual GetClosestTool(Vector3 pos);

        Task DropTool(ToolVisual tool);
        
        void PickUpTool(ToolVisual tool, Transform avatarTransform);
        
        void HighlightOff();
        
        void HighlightOn(ToolVisual closestTool);
    }
}