using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class WalletVisual : BaseVisual<Wallet>
    {
        [SerializeField] Image image;
        [SerializeField] TextMeshProUGUI coinText;

        public void UpdateCoinUI()
        {
            coinText.text = Feature.Record.WalletBalance.ToString();
        }
    }
}