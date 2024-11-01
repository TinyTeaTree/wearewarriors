using System.Collections;
using Codice.CM.Common;
using Core;
using UnityEngine;

namespace Game
{
    public class ToolVisual : BaseVisual<Tools>
    {
        public ToolsEnum ToolID;
        
        [SerializeField] private Outline _outline;
        [SerializeField] private Rigidbody _rigidbody;
        
        public void ToggleRigidBody(bool state)
        {
            _rigidbody.isKinematic = !state;
        }

        public void DropToolPhysics(Transform dropPoint, float force)
        {
            _rigidbody.AddForce(dropPoint.forward + Vector3.up * force, ForceMode.Impulse);
        }
        
        public void SetHighlight(bool state)
        {
            _outline.Toggle(state);
        }


        public void GetPickedUp(Transform handTransform)
        {
            ToggleRigidBody(false);
            transform.SetParent(handTransform, true);

            StartCoroutine(PickedUpRoutine(handTransform));

            SetHighlight(false);
        }

        IEnumerator PickedUpRoutine(Transform handTransform)
        {
            float passed = 0f;
            while (passed < 0.5f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, passed / 0.5f);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, passed / 0.5f);
                passed += Time.deltaTime;
                yield return null;

                if (transform.parent != handTransform)
                {
                    yield break; //We are no longer under the hand, stop doing everything
                }
            }
            
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}