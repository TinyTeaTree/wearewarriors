using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class CameraSO : BaseConfigSO
    {
        [SerializeField] private CameraConfig _config;

        public override BaseConfig Config => _config;
    }
}