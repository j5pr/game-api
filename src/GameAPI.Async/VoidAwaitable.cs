using System.Runtime.CompilerServices;


namespace GameAPI.Async {
    public class VoidAwaitable {
        private TaskAwaiter awaiter;
        public TaskAwaiter GetAwaiter() => awaiter;

        public VoidAwaitable(TaskAwaiter awaiter) =>
            this.awaiter = awaiter;
    }
}