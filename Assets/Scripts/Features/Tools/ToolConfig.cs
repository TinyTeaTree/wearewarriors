using UnityEngine;

namespace Game
{
    [CreateAssetMenu]
    [System.Serializable]
    public class ToolConfig : ScriptableObject
    {
        public ToolVisual prefab;
        public TTools ToolID;
        public ToolAction[] ToolAbilities;
    }
}