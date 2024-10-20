using Core;

namespace Game
{
    public interface ILoadingScreen : IFeature
    {
        void Close();
        void Show();
    }
}