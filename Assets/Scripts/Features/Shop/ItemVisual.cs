using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Core;

namespace Game
{
    public class ItemVisual: BaseVisual<Shop>
    {
        [SerializeField] Image itemImage;
        [SerializeField] Button buyButton;
        [SerializeField] TextMeshProUGUI priceText;

        public void DestroyMe()
        {
            SelfDestroy();
        }
    }
}