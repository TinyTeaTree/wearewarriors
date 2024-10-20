using System.Threading.Tasks;

namespace Core
{
    public abstract class BaseFactory
    {
        public abstract Task<TypeVisual> Create<TypeVisual>()
            where TypeVisual : BaseVisual;
    }
}