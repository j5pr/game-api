using System.Collections.Generic;

namespace GameAPI.Tasks
{
    public interface ITaskRunner<TType> where TType : ITaskRunner<TType>
    {
        public List<GameTask<TType>> TaskQueue { get; }
        public GameTask<TType>? CurrentTask { get; }

        public void QueueTask(params GameTask<TType>[] task);

        public void ClearTasks();

        public void AssignTask(params GameTask<TType>[] task);

        public void TaskLoop();
    }
}
