using System.Threading.Tasks;
using Core;

namespace Game
{
    public interface IJoystick : IFeature, IProvideDirection
    {
        Task Load();

        void Show();
        void Hide();
        
        bool IsAvailable { get; }

        void ToggleDropButton(bool state);
    }
}