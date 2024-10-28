using Core;
using UnityEngine;

namespace Game
{
    public class ToolVisual : BaseVisual<Tools>
    {
        private Vector3 _toolPosition = Vector3.zero;

        public void CreateVisual()
        {
            Instantiate(gameObject, _toolPosition, Quaternion.identity);
        }
    }
}