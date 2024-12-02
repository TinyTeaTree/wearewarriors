using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class WaletSO : BaseConfigSO
    {
        [SerializeField] private WaletConfig _config;

        public override BaseConfig Config => _config;
    }
}