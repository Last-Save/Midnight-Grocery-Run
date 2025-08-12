using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameTimeline
{
    [Serializable]
    public class TutorialTimelineController
    {
        [SerializeField] private bool _gameStartedTriggered;
        [SerializeField] private bool _wasdCompletedTriggered;
        [SerializeField] private bool _shiftCompletedTriggered;
        [SerializeField] private bool _wasdAndShiftControlsCompletedTriggered;
        [SerializeField] private bool _tabCompletedTriggered;
        [SerializeField] private bool _flashlightPickupTriggered;
        [SerializeField] private bool _tutorialCompletedTriggered;

        [NonSerialized] private Action _onGameStarted;
        [NonSerialized] private Action _onWasdCompleted;
        [NonSerialized] private Action _onShiftCompleted;
        [NonSerialized] private Action _onWasdAndShiftControlsCompleted;
        [NonSerialized] private Action _onTabCompleted;
        [NonSerialized] private Action _onFlashlightPickup;
        [NonSerialized] private Action _onTutorialCompleted;

        // === Trigger Methods (внутренний вызов) ===

        public void TriggerNewGameStarted()
        {
            if (_gameStartedTriggered) return;
        
            _gameStartedTriggered = true;
            _onGameStarted?.Invoke();
            _onGameStarted = null;
        }

        public void TriggerWasdCompleted()
        {
            if (_wasdCompletedTriggered) return;
        
            _wasdCompletedTriggered = true;
            _onWasdCompleted?.Invoke();
            _onWasdCompleted = null;
        }

        public void TriggerShiftCompleted()
        {
            if (_shiftCompletedTriggered) return;
        
            _shiftCompletedTriggered = true;
            _onShiftCompleted?.Invoke();
            _onShiftCompleted = null;
        }

        public void TriggerWasdAndShiftControlsCompleted()
        {
            if (_wasdAndShiftControlsCompletedTriggered) return;
        
            _wasdAndShiftControlsCompletedTriggered = true;
            _onWasdAndShiftControlsCompleted?.Invoke();
            _onWasdAndShiftControlsCompleted = null;
        }

        public void TriggerTabCompleted()
        {
            if (_tabCompletedTriggered) return;
        
            _tabCompletedTriggered = true;
            _onTabCompleted?.Invoke();
            _onTabCompleted = null;
        }

        public void TriggerOnFlashlightPickup()
        {
            if (_flashlightPickupTriggered) return;
        
            _flashlightPickupTriggered = true;
            _onFlashlightPickup?.Invoke();
            _onFlashlightPickup = null;
        }

        public void TriggerTutorialCompleted()
        {
            if (_tutorialCompletedTriggered) return;
        
            _tutorialCompletedTriggered = true;
            _onTutorialCompleted?.Invoke();
            _onTutorialCompleted = null;
        }

        // === Subscribe Methods (внешние подписчики) ===

        public void SubscribeOnNewGameStarted(Action callback)
        {
            if (_gameStartedTriggered)
            {
                callback?.Invoke();
                return;
            }

            _onGameStarted += callback;
        }

        public void SubscribeOnWasdCompleted(Action callback)
        {
            if (_wasdCompletedTriggered)
            {
                callback?.Invoke();
                return;
            }

            _onWasdCompleted += callback;
        }

        public void SubscribeOnShiftCompleted(Action callback)
        {
            if (_shiftCompletedTriggered)
            {
                callback?.Invoke();
                return;
            }

            _onShiftCompleted += callback;
        }

        public void SubscribeOnWasdAndShiftControlsCompleted(Action callback)
        {
            if (_wasdAndShiftControlsCompletedTriggered)
            {
                callback?.Invoke();
                return;
            }

            _onWasdAndShiftControlsCompleted += callback;
        }

        public void SubscribeOnTabCompleted(Action callback)
        {
            if (_tabCompletedTriggered)
            {
                callback?.Invoke();
                return;
            }

            _onTabCompleted += callback;
        }

        public void SubscribeOnFlashlightPickup(Action callback)
        {
            if (_flashlightPickupTriggered)
            {
                callback?.Invoke();
                return;
            }

            _onFlashlightPickup  += callback;
        }

        public void SubscribeOnTutorialCompleted(Action callback)
        {
            if (_tutorialCompletedTriggered)
            {
                callback?.Invoke();
                return;
            }

            _onTutorialCompleted += callback;
        }
    }
}