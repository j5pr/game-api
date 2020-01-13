using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GameAPI.Async {
    public class Until {
        private Func<Boolean> condition;
        private int tick;

        public Until(Func<Boolean> condition, int tick = 10) =>
          (this.condition, this.tick) = (condition, tick);

        public TaskAwaiter GetAwaiter() => Task.Run(async () => {

            while (!condition())
                await Task.Delay(tick);

        }).GetAwaiter();
    }
}