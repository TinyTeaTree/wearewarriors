using System.Collections.Generic;
using Core;

namespace Game
{
    public class AnimalsRecord : BaseRecord
    {
        public List<TAnimal> Animals { get; set; } = new();
    }
}