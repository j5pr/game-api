using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace GameAPI.Async {
    public class Delay {
        private float delay;

        public Delay(float seconds)
        {
          delay = seconds;
        }

        public TaskAwaiter GetAwaiter()
        {
            return Task.Delay(TimeSpan.FromSeconds(delay)).GetAwaiter();
        }
    }
}