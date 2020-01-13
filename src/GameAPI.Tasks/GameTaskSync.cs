using System;
using System.Threading.Tasks;
using System.Collections;

namespace GameAPI.Tasks
{
    public abstract class GameTaskSync<TTarget> : GameTask<TTarget>
    {
        protected override Task Run() =>
            Task.CompletedTask;

        protected abstract IEnumerator RunSync();

        public new Task Start() =>
            throw new Exception("Non-Async Task. Call StartSync method instead.");

        public new IEnumerator StartSync() =>
            RunSync();
    }
}
