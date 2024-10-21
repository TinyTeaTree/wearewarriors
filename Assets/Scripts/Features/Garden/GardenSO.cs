using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class GardenSO : BaseConfigSO
    {
        [SerializeField] private GardenConfig _config;

        public override BaseConfig Config => _config;
    }
}