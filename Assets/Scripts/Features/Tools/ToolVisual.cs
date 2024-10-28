using Core;
using UnityEngine;

namespace Game
{
    public class ToolVisual : BaseVisual<Tools>
    {
        [SerializeField] GameObject _toolPrefab;
        [SerializeField] private Outline _outline;

        public void SetHighlight(bool state)
        {
            _outline.Toggle(state);
        }
    }
}