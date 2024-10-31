using System.Threading.Tasks;
using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class Avatar : BaseVisualFeature<AvatarVisual>, IAvatar
    {
        [Inject] public AvatarRecord Record { get; set; }
        [Inject] public IGarden Garden { get; set; }
        [Inject] public IJoystick Joystick { get; set; }
        [Inject] public ITools Tools { get; set; }
        
        public Transform HandTransform => _visual.RightHand;
        private AvatarConfig Config { get; set; }
        
        
        public async Task Load()
        {
            await CreateVisual();

            _visual.SetPos(Garden.AvatarStartSpot);

            Config = _bootstrap.Services.Get<ILocalConfigService>().GetConfig<AvatarConfig>();
        }

        public void Activate()
        {
            _visual.SetDirectionProvider(Joystick);
            
            _visual.StartMovement();
        }

       

        public void Update()
        {
            if (Tools.GetHoldingTool() != null)
                return;
            
            var closestTool = Tools.GetClosestTool(_visual.transform.position);
            if (closestTool != null)
            {
                float distance = Vector3.Distance(closestTool.transform.position, _visual.transform.position);
                if (distance > Config.HighlightDistance)
                {
                    Tools.HighlightOff();
                }
                else
                {
                    Tools.HighlightOn(closestTool);
                }

                if (distance < Config.PickupDistance)
                {
                    Tools.PickUpTool(closestTool, _visual.RightHand);
                }
            }
        }
    }
}