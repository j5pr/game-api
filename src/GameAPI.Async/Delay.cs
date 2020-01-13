using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GameAPI.Async {
    public class Delay {
        private float delay;

        public Delay(float seconds, bool scaled = true) =>
            delay = scaled ?
                seconds / Time.timeScale :
                seconds;

        public TaskAwaiter GetAwaiter() =>
            Task.Delay(TimeSpan.FromSeconds(delay)).GetAwaiter();
    }
}