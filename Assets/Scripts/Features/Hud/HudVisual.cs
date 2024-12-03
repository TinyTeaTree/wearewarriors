using Core;
using UnityEngine;

namespace Game
{
    public class HudVisual : BaseVisual<Hud>
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private Transform mainBorder;
        
        public UnityEngine.Camera Camera => _camera;
        public Transform MainBorder => mainBorder;
    }
}