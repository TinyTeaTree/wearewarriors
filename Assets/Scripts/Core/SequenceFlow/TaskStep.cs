using System.Threading.Tasks;

namespace Core
{
    public partial class SequenceFlow
    {
        public class TaskStep : Step
        {
            private Task _task;

            public TaskStep(Task task)
            {
                _task = task;
            }

            protected override async Task InternalExecute()
            {
                await _task;
            }
        }
    }
}