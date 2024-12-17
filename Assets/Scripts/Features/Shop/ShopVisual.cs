using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ShopVisual : BaseVisual<Shop>
    {
        [SerializeField] private TShops shopType;
        [SerializeField] private Transform itemsContainer;
        [SerializeField] private Button exitStoreButton;

        private List<ShopItemVisual> items = new();

        public bool WasOpen { get; private set; }

        private void Awake()
        {
            exitStoreButton.onClick.AddListener(() =>
            {
                DisplayShop(false);
                ClearShopItems();
            });
        }

        public void ClearShopItems()
        {
            foreach (var item in items)
            {
                item.SelfDestroy();
            }

            items.Clear();
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void DisplayShop(bool status)
        {
            StopAllCoroutines();
            StartCoroutine(ShopDisplayRoutine(status));
        }

        private IEnumerator ShopDisplayRoutine(bool status)
        {
            transform.localScale = status? Vector3.zero : Vector3.one;
            
            while (!Mathf.Approximately(transform.localScale.x, status ? 1f : 0f))
            {
                transform.localScale = Vector3.Lerp(transform.localScale, status? Vector3.one : Vector3.zero, 0.1f);
                yield return null;
            }
            
            transform.localScale = status? Vector3.one : Vector3.zero;
            
            gameObject.SetActive(status);
        }

        public void LoadItems(TShops shopType)
        {
           var shopData = Feature.Config.ShopConfigData.FirstOrDefault(s => s.ShopType == shopType);
           foreach (var item in shopData.ItemsVisual)
           {
               var itemVisual = Instantiate(item, itemsContainer);
               itemVisual.SetFeature(Feature);
               items.Add(itemVisual);
           }
           gameObject.SetActive(true);
           DisplayShop(true);
        }

        public bool OnDisplay()
        {
            return gameObject.activeSelf;
        }

        public void MarkShopOpen(bool status)
        {
            if (!status && WasOpen)
            {
                DisplayShop(false);
                ClearShopItems();
            }
            
            WasOpen = status;
        }
    }
}