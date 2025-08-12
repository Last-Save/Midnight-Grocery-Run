using System;
using GameTimeline.Quests;
using GameTimeline.TImeLines;
using UnityEngine;
using Utils;
using Zenject;

namespace GameTimeline
{
    public class Triggers : IInitializable, IDisposable
    {
        private TutorialTimelineController _tutorialTimeline;
        private LightsFixTimelineController _lightsFixTimeline;
        private GameObject _electricalPanelFoundTrigger;
        
        private readonly OnTriggerEnterEmitter _onTriggerEnterEmitter;

        public Triggers(
            TutorialTimelineController tutorialTimeline, 
            LightsFixTimelineController lightsFixTimeline,
            [Inject(Id = "_onElectricalPanelTriggerEntered")] OnTriggerEnterEmitter onTriggerEnterEmitter
            )
        {
            _tutorialTimeline = tutorialTimeline;
            _lightsFixTimeline = lightsFixTimeline;
            _onTriggerEnterEmitter = onTriggerEnterEmitter;
            
            onTriggerEnterEmitter.OnPlayerEnter += CompleteSearchOfElectricalPanel;
        }

        public void Initialize()
        {
            _lightsFixTimeline.TriggerFindElectricalPanel();
            //TODO: RIGHT
            // _tutorialTimeline.TriggerNewGameStarted();
        }

        public void CompleteTutorial()
        {
            //TODO: RIGHT
            _lightsFixTimeline.TriggerFindElectricalPanel();
        }

        private void CompleteSearchOfElectricalPanel()
        {
            _lightsFixTimeline.TriggerElectricalPanelFound();
        }

        public void CompleteLightsFix()
        {
            
            
            throw new System.NotImplementedException();
        }
        
        public void Dispose()
        {
            _onTriggerEnterEmitter.OnPlayerEnter -= CompleteSearchOfElectricalPanel;
 
        }
    }
}