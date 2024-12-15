using System.Collections;
using Core;
using UnityEngine;

namespace Game
{
    public class ToolVisual : BaseVisual<Tools>
    {
        public TTools ToolID;
        public TPlant SeedType;
        
        [SerializeField] private Outline _outline;
        [SerializeField] private Rigidbody _rigidbody;
        
        [SerializeField] private float workPerSecond;
        public float WorkPerSecond => workPerSecond;
        public bool Pickable => Feature.GetHoldingTool() != this && Time.time > pickableAt;
        public bool Droppable { get; set; } = true;

        private float pickableAt;

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

        public virtual void StartWorking()
        {
            Droppable = false;
        }

        public virtual void EndWorking()
        {
            Droppable = true;
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
        
        public void StartPickupCooldown(float cooldown)
        {
            pickableAt = Time.time + cooldown;
        }

        public void ThrowToolPhysics(Vector3 dropPoint, int force)
        {
            dropPoint.y = 15;
            Vector3 throwDirection = dropPoint - Feature.Avatar.AvatarTransform.position;
            _rigidbody.AddForce(throwDirection.normalized * force, ForceMode.Impulse);
        }
    }
}