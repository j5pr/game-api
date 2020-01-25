using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace GameAPI.Async.Generic {
    public class Until {
        private Func<Boolean> condition;
        private int tick;

        public Until(Func<Boolean> condition, int tick = 10) =>
          (this.condition, this.tick) = (condition, tick);

        public TaskAwaiter GetAwaiter() =>
            ((Task)this).GetAwaiter();
        
        public static implicit operator Task(Until until) => Task.Run(async () =>
        {
            while (!until.condition())
                await Task.Delay(until.tick);
        });
    }
}