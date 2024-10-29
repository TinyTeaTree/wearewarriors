
using Core;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu]
    [System.Serializable]
    public class ToolConfig : ScriptableObject
    {
        public ToolVisual prefab;
        public ToolsEnum ToolID;
        public ToolAction[] ToolAbilities;
        [Space(10)]
        public Vector3 ToolPosition;
    }
}