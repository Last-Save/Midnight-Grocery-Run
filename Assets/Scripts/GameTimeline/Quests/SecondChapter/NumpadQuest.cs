using System;
using System.Collections.Generic;
using UHFPS.Runtime;
using UnityEngine;

namespace GameTimeline.Quests.SecondChapter
{
    public class NumpadQuest
    {
        public Action OnQuestCompleted;
        
        private KeypadPuzzle _keypadPuzzle;

        public NumpadQuest(KeypadPuzzle keypadPuzzle)
        {
            _keypadPuzzle = keypadPuzzle;
        }

        public void Start()
        {
            _keypadPuzzle.OnAccessGranted.AddListener(OnComplete);
        }

        private void OnComplete()
        {
            OnQuestCompleted?.Invoke();
            
            _keypadPuzzle.OnAccessGranted.RemoveListener(OnComplete);
        }
    }
}