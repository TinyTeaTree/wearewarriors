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
            _features.Add<ILoadingScreen>(new LoadingScreen());
            _features.Add<IGarden>(new Garden());
            _features.Add<IAvatar>(new Avatar());
            _features.Add<IJoystick>(new Joystick());
            _features.Add<ITools>(new Tools());
            _features.Add<IPlayerAccount>(new PlayerAccount());

            //<New Feature>
        }

        protected override void AddFactories()
        {
            _factories.Add(typeof(GardenVisual), new AsyncResourceFactory(Addresses.Garden));
            _factories.Add(typeof(AvatarVisual), new AsyncResourceFactory(Addresses.Avatar));
            _factories.Add(typeof(JoystickVisual), new AsyncResourceFactory(Addresses.JostickCanvas));
            _factories.Add(typeof(LoadingScreenVisual), new ResourceFactory(Addresses.LoadingScreen));
            _factories.Add(typeof(ToolsVisual), new AsyncResourceFactory(Addresses.ToolsVisual));
        }

        protected override void AddAgents()
        {
            _agents.Add<IAppLaunchAgent>(new AppLaunchAgent());
            _agents.Add<IAppExitAgent>(new AppExitAgent());
            _agents.Add<ILogoutAgent>(new LogoutAgent());
            //<New Agent>
        }

        protected override void AddRecords()
        {
            _records.Add(typeof(LoadingScreenRecord), new LoadingScreenRecord());
            _records.Add(typeof(GardenRecord), new GardenRecord());
            _records.Add(typeof(AvatarRecord), new AvatarRecord());
            _records.Add(typeof(JoystickRecord), new JoystickRecord());
            _records.Add(typeof(ToolsRecord), new ToolsRecord());
            _records.Add(typeof(PlayerAccountRecord), new PlayerAccountRecord());
            //<New Record>
        }

        protected override void BootstrapCustoms()
        {
            BootstrapRecordService();
            BootstrapSummoningService();
            BootstrapLocalConfigurationService();
            BootstrapSavedRecords();

            AppExitAgent.SelfRegister(_agents.Get<IAppExitAgent>());
        }

        //If you want your Record to be saved, add your Record here
        private void BootstrapSavedRecords()
        {
            var saveService = _services.Get<IPlayerSaveService>();
            
            saveService.AddSaveRecord(_records[typeof(GardenRecord)]);
            saveService.AddSaveRecord(_records[typeof(PlayerAccountRecord)]);
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
