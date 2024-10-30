using System.Collections.Generic;
using Core;
using UnityEngine;


namespace Game
{
    public class ToolsRecord : BaseRecord
    {
        public ToolVisual EquippedToolVisual { get; set; } 
        public ToolAction[] ToolAction { get; set; }
        public List<ToolVisual> AllToolsInGarden { get; set; } = new();
        public List<Vector3> AllToolsPositions { get; set; } = new();
        public Dictionary<ToolsEnum, int> ToolsUsagePercentage { get; set; } = new();

        public List<ToolRecordData> GardenTools { get; set; } = new();
    }
}