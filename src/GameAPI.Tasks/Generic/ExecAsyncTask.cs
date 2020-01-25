using System;
using System.Threading.Tasks;

namespace GameAPI.Tasks.Generic
{
    public class ExecAsyncTask<TTarget> : GameTask<TTarget> where TTarget : ITaskRunner<TTarget>
    {
        private readonly Func<Task> function;

        public ExecAsyncTask(Func<Task<object?>> function) =>
            this.function = function;

        public ExecAsyncTask(Func<Task> function) =>
            this.function = function;

        protected override Task Run() =>
            function();
    }
}