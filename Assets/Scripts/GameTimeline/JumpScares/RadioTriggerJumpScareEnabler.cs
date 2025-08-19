using System;
using UHFPS.Runtime;
using UnityEngine;

namespace GameTimeline.JumpScares
{
    public class RadioTriggerJumpScareEnabler : MonoBehaviour
    {
        [SerializeField] private JumpscareTrigger _radioJumpScare;
        
        private string _playerTag = "Player";
        
        private bool _hasTriggered = false;
        private void OnTriggerExit(Collider other)
        {
            if (!_hasTriggered && other.CompareTag(_playerTag))
            {
                _hasTriggered = true;
                
                _radioJumpScare.TriggerJumpscare();
            }
        }
    }
}