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
            _services.Add<ISoundPlayerService>(new SoundPlayerService());
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
            _features.Add<IWallet>(new Wallet());
            _features.Add<ICamera>(new Camera());
            _features.Add<IHud>(new Hud());
            _features.Add<IMarks>(new Marks());
            _features.Add<IAnimals>(new Animals());
            _features.Add<IWorld>(new World());
            _features.Add<ICoins>(new Coins());
            //<New Feature>
        }

        protected override void AddFactories()
        {
            _factories.Add(typeof(GardenVisual), new AsyncResourceFactory(Addresses.Garden));
            _factories.Add(typeof(AvatarVisual), new AsyncResourceFactory(Addresses.Avatar));
            _factories.Add(typeof(JoystickVisual), new AsyncResourceFactory(Addresses.JostickCanvas));
            _factories.Add(typeof(LoadingScreenVisual), new ResourceFactory(Addresses.LoadingScreen));
            _factories.Add(typeof(ToolsVisual), new AsyncResourceFactory(Addresses.ToolsVisual));
            _factories.Add(typeof(CameraVisual), new ResourceFactory(Addresses.CameraVisual));
            _factories.Add(typeof(HudVisual), new ResourceFactory(Addresses.HudVisual));
            _factories.Add(typeof(MarksVisual), new ResourceFactory(Addresses.MarksCanvas));
            _factories.Add(typeof(AnimalsVisual), new ResourceFactory(Addresses.AnimalsVisual));
            _factories.Add(typeof(WalletVisual), new AsyncResourceFactory(Addresses.Wallet));
            _factories.Add(typeof(WorldVisual), new AsyncResourceFactory(Addresses.World));
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
            _records.Add(typeof(CameraRecord), new CameraRecord());
            _records.Add(typeof(WalletRecord), new WalletRecord());
            _records.Add(typeof(AnimalsRecord), new AnimalsRecord());
            _records.Add(typeof(CoinsRecord), new CoinsRecord());
            //<New Record>
        }

        protected override void BootstrapCustoms()
        {
            BootstrapRecordService();
            BootstrapSummoningService();
            BootstrapLocalConfigurationService();
            BootstrapSavedRecords();
            BootstrapSoundSystem();

            AppExitAgent.SelfRegister(_agents.Get<IAppExitAgent>());
        }

        private void BootstrapSoundSystem()
        {
            DJ.SoundPlayer = _services.Get<ISoundPlayerService>();
        }

        //If you want your Record to be saved, add your Record here
        private void BootstrapSavedRecords()
        {
            var saveService = _services.Get<IPlayerSaveService>();
            
            saveService.AddSaveRecord(_records[typeof(GardenRecord)]);
            saveService.AddSaveRecord(_records[typeof(PlayerAccountRecord)]);
            saveService.AddSaveRecord(_records[typeof(ToolsRecord)]);
            saveService.AddSaveRecord(_records[typeof(AvatarRecord)]);
            saveService.AddSaveRecord(_records[typeof(WalletRecord)]);
            saveService.AddSaveRecord(_records[typeof(AnimalsRecord)]);
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
