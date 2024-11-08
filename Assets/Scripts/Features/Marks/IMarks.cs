using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Game
{
    public interface IMarks : IFeature
    {
        Task Load();

        /// <summary>
        /// Create a Mark and Return the Id. You should cache this Id for later references if you want to do stuff with your Mark
        /// </summary>
        string AddMark(Transform anchor, TMark type, string id = null);
        
        T GetMark<T>(string id)
            where T : BaseMarkVisual;
    }
}