using Core;

namespace Game
{
    public class Field : BaseVisualFeature<FieldVisual>, IField
    {
        [Inject] public ILoadingScreen LoadingScreen { get; set; }

        public void Show()
        {
            LoadingScreen.Close();
        }
    }
}