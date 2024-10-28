using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agents;
using Core;
using Services;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Linq;

namespace Game
{
    public class PlayerAccount : BaseFeature, IPlayerAccount, IAppLaunchAgent
    {
        [Inject] public PlayerAccountRecord Record { get; set; }
        [Inject] public IPlayerSaveService Saver { get; set; }
        [Inject] public ILocalConfigService ConfigService { get; set; }
        
        [Inject] public ILogoutAgent LogoutAgent { get; set; }

        public bool IsLoggedIn => Record.PlayerId.HasContent();
        public string PlayerId => Record.PlayerId;
        
        public PlayerAccountConfig Config { get; set; }
        
        public Task AppLaunch()
        {
            Config = ConfigService.GetConfig<PlayerAccountConfig>();
            return Task.CompletedTask;
        }

        public async Task Login()
        {
            if (IsLoggedIn)
            {
                await SyncPlayerData();
                await Logout();
            }

            var savedPlayerAccount = await Saver.GetSavedJson(Record.Id);
            if (savedPlayerAccount.IsNullOrEmpty())
            {
                if (Config.CreateNewPlayerAutomatically)
                {
                    await CreateNewPlayer();
                    await SyncPlayerData();
                }
            }
            else
            {
                Record.Populate(savedPlayerAccount);
            }

            var records = Saver.RecordsForSaving;
            foreach (var record in records)
            {
                var saveJson = await Saver.GetSavedJson(record.Id);
                record.Populate(saveJson);
            }

            await Task.Delay(TimeSpan.FromSeconds(0.1f)); //Intentional delay to test loading
        }

        public Task Logout()
        {
            LogoutAgent.Logout();

            Record.Reset();

            return Task.CompletedTask;
        }

        public Task CreateNewPlayer()
        {
            Record.PlayerId = Guid.NewGuid().GetHashCode().ToString();

            var records = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(Config.NewPlayerRecords.text);

            foreach (var recordKVP in records)
            {
                var recordToStart = Saver.RecordsForSaving.First(r => r.Id == recordKVP.Key);
                recordToStart.Populate(recordKVP.Value);
            }

            return Task.CompletedTask;
        }

        public async Task SyncPlayerData()
        {
            var records = Saver.RecordsForSaving;
            foreach (var record in records)
            {
                await Saver.SaveData(record, record.Id);
            }
        }
    }
}