using System.Collections.Generic;
using Core;
using Unity.Plastic.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ToolsVisual : BaseVisual<Tools>
    {
        [SerializeField] Button _dropToolsButton;
        
        private List<ToolVisual> _toolVisuals;
        public List<ToolVisual> AllTools => _toolVisuals;

        public void SetToolVisuals(List<ToolVisual> toolVisuals)
        {
            _toolVisuals = toolVisuals;
        }
    }
}