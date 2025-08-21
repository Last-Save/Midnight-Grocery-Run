using System;
using UHFPS.Runtime;
using UnityEngine;

namespace Utils
{
    public class DeleteMeTest : MonoBehaviour
    {
        public DialogueTrigger trigger;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                trigger.TriggerDialogue();
            }
        }

        private bool _isTriggerd = false;

        private void Update()
        {
            if (!_isTriggerd)
            {
                trigger.TriggerDialogue();
                _isTriggerd = true;
            }
        }
    }
}