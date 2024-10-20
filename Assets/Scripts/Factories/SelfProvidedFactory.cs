using System.Threading.Tasks;
using Core;

namespace Factories
{
    public class SelfProvidedFactory : BaseFactory
    {
        private BaseVisual _providedVisual;
        
        public void ProvideVisual(BaseVisual visual)
        {
            _providedVisual = visual;
        }
        
        public override Task<TypeVisual> Create<TypeVisual>()
        {
            return Task.FromResult((TypeVisual)_providedVisual);
        }
    }
}