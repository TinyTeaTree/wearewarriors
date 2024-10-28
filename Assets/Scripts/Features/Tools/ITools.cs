using System.Threading.Tasks;
using Core;
using UnityEngine;


namespace Game
{
    public interface ITools : IFeature
    {
        Task LoadTools();

        ToolAction GetToolAbilities();

        ToolVisual GetHoldingTool();

        ToolVisual GetClosestTool(Vector3 pos);

        void DropTool(ToolVisual tool);
        
        void PickUpTool(ToolVisual tool);
        
        void HighlightOff();
        
        void HighlightOn(ToolVisual closestTool);
    }
}