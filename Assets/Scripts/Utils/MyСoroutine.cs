using System.Collections;
using GameTimeline;
using UnityEngine;
using Zenject;

namespace Utils
{
    public class MyСoroutine  : MonoBehaviour
    {
        public static MyСoroutine Instance { get; private set; }
        
        [Inject]
        private TutorialTimelineController _tutorialTimelineController;

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

        public void ScheduleCompleteTutorial(float seconds)
        {
            Debug.Log(_tutorialTimelineController);
            StartCoroutine(CompleteTutorialAfterDelay(seconds));
        }

        private IEnumerator CompleteTutorialAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _tutorialTimelineController.TriggerTutorialCompleted();
        }
    }
}
