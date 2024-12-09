using System.Linq;
using UnityEngine;

namespace Game
{
    public class SeedItemVisual : ItemVisual
    {
        [SerializeField] private TPlant seedType;
        
        private void OnEnable()
        {
            buyButton.onClick.AddListener(() =>
            {
                Feature.Visual.DisplayShop(false);
                Feature.Visual.ClearShopItems();

               var toolConfig = Feature.ConfigService.GetConfig<ToolsConfig>();
               var grainBagPrefab = toolConfig.Tools
                    .FirstOrDefault(t => t.GrainBagSeedType == seedType).prefab;

               var avatarPos = Feature.Avatar.AvatarTransform.position;
               var toolVisual = Feature.Tools.ToolVisual.transform;

               Vector3 grainBagSpawnPos = avatarPos - Vector3.forward * 3 + Vector3.up * 5;
               
               var grainBag = Instantiate(grainBagPrefab, grainBagSpawnPos , Quaternion.identity);
               grainBag.transform.SetParent(toolVisual);

              var toolRecordData = Feature.Tools.ToolsRecord.GardenTools.FirstOrDefault(t => t.Id == grainBag.ToolID);
              
              
               Feature.Tools.ToolsRecord.AllToolsInGarden.Add(grainBag);

            });
        }
    }
}