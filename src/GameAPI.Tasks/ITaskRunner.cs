using System.Collections.Generic;

namespace GameAPI.Tasks
{
    public interface ITaskRunner<TType> where TType : ITaskRunner<TType>
    {
        public List<GameTask<TType>> TaskQueue { get; }
        public GameTask<TType>? CurrentTask { get; }

        public bool Idling =>
            TaskQueue.Count == 0 && CurrentTask == null;

        public void Queue(params GameTask<TType>[] tasks)
        {
            foreach (GameTask<TType> task in tasks)
                task.Register((TType)this);

            TaskQueue.AddRange(tasks);
        }

        public void QueueFirst(params GameTask<TType>[] tasks)
        {
            foreach (GameTask<TType> task in tasks)
                task.Register((TType)this);

            TaskQueue.InsertRange(0, tasks);
        }

        public void ClearTasks() =>
            TaskQueue.Clear();

        public void Assign(params GameTask<TType>[] tasks)
        {
            ClearTasks();
            Queue(tasks);
        }

        public void TaskLoop();
    }

    public static class ITaskRunner
    {
        public static ITaskRunner<T> Of<T>(ITaskRunner<T> t) where T: ITaskRunner<T> => t;
    }
}
