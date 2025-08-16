using System;
using UHFPS.Runtime;
using UnityEngine;
using Zenject;

namespace GameTimeline.Quests.SecondChapter
{
    public class PadlockQuest
    {
        public Action OnQuestCompleted;
        public Action OnFirstPadlockOpened;
        public Action OnSecondPadlockOpened;
        public Action OnThirdPadlockOpened;
        
        // Interactables DO
        private TimedInteractEvent _padlock01;
        private TimedInteractEvent _padlock02;
        private TimedInteractEvent _padlock03;
        
        //Interactables animations
        private Animator _chain01Anim;
        private Animator _chain02Anim;
        private Animator _chain03Anim;
        
        private static readonly int _open = Animator.StringToHash("Open");
        
        private bool _isFirstLockOpened;
        private bool _isSecondLockOpened;
        private bool _isThirdLockOpened;
        
        public PadlockQuest(
                [Inject(Id = "_padlock01")] TimedInteractEvent padlock01,
                [Inject(Id = "_padlock02")] TimedInteractEvent padlock02,
                [Inject(Id = "_padlock03")] TimedInteractEvent padlock03,
                [Inject(Id = "_chain01Anim")] Animator chain01Anim,
                [Inject(Id = "_chain02Anim")] Animator chain02Anim,
                [Inject(Id = "_chain03Anim")] Animator chain03Anim
            )
        {
            _padlock01 = padlock01; 
            _padlock02 = padlock02; 
            _padlock03 = padlock03; 
            _chain01Anim = chain01Anim; 
            _chain02Anim = chain02Anim; 
            _chain03Anim = chain03Anim; 
        }
        
        public void Start()
        {
            _padlock01.OnInteract.AddListener(OnPadlock01Opened);
            _padlock02.OnInteract.AddListener(OnPadlock02Opened);
            _padlock03.OnInteract.AddListener(OnPadlock03Opened);
        }
        
        private void OnComplete()
        {
            OnQuestCompleted?.Invoke();
            
            _padlock01.OnInteract.RemoveListener(OnPadlock01Opened);
            _padlock02.OnInteract.RemoveListener(OnPadlock02Opened);
            _padlock03.OnInteract.RemoveListener(OnPadlock03Opened);
        }

        private void OnPadlock01Opened()
        {
            if (_isFirstLockOpened)
                throw new InvalidOperationException("This padlock 01 is already opened");

            _chain01Anim.SetTrigger(_open);
                
            _isFirstLockOpened = true;
            OnFirstPadlockOpened?.Invoke();

            TryToCompleteQuest();
        }
        
        private void OnPadlock02Opened()
        {
            if (_isFirstLockOpened)
                throw new InvalidOperationException("This padlock 02 is already opened");

            _chain02Anim.SetTrigger(_open);
            
            _isSecondLockOpened = true;
            OnSecondPadlockOpened?.Invoke();

            TryToCompleteQuest();
        }
        
        private void OnPadlock03Opened()
        {
            if (_isFirstLockOpened)
                throw new InvalidOperationException("This padlock 03 is already opened");
            
            _chain03Anim.SetTrigger(_open);
            
            _isThirdLockOpened = true;
            OnThirdPadlockOpened?.Invoke();

            TryToCompleteQuest();
        }

        private void TryToCompleteQuest()
        {
            if (_isFirstLockOpened && _isSecondLockOpened && _isThirdLockOpened)
                OnComplete();
        }

        
    }
}