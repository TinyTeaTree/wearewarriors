using Core;
using MergetoolGui;

namespace Services
{
    public interface ILocalConfigService : IService
    {
        void SetConfigSO(LocalConfigCollectionSO so);

        T GetConfig<T>()
            where T : BaseConfig;
    }
}