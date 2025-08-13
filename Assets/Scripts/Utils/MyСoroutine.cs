using System;
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
            StartCoroutine(CompleteTutorialAfterDelay(seconds));
        }

        public void ScheduleMethodCall(float seconds, Action callback)
        {
            StartCoroutine(CallAfterDelay(seconds, callback));
        }

        private IEnumerator CompleteTutorialAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _tutorialTimelineController.TriggerTutorialCompleted();
        }
        
        private IEnumerator CallAfterDelay(float delay, Action callback)
        {
            yield return new WaitForSeconds(delay);
            callback?.Invoke();
        }
        
    }
}
