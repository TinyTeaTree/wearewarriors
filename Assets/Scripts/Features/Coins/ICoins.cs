using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Game
{
    public interface ICoins : IFeature
    {
        void SpawnCoin(Vector3 position);
        
        
        /// <summary>
        /// The Total Weight is how much actuall Coins are spawned.
        ///
        /// Each Coin has a different Worth, we will partition the coins in such a way that it will sum up to the totalWeight
        /// </summary>
        void SpawnCoins(int totalWeight, Vector3 from);
        
        /// <summary>
        /// Another option is to spawn coins by custom volumes. Coin volumes is a number in range [1, 5] inclusive
        /// </summary>
        /// <param name="volumes"></param>
        /// <param name="position"></param>
        Task SpawnCoins(List<int> volumes, Vector3 from);
    }
}