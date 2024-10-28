using Core;
using UnityEngine;

namespace Game
{
    public class ToolVisual : BaseVisual<Tools>
    {
        [SerializeField] private Outline _outline;

        public void SetHighlight(bool state)
        {
            _outline.Toggle(state);
        }

        private Vector3 toolPosition;

    }
}