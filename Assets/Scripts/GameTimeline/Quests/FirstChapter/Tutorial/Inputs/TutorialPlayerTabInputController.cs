using UHFPS.Input;
using UnityEngine;
using Zenject;

namespace GameTimeline.Quests.FirstChapter.Tutorial
{
    public class TutorialPlayerTabInputController : MonoBehaviour
    {
        
        //TODO: Он вообще нужен ли?
        private TutorialTimelineController _tutorialTimelineController;
        private bool _isInventoryOpenCompleted;

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
            if (!_isInventoryOpenCompleted)
                HandleTabInput();
        }
        

        /// <summary>
        /// Проверка нажатия Tab
        /// </summary>
        private void HandleTabInput()
        {
            if (!InputManager.ReadButtonOnce(this, Controls.INVENTORY))
                return;
            
            _tutorialTimelineController.TriggerTabCompleted();
            _isInventoryOpenCompleted = true;
        }
        
        private void ResetProgress()
        {
            _isInventoryOpenCompleted = false;
        }
    }
}