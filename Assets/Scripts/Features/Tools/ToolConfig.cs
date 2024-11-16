
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [CreateAssetMenu]
    [System.Serializable]
    public class ToolConfig : ScriptableObject
    {
        public ToolVisual prefab;
        public TTools ToolID;
        public ToolAction[] ToolAbilities;
        [Space(10)]
        public Vector3 ToolPosition;
    }
}