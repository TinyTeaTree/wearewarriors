using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class ShopSO : BaseConfigSO
    {
        [SerializeField] private ShopConfig _config;

        public override BaseConfig Config => _config;
    }
}