using System;
using UHFPS.Runtime;
using UnityEngine;
using Utils;
using Zenject;

namespace GameTimeline.Quests.SecondChapter
{
    public class StoreInspection
    {
        private ObjectiveTrigger _give;
        private ObjectiveTrigger _complete;
        
        private PlayerHealth _playerHealth;
        
        private Action _onQuestCompleted;
        
        private StoreInspection(
            [Inject(Id = "_inspectionQuestGiver")] ObjectiveTrigger give, 
            [Inject(Id = "_inspectionQuestCompleter")] ObjectiveTrigger complete,
            [Inject(Id = "_player")] GameObject player
            )
        {
            _give = give;
            _complete = complete;
            _playerHealth = player.GetComponent<PlayerHealth>();
        }

        public void StartQuest(Action onQuestCompleted)
        {
            _onQuestCompleted = onQuestCompleted;
            
            _playerHealth.OnApplyDamage(85);
            // MyСoroutine.Instance.ScheduleMethodCall(8f, AssignInspectionQuest);
            MyСoroutine.Instance.ScheduleMethodCall(5f, AssignInspectionQuest);
        }

        private void AssignInspectionQuest()
        {
            DialogBrain.Instance.PlayNext();
            
            _playerHealth.OnApplyHeal(85);
            _give.TriggerObjective();
            MyСoroutine.Instance.ScheduleMethodCall(60f, CompleteInspectionQuest);
        }

        private void CompleteInspectionQuest()
        {
            DialogBrain.Instance.PlayNext();
            
            _complete.TriggerObjective();
            _onQuestCompleted?.Invoke();
        }
    }
}