using System.Threading.Tasks;
using UnityEngine;

namespace Services
{
    public class ResourceAssetPackProvider : BaseAssetPackProvider
    {
        private string _path;
        
        public ResourceAssetPackProvider(string path)
        {
            _path = path;
        }
        
        public override Task<TypeAssetPack> Load<TypeAssetPack>()
        {
            var result = Resources.Load<TypeAssetPack>(_path);
            return Task.FromResult(result);
        }
    }
}