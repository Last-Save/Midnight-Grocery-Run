using System;
using GameTimeline.Quests;
using UHFPS.Runtime;
using UnityEngine;

namespace GameTimeline.JumpScares
{
    public class RadioJumpscare : MonoBehaviour
    {
        [SerializeField] private AudioSource _radioSoundOutput;
        [SerializeField] private GameObject _radio;
        
        private readonly string _playerTag = "Player";
        
        private bool _hasTriggered = false;

        public void TurnRadioOn()
        {
            if (!_hasTriggered) return;
            if (_radioSoundOutput == null || _radioSoundOutput.isPlaying) return;

            ObjectsToggler.SetLayerToInteract(_radio);
            _radioSoundOutput.Play();
        }

        // Метод вызывается в GemeObject radio в инспекторе
        public void TurnRadioOff()
        {
            if (_radioSoundOutput != null && _radioSoundOutput.isPlaying)
            {
                _radioSoundOutput.Stop();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!_hasTriggered && other.CompareTag(_playerTag))
            {
                _hasTriggered = true;

                TurnRadioOn();
                
                Destroy(gameObject);
            }
        }
        
        
    }
}