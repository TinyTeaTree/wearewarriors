
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
        public Vector3 ToolPosition;
    }
}