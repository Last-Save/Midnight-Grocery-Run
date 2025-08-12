using UnityEngine;
using Zenject;

namespace GameTimeline.Quests.OnItemPickups
{
    public class OnFlashlightPickup : MonoBehaviour
    {
        [Inject]
        private TutorialTimelineController _tutorialTimelineController;

        public void OnPickup()
        {
            _tutorialTimelineController.TriggerOnFlashlightPickup();
        }
    }
}