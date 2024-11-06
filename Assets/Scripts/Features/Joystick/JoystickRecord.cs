using Core;
using UnityEngine;

namespace Game
{
    public class JoystickRecord : BaseRecord
    {
        public bool IsShowing { get; set; }
        public Vector2 Direction { get; set; }
    }
}