using UnityEngine;
using System.Threading.Tasks;
using Core;

namespace Factories
{
    public class ResourceFactory : BaseFactory
    {
        private string _loadPath;

        public ResourceFactory(string path)
        {
            _loadPath = path;
        }
        
        public override Task<TypeVisual> Create<TypeVisual>(Transform parent = null)
        {
            var path = _loadPath.HasContent() ? _loadPath : typeof(TypeVisual).Name;
            var visual = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<TypeVisual>(path), parent);
            return Task.FromResult(visual);
        }
    }
}