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
    }
}