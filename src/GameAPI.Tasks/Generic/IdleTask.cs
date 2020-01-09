using GameAPI.Async;
using System;
using System.Threading.Tasks;

namespace GameAPI.Tasks.Generic
{
    [Obsolete("You probably shouldn't be using this. Instead, leave the task queue empty and queue a task when required.")]
    public class IdleTask<TTarget> : GameTask<TTarget> where TTarget : ITaskRunner<TTarget>
    {
        private readonly float delay;

        public IdleTask(float tick = 0.1f)
        {
            delay = tick;
        }

        protected override async Task Run()
        {
            await new Delay(delay);

            if (Target.TaskQueue.Count < 1)
                Target.QueueTask(new IdleTask<TTarget>(delay));
        }
    }
}
