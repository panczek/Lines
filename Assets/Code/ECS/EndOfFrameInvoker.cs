using System.Collections;
using UnityEngine;

namespace Code.ECS
{
    public class EndOfFrameInvoker : MonoBehaviour
    {
        public readonly static EventList OnOnce = new EventList();
        public readonly static EventList OnEveryFrame = new EventList();

        private readonly static WaitForEndOfFrame wait = new WaitForEndOfFrame();

        private void OnEnable()
        {
            OnOnce.ClearHandlers();
            OnEveryFrame.ClearHandlers();

            StartCoroutine( Coroutine() );
        }

        private void OnDisable()
        {
            OnOnce.ClearHandlers();
            OnEveryFrame.ClearHandlers();
        }

        private IEnumerator Coroutine()
        {
            while( isActiveAndEnabled )
            {
                yield return wait;
                OnOnce.Invoke();
                OnOnce.ClearHandlers();

                OnEveryFrame.Invoke();
            }
        }
    }
}
