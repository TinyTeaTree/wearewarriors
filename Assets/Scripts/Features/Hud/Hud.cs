using System.Threading.Tasks;
using Core;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Game
{
    public class Hud : BaseVisualFeature<HudVisual>, IHud
    {
        [Inject] public ICamera Camera { get; set; }
        
        public UnityEngine.Camera HudCamera => _visual.Camera;
        public Transform NavBar => _visual.MainBorder;
        
        public async Task Load()
        {
            await CreateVisual();
            
            Camera.WorldCamera.GetUniversalAdditionalCameraData().cameraStack.Add(_visual.Camera);
        }

        public void SetHudOnCanvas(Canvas canvas)
        {
            canvas.transform.SetParent(_visual.transform);
            canvas.worldCamera = _visual.Camera;
        }

    }
}