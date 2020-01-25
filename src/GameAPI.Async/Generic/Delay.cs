using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GameAPI.Async.Generic {
    public class Delay {
        private float delay;

        public Delay(float seconds, bool scaled = true) =>
            delay = scaled ?
                seconds / Time.timeScale :
                seconds;

        public TaskAwaiter GetAwaiter() =>
            ((Task)this).GetAwaiter();

        public static implicit operator Task(Delay delay) =>
            Task.Delay(TimeSpan.FromSeconds(delay.delay));
    }
}