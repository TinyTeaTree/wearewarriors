using System;
using System.Threading.Tasks;
using Core;

namespace Game
{
    public interface IGarden : IFeature, IDisposable
    {
        Task Load();
    }
}