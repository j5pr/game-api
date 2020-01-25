#pragma warning disable 1998

using System;
using System.Threading.Tasks;

namespace GameAPI.Tasks.Generic
{
    public class ExecTask<TTarget> : GameTask<TTarget> where TTarget : ITaskRunner<TTarget>
    {
        private readonly Action function;

        public ExecTask(Func<object?> function) =>
            this.function = () => { function(); };

        public ExecTask(Action function) =>
            this.function = function;

        protected override async Task Run() =>
            function();
    }
}