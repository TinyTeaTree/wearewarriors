using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MarkPlantProgress : BaseMarkVisual
    {
        [SerializeField] Image _markSlider;
        
        public void UpdateMarkProgress(float progress)
        {
            _markSlider.fillAmount = progress;
        }
    }
}