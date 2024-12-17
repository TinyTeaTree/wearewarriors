using Core;
using Game;
using UnityEngine;

public class ShopEnterDetector : BaseVisual<Shop>
{
    [SerializeField] private TShops shop;
    public TShops ShopType => shop;
    
    void Awake()
    {
        SetFeature((Shop)GameInfra.Single.Features.Get<IShop>());
    }
    
    private void Update()
    {
        Feature.CheckLocation(this);
    }
}
