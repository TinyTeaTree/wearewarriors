using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Game
{
    public class Joystick : BaseVisualFeature<JoystickVisual>, IJoystick
    {
        [Inject] public JoystickRecord Record { get; set; }
        [Inject] public IHud Hud { get; set; }
        [Inject] public ICamera Camera { get; set; }
        [Inject] public ITools Tools { get; set; }
        public bool IsAvailable => Record.IsShowing;
        public Vector2 Direction => Record.Direction;
        
        public async Task Load()
        {
            await CreateVisual();
            Hud.SetHudOnCanvas(_visual.Canvas);
            Hide();
        }

        public void Show()
        {
            _visual.Show();
            Record.IsShowing = true;
        }

        public void Hide()
        {
            _visual.Hide();
            Record.IsShowing = false;
        }

        public void MoveJoystick(Vector2 screenPos)
        {
            if (screenPos == Vector2.zero)
            {
                Record.Direction = Vector2.zero;
            }
            else
            {
                var viewPortPoint = Camera.WorldCamera.ScreenToViewportPoint(screenPos);
                var worldPoint = Hud.HudCamera.ViewportToWorldPoint(new Vector3(viewPortPoint.x, viewPortPoint.y, _visual.Canvas.planeDistance));

                var direction = _visual.MoveKnob(worldPoint);
                Record.Direction = direction;
            }
        }
        
        public void ToggleDropButton(bool state)
        {
            _visual.ToggleDropButton(state);
        }

        public void DropPressed()
        {
            Tools.DropTool(Tools.GetHoldingTool());
        }
    }
}