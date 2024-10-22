using System.Threading.Tasks;
using Core;

namespace Game
{
    public class Avatar : BaseVisualFeature<AvatarVisual>, IAvatar
    {
        [Inject] public AvatarRecord Record { get; set; }
        [Inject] public IGarden Garden { get; set; }
        [Inject] public IJoystick Joystick { get; set; }
        
        
        public async Task Load()
        {
            await CreateVisual();

            _visual.SetPos(Garden.AvatarStartSpot);


        }

        public void Activate()
        {
            _visual.SetDirectionProvider(Joystick);
            
            _visual.StartMovement();
        }
    }
}