using Core;
using UnityEngine;

namespace Game
{
    public class JoystickVisual : BaseVisual<Joystick>
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private JoystickInputCapture _joystick;

        public Canvas Canvas => _canvas;

        public void Show()
        {
            _joystick.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _joystick.gameObject.SetActive(false);
        }

        public void ReportJoystick(Vector2 mousePos)
        {
            Feature.MoveJoystick(mousePos);
        }

        public Vector2 MoveKnob(Vector3 worldPoint)
        {
            return _joystick.MoveKnob(worldPoint);
        }
    }
}