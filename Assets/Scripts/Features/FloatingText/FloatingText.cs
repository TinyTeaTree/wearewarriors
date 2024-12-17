using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Game
{
    public class FloatingText : BaseVisualFeature<FloatingTextVisual>, IFloatingText
    {
        [Inject] public ICamera Camera { get; set; }
        
        public async Task Load()
        {
           await CreateVisual();
           _visual.TurnOffPrefab();
        }

        public void PopText(Vector3 position, string text, float duration)
        {
            _visual.PopText(position, text, duration);
        }
    }
}