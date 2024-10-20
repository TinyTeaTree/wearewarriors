using System;
using System.Threading.Tasks;

namespace Core
{
    public partial class SequenceFlow
    {
        public class AsyncMethodStep : Step
        {
            private Func<Task> _asyncMethod;
            
            public AsyncMethodStep(Func<Task> asyncMethod)
            {
                _asyncMethod = asyncMethod;
            }

            protected override async Task InternalExecute()
            {
                await _asyncMethod();
            }
        }
    }
}