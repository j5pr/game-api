using GameAPI.Async.Generic;
using System;
using System.Threading.Tasks;

namespace GameAPI.Tasks.Generic
{
    public class UntilTask<TTarget> : GameTask<TTarget> where TTarget : ITaskRunner<TTarget>
    {
        private readonly Func<bool> condition;

        public UntilTask(Func<bool> condition) =>
            this.condition = condition;

        protected override async Task Run() =>
            await new Until(condition);
    }
}