using System.Threading.Tasks;
using Core;

namespace Game
{
    public class Animals : BaseVisualFeature<AnimalsVisual>, IAnimals
    {
        [Inject] public IAvatar Avatar { get; set; }
        [Inject] public IWorld World { get; set; }
        [Inject] public ICoins Coins { get; set; }

        [Inject] public AnimalsRecord Record { get; set; }

        public async Task LoadSheep()
        {
            await CreateVisual();

            _visual.AvatarTransform = Avatar.AvatarTransform;
            
            foreach (var animalType in Record.Animals)
            {
                _visual.CreateAnimal(animalType, World.GetAnimalStartPoint());
            }
        }
    }
}