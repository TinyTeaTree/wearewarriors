using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agents;
using Core;
using Services;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;

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
                    return;
                }
                else
                {
                    //The caller must create User manually
                    return;
                }
            }
            else
            {
                Record.Populate(savedPlayerAccount);
            }

            if (Record.Version < PlayerAccountRecord.MigrationRecord)
            {
                //This means that a new version is available. Currently we dont have migrations, just restarting Player Data
                Notebook.NoteCritical($"Migration activated for player {Record.PlayerId}");
                
                await CreateNewPlayer();
                await SyncPlayerData();
                
                return;
            }

            var records = Saver.RecordsForSaving;
            foreach (var record in records)
            {
                if (record.Id == Record.Id)
                    continue; //This Record is already populated
                
                var saveJson = await Saver.GetSavedJson(record.Id);
                if(saveJson.IsNullOrEmpty())
                {
                    Notebook.NoteError($"Do not have any save for {record.Id}, maybe save was corrupted, or migration?");
                    continue;
                }
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
            
            Notebook.NoteCritical($"New User Created {Record.PlayerId}");

            var records = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(Config.NewPlayerRecords.text);

            foreach (var recordKVP in records)
            {
                var recordToStart = Saver.RecordsForSaving.FirstOrDefault(r => r.Id == recordKVP.Key);
                if(recordToStart == null)
                {
                    Notebook.NoteError($"Record {recordKVP.Key} not present in the Records For Saving, Make sure you bootstrapped this record to have save support");
                    continue;
                }
                recordToStart.Populate(recordKVP.Value);
            }

            Record.Version = PlayerAccountRecord.MigrationRecord;

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