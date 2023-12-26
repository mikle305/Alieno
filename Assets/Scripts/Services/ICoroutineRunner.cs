using System.Collections;
using UnityEngine;

namespace Services
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);

        public void StopCoroutine(Coroutine coroutine);
    }
}