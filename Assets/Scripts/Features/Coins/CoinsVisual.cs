using System.Collections.Generic;
using System.Linq;
using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class CoinsVisual : BaseVisual<Coins>
    {
        [SerializeField] private List<CoinVisual> coinPrefab;

        private Stack<CoinVisual> coinPool1 = new();
        private Stack<CoinVisual> coinPool2 = new();
        private Stack<CoinVisual> coinPool3 = new();
        private Stack<CoinVisual> coinPool4 = new();
        private Stack<CoinVisual> coinPool5 = new();

        private List<CoinVisual> onGridCoins = new();

        private Stack<CoinVisual> GetVolumeStack(int volume)
        {
            switch (volume)
            {
                case 1: return coinPool1;
                case 2: return coinPool2;
                case 3: return coinPool3;
                case 4: return coinPool4;
                case 5: return coinPool5;
            }

            Notebook.NoteError($"No such volume {volume}");
            return coinPool1;
        }
        
        public void SpawnCoin(int volume, Vector3 position)
        {
            var coin = GetCoin(volume);
            coin.transform.position = position;
            coin.gameObject.SetActive(true);
            
            onGridCoins.Add(coin);
            
            coin.Fly();
        }

        void Update()
        {
            var avatar = Feature.Avatar;
            if (avatar.AvatarTransform == null)
                return;
            
            var avatarPos = Feature.Avatar.AvatarTransform.position.XZ();

            bool didDestroy = false;
            foreach (var coin in onGridCoins)
            {
                if (coin.IsPickable)
                {
                    if (Vector3.Distance(coin.transform.position.XZ(), avatarPos) < 1f)
                    {
                        var adding = Feature.VolumeAmounts[coin.Volume - 1];
                        Feature.Wallet.AddToWallet(adding).Forget();
                        coin.Pick();
                        Feature.FloatingText.PopText(coin.transform.position, coin.Volume.ToString(),2f);
                        coin.SelfDestroy();
                        didDestroy = true;
                    }
                }
            }

            if (didDestroy)
            {
                var leftCoins = onGridCoins.Where(c => !c.IsDestroyed()).ToList();
                onGridCoins.Clear();
                onGridCoins = leftCoins;
            }
        }

        public CoinVisual GetCoin(int volume)
        {
            var pool = GetVolumeStack(volume);
            
            if (pool.Any())
                return pool.Pop();

            var prefab = coinPrefab.First(c => c.Volume == volume);
            return Summoner.CreateAsset(prefab, transform);
        }
    }
}