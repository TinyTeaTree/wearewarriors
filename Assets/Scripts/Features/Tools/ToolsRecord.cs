using System.Collections.Generic;
using Core;
using UnityEngine;


namespace Game
{
    public class ToolsRecord : BaseRecord
    {
        public ToolsEnum ToolHeld { get; set; } = ToolsEnum.Hands;
        public ToolAction[] ToolAction { get; set; }
        public Dictionary<ToolsEnum, Vector3> ToolsPositions { get; set; } = new();
        public Dictionary<ToolsEnum, int> ToolsUsagePercentage { get; set; } = new();
    }
}