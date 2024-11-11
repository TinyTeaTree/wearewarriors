using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class MarksSO : BaseConfigSO
    {
        [SerializeField] private MarksConfig _config;

        public override BaseConfig Config => _config;
    }
}