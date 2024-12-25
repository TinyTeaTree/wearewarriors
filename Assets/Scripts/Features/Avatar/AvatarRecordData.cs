using Newtonsoft.Json;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class AvatarRecordData
    {
        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 Pos { get; set; }

        public float Rot { get; set; }
    }
}