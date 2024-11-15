using System.Threading.Tasks;
using Core;

namespace Game
{
    public class Sheeps : BaseVisualFeature<SheepsVisual>, ISheeps
    {
        [Inject] public IAvatar Avatar { get; set; }

        public async Task LoadSheep()
        {
            await CreateVisual();

            _visual.AvatarTransform = Avatar.AvatarTransform;

            _visual.CreateSheep();
        }
    }
}