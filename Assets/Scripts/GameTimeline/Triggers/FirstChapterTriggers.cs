using System;
using GameTimeline.JumpScares;
using GameTimeline.Quests;
using GameTimeline.TImeLines;
using UHFPS.Runtime;
using UnityEngine;
using Utils;
using Zenject;

namespace GameTimeline
{
    public class FirstChapterTriggers : IInitializable, IDisposable
    {
        private TutorialTimelineController _tutorialTimeline;
        private LightsFixTimelineController _lightsFixTimeline;
        private GameObject _electricalPanelFoundTrigger;
        private GameObject _registerTrigger;
        
        private SecondChapterTriggers _secondChapterTriggers;

        private ObjectiveTrigger _registerQuestStart;

        private FirstChapterJumpscare _jumpScaresFirstChapterJumpscare;

        
        private readonly OnTriggerEnterEmitter _onTriggerEnterEmitter;
        private readonly OnTriggerEnterEmitter _onFinishFirstChapter;

        public FirstChapterTriggers(
            TutorialTimelineController tutorialTimeline, 
            LightsFixTimelineController lightsFixTimeline,
            SecondChapterTriggers secondChapterTriggers,
            FirstChapterJumpscare jumpScaresFirstChapterJumpscare,
            [Inject(Id = "_onElectricalPanelTriggerEntered")] OnTriggerEnterEmitter onTriggerEnterEmitter,
            [Inject(Id = "_registerTrigger")] GameObject registerTrigger,
            [Inject(Id = "_registerQuestStart")] ObjectiveTrigger registerQuestStart
            )
        {
            _secondChapterTriggers = secondChapterTriggers;
            
            _tutorialTimeline = tutorialTimeline;
            _lightsFixTimeline = lightsFixTimeline;
            _onTriggerEnterEmitter = onTriggerEnterEmitter;

            _registerTrigger = registerTrigger;
            _onFinishFirstChapter = _registerTrigger.GetComponent<OnTriggerEnterEmitter>();

            _registerQuestStart = registerQuestStart;
            
            _jumpScaresFirstChapterJumpscare = jumpScaresFirstChapterJumpscare;
            
            _onTriggerEnterEmitter.OnPlayerEnter += CompleteSearchOfElectricalPanel;
            _onFinishFirstChapter.OnPlayerEnter += FinishFirstChapter;
        }

        public void Initialize()
        {
            //Начинать сначала
            _tutorialTimeline.TriggerNewGameStarted();
        }

        // public void Initialize()
        // {
        //     FinishFirstChapter();
        // }

        public void CompleteTutorial()
        {
            
            // TODO: баг с корутинами
            _lightsFixTimeline.TriggerFindElectricalPanel();
            
            // TODO: Переместить в GameTimeline.Quests.FirstChapter.Tutorial
            MyСoroutine.Instance.ScheduleMethodCall(5f, _jumpScaresFirstChapterJumpscare.StartBabyCrying);
        }

        private void CompleteSearchOfElectricalPanel()
        {
            _lightsFixTimeline.TriggerElectricalPanelFound();
        }

        public void CompleteLightsFix()
        {
            StartRegisterQuest();
        }
        
        public void Dispose()
        {
            _onTriggerEnterEmitter.OnPlayerEnter -= CompleteSearchOfElectricalPanel;
            //TODO:
            _registerTrigger.GetComponent<OnTriggerEnterEmitter>().OnPlayerEnter -= FinishFirstChapter;
        }

        private void StartRegisterQuest()
        {
            _registerQuestStart.TriggerObjective();
            ObjectsToggler.EnableObject(_registerTrigger);
        }

        private void FinishFirstChapter()
        {
            _secondChapterTriggers.StartSecondChapterFirstQuest();
            Dispose();
        }
    }
}