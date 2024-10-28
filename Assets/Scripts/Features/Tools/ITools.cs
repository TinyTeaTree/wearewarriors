using Core;
using UnityEngine;

namespace Game
{
    public interface ITools : IFeature
    {
        void LoadTools();

        void GetToolAbilities();

        ToolVisual GetHoldingTool();

        ToolVisual GetClosestTool(Vector3 pos);

        void DropTool();
        
        void PickUpTool();
        void HighlightOff();
        void HighlightOn(ToolVisual closestTool);
    }
}