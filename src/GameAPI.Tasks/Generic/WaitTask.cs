using GameAPI.Async;
using System.Threading.Tasks;
using UnityEngine;

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
