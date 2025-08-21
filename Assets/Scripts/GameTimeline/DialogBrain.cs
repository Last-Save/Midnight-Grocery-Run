using System;
using UHFPS.Runtime;
using UnityEngine;

namespace GameTimeline
{
    public class DialogBrain : MonoBehaviour
    {
        public static DialogBrain Instance { get; private set; }

        [SerializeField] private DialogueTrigger[] _dialogues;
        private int _currentDialogueIndex = 0;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void PlayNext()
        {
            if (_dialogues.Length < _currentDialogueIndex)
                return;
            
            Debug.Log($"Played # {_currentDialogueIndex}");
            _dialogues[_currentDialogueIndex].TriggerDialogue();
            _currentDialogueIndex++;
        }
    }
}
