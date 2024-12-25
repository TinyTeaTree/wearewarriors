using Newtonsoft.Json;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class ToolRecordData
    {
        public TTools Id { get; set; }
        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 Pos { get; set; }

        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 Rot { get; set; }
    }
}