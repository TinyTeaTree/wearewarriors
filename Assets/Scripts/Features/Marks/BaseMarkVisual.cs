using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    //TODO: Add Test to make sure no-one changed this script
    public class BaseMarkVisual : MonoBehaviour, IAmDestructible
    {
        [SerializeField] private TMark _type;
        [SerializeField] private Image _markImage;
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

        public void UpdateMarkProgress(float progress)
        {
            _markImage.fillAmount = progress;
        }
    }
}