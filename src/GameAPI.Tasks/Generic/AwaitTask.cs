using System.Threading.Tasks;

namespace GameAPI.Tasks.Generic
{
    public class AwaitTask<TTarget> : GameTask<TTarget> where TTarget : ITaskRunner<TTarget>
    {
        private readonly Task awaitable;

        public AwaitTask(Task awaitable) =>
            this.awaitable = awaitable;

        protected override async Task Run() =>
            await awaitable;
    }
}