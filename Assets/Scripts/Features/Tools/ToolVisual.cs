using Codice.CM.Common;
using Core;
using UnityEngine;

namespace Game
{
    public class ToolVisual : BaseVisual<Tools>
    {
        [SerializeField] private Outline _outline;
        [SerializeField] private Rigidbody _rigidbody;
        
        public void ToggleRigidBody(bool state)
        {
            _rigidbody.isKinematic = state;
        }

        public void DropToolPhysics(Transform dropPoint, float force)
        {
            _rigidbody.AddForce(dropPoint.forward + Vector3.up * force, ForceMode.Impulse);
        }
        
        public void SetHighlight(bool state)
        {
            _outline.Toggle(state);
        }
        
        

    }
}