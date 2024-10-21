using Core;
using UnityEngine;

namespace Game
{
    public class JoystickVisual : BaseVisual<Joystick>
    {
        [SerializeField] private GameObject _joystick;

        private Vector2 _joystickDirection;

        public Vector2 Direction => _joystickDirection;
        
        public void Show()
        {
            _joystick.SetActive(true);
        }

        public void Hide()
        {
            _joystick.SetActive(false);
        }

        public void ReportJoystick(Vector2 vector2)
        {
            _joystickDirection = vector2;
        }
    }
}