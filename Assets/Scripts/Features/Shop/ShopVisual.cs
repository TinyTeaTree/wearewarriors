using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game
{
    public class ShopVisual : BaseVisual<Shop>
    {
        [SerializeField] private TShops shopType;
        [SerializeField] private Transform itemsContainer;
        [SerializeField] private Button exitStoreButton;

        private List<ItemVisual> items = new();

        private void OnEnable()
        {
            exitStoreButton.onClick.AddListener(() =>
            {
                DisplayShop(false);

                foreach (var item in items)
                {
                    item.DestroyMe();
                }
                items.Clear();
            });
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void DisplayShop(bool status)
        {
            StartCoroutine(ShopDisplayRoutin(status));
        }

         IEnumerator ShopDisplayRoutin(bool status)
        {
            transform.localScale = status? Vector3.zero : Vector3.one;
            
            while (!Mathf.Approximately(transform.localScale.x, status ? 1f : 0f))
            {
                transform.localScale = Vector3.Lerp(transform.localScale, status? Vector3.one : Vector3.zero, 10f * Time.deltaTime);
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
               items.Add(itemVisual);
           }
           gameObject.SetActive(true);
           DisplayShop(true);
        }

        public bool OnDisplay()
        {
            return gameObject.activeSelf;
        }
    }
}