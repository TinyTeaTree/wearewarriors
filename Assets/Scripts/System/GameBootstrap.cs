using Agents;
using Core;
using Factories;
using Services;

namespace Game
{
    public class GameBootstrap : GameInfra
    {
        protected override void AddServices()
        {
            var Notebook = new NotebookService();
            Core.Notebook.NotebookService = Notebook;

            _services.Add<INotebookService>(Notebook);
            _services.Add<ISummoningService>(new SummoningService());
            _services.Add<ILocalConfigService>(new LocalConfigService());
            _services.Add<IPlayerSaveService>(new PlayerSaveService());
            _services.Add<IRecordService>(new RecordService());
            //<New Service>
        }

        protected override void AddFeatures()
        {
            //<New Feature>
        }

        protected override void AddFactories()
        {
            
        }

        protected override void AddAgents()
        {
            _agents.Add<IAppLaunchAgent>(new AppLaunchAgent());
            _agents.Add<IAppExitAgent>(new AppExitAgent());
            //<New Agent>
        }

        protected override void AddRecords()
        {
            //<New Record>
        }

        protected override void BootstrapCustoms()
        {
            BootstrapRecordService();
            BootstrapSummoningService();
            BootstrapLocalConfigurationService();

            AppExitAgent.SelfRegister(_agents.Get<IAppExitAgent>());
        }

        private void BootstrapRecordService()
        {
            var recordService = _services.Get<IRecordService>();
            recordService.SetUp(_records.Values);
        }

        private void BootstrapLocalConfigurationService()
        {
            var localConfigService = _services.Get<ILocalConfigService>();
            localConfigService.SetConfigSO(Services.Get<ISummoningService>().LoadResource<LocalConfigCollectionSO>(Addresses.LocalConfigs));
        }

        private void BootstrapSummoningService()
        {
            var summoner = _services.Get<ISummoningService>();
            Summoner.SummoningService = summoner;
        }

        protected override void StartGame()
        {
            new GameLaunchFlow(this).Execute();
        }
    }
}
