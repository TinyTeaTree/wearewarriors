using System.Threading.Tasks;
using Core;
using UnityEngine;


namespace Game
{
    public interface ITools : IFeature
    {
        Task LoadTools();
        
        ToolVisual GetHoldingTool();

        ToolVisual GetClosestPickableTool(Vector3 pos);
        
        void PickUpTool(ToolVisual tool, Transform avatarTransform);
        
        void HighlightOff();
        
        void HighlightOn(ToolVisual closestTool);
        
        void AddGrainBag(TPlant getSeedType);
        void DestroyGrainBagTool(ToolVisual tool);
    }
}