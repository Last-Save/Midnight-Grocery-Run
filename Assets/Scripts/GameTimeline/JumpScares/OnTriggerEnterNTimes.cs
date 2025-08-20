using UHFPS.Runtime;
using UnityEngine;

namespace GameTimeline.JumpScares
{
    public class OnTriggerEnterNTimes : MonoBehaviour
    {
        [SerializeField] JumpscareTrigger _jumpScare;
        [SerializeField] int _timesToEnterToEnableJumpscare;
        
        private int _currentEnteredTimes = 0;
        private readonly string _playerTag = "Player";
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_playerTag)) return;

            _currentEnteredTimes++;

            if (_currentEnteredTimes >= _timesToEnterToEnableJumpscare)
            {
                _jumpScare.TriggerJumpscare();
            }
        }
    }
}