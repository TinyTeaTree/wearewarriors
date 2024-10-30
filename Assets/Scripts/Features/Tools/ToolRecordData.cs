using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class ToolRecordData
    {
        public ToolsEnum Id { get; set; }
        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 Pos { get; set; }
    }
}