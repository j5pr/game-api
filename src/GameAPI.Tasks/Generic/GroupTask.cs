using GameAPI.Async.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameAPI.Tasks.Generic
{
    public class GroupTask<TTarget> : GameTask<TTarget>, ITaskRunner<TTarget> where TTarget : TaskedBehaviour<TTarget>, ITaskRunner<TTarget>
    {
        public List<GameTask<TTarget>> TaskQueue { get; } = new List<GameTask<TTarget>>();
        public GameTask<TTarget>? CurrentTask { get; private set; } = null;

        private List<GameTask<TTarget>>? queue;

        public GroupTask()
        {
        }

        public GroupTask(params GameTask<TTarget>[] tasks) =>
            this.Queue(tasks);

        protected override async Task Run()
        {
            foreach (var task in TaskQueue)
                await task;
        }

        public void TaskLoop() =>
            _ = Run();
    }
}