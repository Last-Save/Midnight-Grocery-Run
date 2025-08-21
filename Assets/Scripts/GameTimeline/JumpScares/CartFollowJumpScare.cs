using System.Collections;
using UnityEngine;

namespace GameTimeline.JumpScares
{
    public class CartFollowJumpScare : MonoBehaviour
    {
        [SerializeField] private float _destroyDelay;
        private bool _hasBeenTriggered = false;
        
        private void OnEnable()
        {
            if (_hasBeenTriggered)
            {
                Destroy(gameObject);
                return;
            }
            
            _hasBeenTriggered = true; 
            StartCoroutine(DestroyThis());
        }

        private IEnumerator DestroyThis()
        {
            yield return new WaitForSeconds(_destroyDelay);
            Destroy(gameObject);
        }
    }
}