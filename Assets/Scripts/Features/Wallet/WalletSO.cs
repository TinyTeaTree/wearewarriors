using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class WalletSO : BaseConfigSO
    {
        [SerializeField] private WalletConfig _config;

        public override BaseConfig Config => _config;
    }
}