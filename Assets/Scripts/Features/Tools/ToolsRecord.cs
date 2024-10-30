using System.Collections.Generic;
using Core;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;


namespace Game
{
    public class ToolsRecord : BaseRecord
    {
        [JsonIgnore]
        public ToolVisual EquippedToolVisual { get; set; }
        [JsonIgnore]
        public ToolAction[] ToolAction { get; set; }
        [JsonIgnore]
        public List<ToolVisual> AllToolsInGarden { get; set; } = new();
        [JsonIgnore]
        public Dictionary<ToolsEnum, int> ToolsUsagePercentage { get; set; } = new();

        public List<ToolRecordData> GardenTools { get; set; } = new();
    }
}