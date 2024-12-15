using System.Collections.Generic;
using Core;
using Unity.Plastic.Newtonsoft.Json;

namespace Game
{
    public class AvatarRecord : BaseRecord
    {
        [JsonIgnore]
        public bool IsWorking { get; set; }
        public AvatarRecordData AvatarRecordData { get; set; } = new();
        
        [JsonIgnore]
        public TTools ToolWorking { get; set; }
        [JsonIgnore]
        public float WorkTime { get; set; }
        
        [JsonIgnore]
        public bool IsMoving { get; set; }

        public List<TPlant> PlantsGathered { get; set; } = new();
        public float GatherProgress = 0f;

    }
}