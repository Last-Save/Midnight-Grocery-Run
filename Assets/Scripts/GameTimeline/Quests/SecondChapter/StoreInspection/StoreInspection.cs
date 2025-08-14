using UHFPS.Runtime;
using UnityEngine;
using Utils;
using Zenject;

namespace GameTimeline.Quests.SecondChapter.StoreInspection
{
    public class StoreInspection
    {
        private ObjectiveTrigger _give;
        private ObjectiveTrigger _complete;
        
        private PlayerHealth _playerHealth;
        
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

        public void StartQuest()
        {
            _playerHealth.OnApplyDamage(85);
            // MyСoroutine.Instance.ScheduleMethodCall(8f, AssignInspectionQuest);
            MyСoroutine.Instance.ScheduleMethodCall(1f, AssignInspectionQuest);
        }

        private void AssignInspectionQuest()
        {
            _playerHealth.OnApplyHeal(85);
            _give.TriggerObjective();
            // MyСoroutine.Instance.ScheduleMethodCall(60f, CompleteInspectionQuest);
            MyСoroutine.Instance.ScheduleMethodCall(1f, CompleteInspectionQuest);
        }

        private void CompleteInspectionQuest()
        {
            _complete.TriggerObjective();
            
            
        }
    }
}