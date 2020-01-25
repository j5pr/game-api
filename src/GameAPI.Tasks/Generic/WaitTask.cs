using GameAPI.Async.Generic;
using System.Threading.Tasks;

namespace GameAPI.Tasks.Generic
{
    public class WaitTask<TTarget> : GameTask<TTarget> where TTarget : ITaskRunner<TTarget>
    {
        private readonly float delay;

        public WaitTask(float delay) =>
            this.delay = delay;

        protected override async Task Run() =>
            await new Delay(delay);
    }
}
