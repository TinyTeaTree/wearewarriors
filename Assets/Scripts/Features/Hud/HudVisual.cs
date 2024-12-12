using Core;
using UnityEngine;

namespace Game
{
    public class HudVisual : BaseVisual<Hud>
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private Transform mainBorder;
        [SerializeField] private Transform canvas;
        
        public UnityEngine.Camera Camera => _camera;
        public Transform MainBorder => mainBorder;
        public Transform Canvas => canvas;
    }
}