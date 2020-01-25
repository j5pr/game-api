#pragma warning disable CS1998

using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace GameAPI.Tasks
{
    public abstract class GameTaskSync<TTarget> : GameTask<TTarget>
    {
        protected override Task Run() =>
            Task.CompletedTask;

        protected abstract IEnumerator RunSync();

        public new async Task Start()
        {
            Debug.LogWarning("Running a synchronous task asynchronously. Standard Unity yield operations will not work and may freeze task execution.");

            RunSync();
        }

        public new IEnumerator StartSync() =>
            RunSync();
    }
}
