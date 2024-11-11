using Core;
using UnityEngine;

namespace Game
{
    //TODO: Add Test to make sure no-one changed this script
    public class BaseMarkVisual : MonoBehaviour, IAmDestructible
    {
        [SerializeField] private TMark _type;
        public TMark Type => _type;
        public string Id { get; set; }
        
        public Transform Anchor { get; private set; }

        public void SetAnchor(Transform anchor)
        {
            Anchor = anchor;
        }
        
        public virtual void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}