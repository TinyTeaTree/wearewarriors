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

        private List<ItemVisual> items = new();
        
        private bool wasOpen = false;
        public bool WasOpen => wasOpen;
        private void OnEnable()
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
            StartCoroutine(ShopDisplayRoutin(status));
        }

         IEnumerator ShopDisplayRoutin(bool status)
        {
            transform.localScale = status? Vector3.zero : Vector3.one;
            
            gameObject.SetActive(status);
            while (!Mathf.Approximately(transform.localScale.x, status ? 1f : 0f))
            {
                transform.localScale = Vector3.Lerp(transform.localScale, status? Vector3.one : Vector3.zero, 0.1f);
                yield return null;
            }
            
            transform.localScale = status? Vector3.one : Vector3.zero;
            
        }

        public void LoadItems(TShops shopType)
        {
           var shopData = Feature.Config.ShopConfigData.FirstOrDefault(s => s.ShopType == shopType);
           foreach (var item in shopData.ItemsVisual)
           {
               var itemVisual = Instantiate(item, itemsContainer);
               itemVisual.SetFeature(Feature);
               Feature.Record.ItemVisuals.Add(itemVisual);
               items.Add(itemVisual);
           }
           gameObject.SetActive(true);
           DisplayShop(true);
        }

        public bool OnDisplay()
        {
            return gameObject.activeSelf;
        }

        public void SetTriggerStatus(bool status)
        {
            wasOpen = status;
        }
    }
}