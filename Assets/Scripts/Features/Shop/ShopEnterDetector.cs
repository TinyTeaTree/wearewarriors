using Core;
using Game;
using UnityEngine;
using UnityEngine.Serialization;

public class ShopEnterDetector : BaseVisual<Shop>
{
    [SerializeField] private TShops shop;
    [SerializeField] private Transform[] sellPoints;
    public TShops ShopType => shop;
    public Transform[] SellPoints => sellPoints;

    void Awake()
    {
        SetFeature((Shop)GameInfra.Single.Features.Get<IShop>());
    }
    
    private void Update()
    {
        Feature.CheckLocation(this);
    }
}
