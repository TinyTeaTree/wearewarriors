using Core;
using UnityEngine;

namespace Game
{
    public class HudVisual : BaseVisual<Hud>
    {
        [SerializeField] private UnityEngine.Camera _camera;
        
        public UnityEngine.Camera Camera => _camera;
    }
}