using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ToolsVisual : BaseVisual<Tools>
    {
        [SerializeField] Button _dropToolsButton;
        
        private ToolVisual[] _toolVisuals;
        public ToolVisual[] AllTools => _toolVisuals;
    }
}