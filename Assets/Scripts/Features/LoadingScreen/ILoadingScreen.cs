using Core;

namespace Game
{
    public interface ILoadingScreen : IFeature
    {
        void Close();
        void Show(bool toggleTips);
        void ProgressControl(float progress);
    }
}