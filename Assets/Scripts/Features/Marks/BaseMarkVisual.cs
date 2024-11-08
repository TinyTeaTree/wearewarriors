using UnityEngine;

namespace Game
{
    public class BaseMarkVisual : MonoBehaviour
    {
        [SerializeField] private TMark _type;
        public TMark Type => _type;
        public string Id { get; set; }
        
        public Transform Anchor { get; private set; }

        public void SetAnchor(Transform anchor)
        {
            Anchor = anchor;
        }
        
        //TODO: Add Test to make sure no-one changed this script
    }
}