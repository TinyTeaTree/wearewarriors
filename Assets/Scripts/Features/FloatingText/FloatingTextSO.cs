using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class FloatingTextSO : BaseConfigSO
    {
        [SerializeField] private FloatingTextConfig _config;

        public override BaseConfig Config => _config;
    }
}