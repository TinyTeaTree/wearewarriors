using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class AvatarSO : BaseConfigSO
    {
        [SerializeField] private AvatarConfig _config;

        public override BaseConfig Config => _config;
    }
}