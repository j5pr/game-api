using System.Threading.Tasks;
using System.Collections;

namespace GameAPI.Tasks
{
    public abstract class GameTask<TTarget>
    {
#nullable disable // Registered in GameTask<T>#Register();

        public TTarget Target;
        
#nullable restore

        public bool IsRunning = false;
        public bool IsComplete = false;

        protected abstract Task Run();

        public async Task Start()
        {
            IsRunning = true;

            await Run();

            IsRunning = false;
            IsComplete = true;
        }

        public IEnumerator StartCoroutine()
        {
            Run().Wait();
            yield break;
        }

        public void Register(TTarget target)
        {
            Target = target;
        }
    }
}
