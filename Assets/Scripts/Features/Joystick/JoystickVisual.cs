using System;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class JoystickVisual : BaseVisual<Joystick>
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private JoystickInputCapture _joystick;
        [SerializeField] private Button _dropToolButton;

        public Canvas Canvas => _canvas;

        private void Start()
        {
            _dropToolButton.onClick.AddListener(Feature.DropPressed);
        }

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

        public void ToggleDropButton(bool state)
        {
            _dropToolButton.gameObject.SetActive(state);
        }
    }
}