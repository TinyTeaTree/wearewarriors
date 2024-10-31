using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Game
{
    public class ToolVisual : BaseVisual<Tools>
    {
        public ToolsEnum ToolID;
        
        [SerializeField] private Outline _outline;
        [SerializeField] private Material _outlineMat;
        [SerializeField] private MeshRenderer _renderer;
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
            if(_outline != null)
            {
                _outline.Toggle(state);
            }
            else if(_outlineMat != null)
            {
                var matList = new List<Material>(2);
                _renderer.GetSharedMaterials(matList);

                if (state)
                {
                    if (!matList.Contains(_outlineMat))
                    {
                        matList.Add(_outlineMat);
                        _renderer.SetMaterials(matList);
                    }
                }
                else
                {
                    matList.Remove(_outlineMat);
                    _renderer.SetMaterials(matList);
                }
            }
        }
        
        

    }
}