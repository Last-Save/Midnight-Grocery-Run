using UHFPS.Input;
using UnityEngine;
using Zenject;

namespace GameTimeline.Quests.FirstChapter.Tutorial
{
    public class TutorialPlayerInputController : MonoBehaviour
    {
        private TutorialTimelineController _tutorialTimelineController;

        private bool _isWasdCompleted;
        private bool _isSprintCompleted;
        
        private bool _pressedForward;
        private bool _pressedLeft;
        private bool _pressedBackward;
        private bool _pressedRight;
        
        [Inject]
        public void Install(TutorialTimelineController tutorialTimelineController)
        {
            _tutorialTimelineController = tutorialTimelineController;
        }

        private void OnEnable()
        {
            ResetProgress();
        }
        
        private void Update()
        {
            if (!_isWasdCompleted)
                HandleMovementInput();
            if (!_isSprintCompleted)
                HandleSprintInput();
            
            if (_isWasdCompleted && _isSprintCompleted)
                _tutorialTimelineController.TriggerWasdAndShiftControlsCompleted();
        }
        
        /// <summary>
        /// Проверка движения по всем WASD
        /// </summary>
        private void HandleMovementInput()
        {
            if (!InputManager.ReadInput(Controls.MOVEMENT, out Vector2 rawInput))
                return;

            if (rawInput.y > 0.5f) _pressedForward = true;
            if (rawInput.y < -0.5f) _pressedBackward = true;
            if (rawInput.x < -0.5f) _pressedLeft = true;
            if (rawInput.x > 0.5f) _pressedRight = true;

            if (!_pressedForward || !_pressedLeft || !_pressedBackward || !_pressedRight)
                return;

            _isWasdCompleted = true;
            
            if (_isSprintCompleted)
                _tutorialTimelineController.TriggerWasdAndShiftControlsCompleted();
                
            _tutorialTimelineController.TriggerWasdCompleted();
        }

        /// <summary>
        /// Проверка комбинации Shift + W
        /// </summary>
        private void HandleSprintInput()
        {
            if (!InputManager.ReadInput(Controls.MOVEMENT, out Vector2 moveInput) ||
                !InputManager.ReadButton(Controls.SPRINT))
                return;

            _isSprintCompleted = true;

            if (_isWasdCompleted)
                _tutorialTimelineController.TriggerWasdAndShiftControlsCompleted();
            
            _tutorialTimelineController.TriggerShiftCompleted();
        }

        
        private void ResetProgress()
        {
            _isWasdCompleted = false;
            _isSprintCompleted = false;
            
            _pressedForward = false;
            _pressedLeft = false;
            _pressedBackward = false;
            _pressedRight = false;
        }
    }
}