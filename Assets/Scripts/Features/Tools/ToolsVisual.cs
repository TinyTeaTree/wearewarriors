using System;
using System.Collections.Generic;
using Codice.CM.Common;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ToolsVisual : BaseVisual<Tools>
    {
       [SerializeField] Button _dropToolsButtonPrefab;

       private Button _dropButton;
       
        private List<ToolVisual> _toolVisuals;
        public List<ToolVisual> AllTools => _toolVisuals;
        
        public void SetToolVisuals(List<ToolVisual> toolVisuals)
        {
            _toolVisuals = toolVisuals;
        }
    }
}