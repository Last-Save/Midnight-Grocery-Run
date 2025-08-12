using System;
using UnityEngine;

namespace Utils
{
    public class OnTriggerEnterEmitter : MonoBehaviour
    {
        public event Action OnPlayerEnter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerEnter?.Invoke();
            }
        }
    }
}
