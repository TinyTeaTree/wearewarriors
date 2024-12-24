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
            if (status)
            {
                gameObject.SetActive(true);
            }

            if (gameObject.activeSelf)
            {
                StopAllCoroutines();
                StartCoroutine(ShopDisplayRoutine(status));
            }
        }
        
        public void ShakeShop()
        {
            StartCoroutine(ShakeRoutine(0.2f, 6f));
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
        
        private IEnumerator ShopDisplayRoutine(bool status)
        {
            transform.localScale = status ? Vector3.zero : transform.localScale;
            
            while (!Mathf.Approximately(transform.localScale.x, status ? 1f : 0f))
            {
                transform.localScale = Vector3.Lerp(transform.localScale, status? Vector3.one : Vector3.zero, 0.1f);
                yield return null;
            }
            
            transform.localScale = status? Vector3.one : Vector3.zero;
            
            gameObject.SetActive(status);
        }
        
        private IEnumerator ShakeRoutine(float duration, float magnitude)
        {
            Vector3 originalPos = transform.localPosition;
            float elapsed = 0.0f;
        
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;
            
                transform.localPosition = new Vector3(
                    originalPos.x + x,
                    originalPos.y + y,
                    originalPos.z
                );
                
                yield return null;
            }
            transform.localPosition = originalPos;
        }
    }
}