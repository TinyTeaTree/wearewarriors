using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class ToolsSO : BaseConfigSO
    {
        [SerializeField] private ToolsConfig _config;
        public override BaseConfig Config => _config;
     
    }
}