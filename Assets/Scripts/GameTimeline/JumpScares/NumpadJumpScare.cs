using UHFPS.Runtime;
using UnityEngine;

namespace GameTimeline.JumpScares
{
    public class NumpadJumpScare : MonoBehaviour
    {
        [SerializeField] private JumpscareTrigger _jumpscareTrigger;
        [SerializeField] private int _numbOfKeyPressedToTriggerJumpscare;
        private int _currentEnteredTimes = 0;

        public void OnNumpadKeyPressed()
        {
            _currentEnteredTimes++;

            if (_currentEnteredTimes < _numbOfKeyPressedToTriggerJumpscare) return;
            
            _jumpscareTrigger.TriggerJumpscare();
            enabled = false;
        }
    }
}