using Core;
using Game;
using UnityEngine;

public class ShopEnterDetector : BaseVisual<Shop>
{
    [SerializeField] private TShops shop;
    [SerializeField] private Transform sellPoint;
    public TShops ShopType => shop;
    public Transform SellPoint => sellPoint;

    void Awake()
    {
        SetFeature((Shop)GameInfra.Single.Features.Get<IShop>());
    }
    
    private void Update()
    {
        Feature.CheckLocation(this);
    }
}
