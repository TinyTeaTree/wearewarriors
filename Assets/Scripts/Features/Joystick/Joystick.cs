using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Game
{
    public class Joystick : BaseVisualFeature<JoystickVisual>, IJoystick
    {
        [Inject] public JoystickRecord Record { get; set; }
        
        public bool IsAvailable => Record.IsShowing;
        public Vector2 Direction => _visual.Direction;
        
        public async Task Load()
        {
            await CreateVisual();
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

    }
}