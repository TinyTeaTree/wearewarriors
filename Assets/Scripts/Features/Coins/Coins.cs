using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agents;
using Core;
using UnityEngine;

namespace Game
{
    public class Coins : BaseVisualFeature<CoinsVisual>, ICoins, IAppLaunchAgent
    {
        [Inject] public CoinsRecord Record { get; set; }
        [Inject] public IAvatar Avatar { get; set; }
        [Inject] public IWallet Wallet { get; set; }
        
        public readonly List<int> VolumeAmounts = new() { 1, 3, 5, 10, 25 };

        public async Task AppLaunch()
        {
            await CreateVisual();
        }

        public void SpawnCoin(Vector3 position)
        {
            
        }

        public void SpawnCoins(int totalWeight, Vector3 from)
        {
            var volumes = GetPartitions(totalWeight);

            SpawnCoins(volumes, from).Forget();
        }

        private List<int> GetPartitions(int totalWeight)
        {
            List<int> volumes = new();
            int total = 0;
            
            int volumeIndex = 4;
            int adding = VolumeAmounts[volumeIndex];
            while (totalWeight != total)
            {
                if (totalWeight - total < adding)
                {
                    volumeIndex--;
                    adding = VolumeAmounts[volumeIndex];
                    continue;
                }
                
                total += adding;
                volumes.Add(volumeIndex + 1);

                if (total == totalWeight)
                    break;

                if (adding != 1)
                {
                    //After we add the Big Coin, add another any other smaller coin if possible
                    var randomSmallerAdding = VolumeAmounts.Where(a => totalWeight - total >= a && a != adding).ToList().GetRandom();
                    total += randomSmallerAdding;
                    volumes.Add(VolumeAmounts.IndexOf(randomSmallerAdding) + 1);
                }
            }

            return volumes;
        }

        public async Task SpawnCoins(List<int> volumes, Vector3 from)
        {
            foreach (var volume in volumes)
            {
                _visual.SpawnCoin(volume, from);
                await Task.Delay(200);
            }
        }
    }
}