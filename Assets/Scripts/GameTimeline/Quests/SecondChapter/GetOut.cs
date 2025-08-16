using System;
using UHFPS.Runtime;
using UnityEngine;
using Zenject;

namespace GameTimeline.Quests.SecondChapter
{
    public class GetOut : IInitializable, IDisposable
    {
        private PadlockQuest _padlockQuest;
        private NumpadQuest _numpadQuest;

        // GOQuest
        private ObjectiveTrigger _findDoor; 
        private ObjectiveTrigger _doorFoundTrigger;
        private ObjectiveTrigger _threeLocksComplete;
        private ObjectiveTrigger _openFirstDoor;
        private ObjectiveTrigger _openFirstDoorComplete;
        private ObjectiveTrigger _newWayOutTrigger;
        private ObjectiveTrigger _solveNumpadComplete;
        private ObjectiveTrigger _exploreCorridor;
        private ObjectiveTrigger _exploreCorridorTrigger;
        private ObjectiveTrigger _solveLeversComplete;
        private ObjectiveTrigger _fleeStart;
        private ObjectiveTrigger _fleeComplete;
        
        // Interactables DO
        private TimedInteractEvent _padlock01;
        private TimedInteractEvent _padlock02;
        private TimedInteractEvent _padlock03;
        private KeypadPuzzle _keypadPuzzle;
        
        //Interactables animations
        private Animator _chain01Anim;
        private Animator _chain02Anim;
        private Animator _chain03Anim;
        
        // Interactables GO
        private GameObject _keypad;
        
        private GameObject _mainHorrorDoorClosed;
        private GameObject _mainHorrorDoorOpened;
        private GameObject _stockroomHorrorDoorClosed;
        private GameObject _stockroomHorrorDoorOpened;
        private GameObject _finalHorrorDoorClosed;
        private GameObject _finalHorrorDoorOpened;
        
        // Doors dynamic objects
        private DynamicObject _mainHorrorDoorOpenedDynamicObject;
        private DynamicObject _stockroomHorrorDoorOpenDynamicObject;
        private DynamicObject _finalHorrorDoorOpenedDynamicObject;

        public GetOut(
            PadlockQuest padlockQuest,
            NumpadQuest numpadQuest,
            [Inject(Id = "_findDoor")] ObjectiveTrigger findDoor,
            [Inject(Id = "_doorFoundTrigger")] ObjectiveTrigger doorFoundTrigger,
            [Inject(Id = "_threeLocksComplete")] ObjectiveTrigger threeLocksComplete,
            [Inject(Id = "_openFirstDoor")] ObjectiveTrigger openFirstDoor,
            [Inject(Id = "_openFirstDoorComplete")] ObjectiveTrigger openFirstDoorComplete,
            [Inject(Id = "_newWayOutTrigger")] ObjectiveTrigger newWayOutTrigger,
            [Inject(Id = "_solveNumpadComplete")] ObjectiveTrigger solveNumpadComplete,
            [Inject(Id = "_exploreCorridor")] ObjectiveTrigger exploreCorridor,
            [Inject(Id = "_exploreCorridorTrigger")] ObjectiveTrigger exploreCorridorTrigger,
            [Inject(Id = "_solveLeversComplete")] ObjectiveTrigger solveLeversComplete,
            [Inject(Id = "_fleeStart")] ObjectiveTrigger fleeStart,
            [Inject(Id = "_fleeComplete")] ObjectiveTrigger fleeComplete,
            [Inject(Id = "_padlock01")] TimedInteractEvent padlock01,
            [Inject(Id = "_padlock02")] TimedInteractEvent padlock02,
            [Inject(Id = "_padlock03")] TimedInteractEvent padlock03,
            KeypadPuzzle keypadPuzzle,
            [Inject(Id = "_mainHorrorDoorClosed")] GameObject mainHorrorDoorClosed,
            [Inject(Id = "_mainHorrorDoorOpened")] GameObject mainHorrorDoorOpened,
            [Inject(Id = "_stockroomHorrorDoorClosed")] GameObject stockroomHorrorDoorClosed,
            [Inject(Id = "_stockroomHorrorDoorOpened")] GameObject stockroomHorrorDoorOpened,
            [Inject(Id = "_finalHorrorDoorClosed")] GameObject finalHorrorDoorClosed,
            [Inject(Id = "_finalHorrorDoorOpened")] GameObject finalHorrorDoorOpened,
            [Inject(Id = "_keypad")] GameObject keypad,
            [Inject(Id = "_chain01Anim")] Animator chain01Anim,
            [Inject(Id = "_chain02Anim")] Animator chain02Anim,
            [Inject(Id = "_chain03Anim")] Animator chain03Anim,
            [Inject(Id = "_mainHorrorDoorOpenedDynamicObject")] DynamicObject mainHorrorDoorOpenedDynamicObject,
            [Inject(Id = "_stockroomHorrorDoorOpenDynamicObject")] DynamicObject stockroomHorrorDoorOpenDynamicObject,
            [Inject(Id = "_finalHorrorDoorOpenedDynamicObject")] DynamicObject finalHorrorDoorOpenedDynamicObject
        )
        {
            _padlockQuest = padlockQuest;
            _numpadQuest = numpadQuest;
                
            _findDoor = findDoor;
            _doorFoundTrigger = doorFoundTrigger;
            _threeLocksComplete = threeLocksComplete;
            _openFirstDoor = openFirstDoor;
            _openFirstDoorComplete = openFirstDoorComplete;
            _newWayOutTrigger = newWayOutTrigger;
            _solveNumpadComplete = solveNumpadComplete;
            _exploreCorridor = exploreCorridor;
            _exploreCorridorTrigger = exploreCorridorTrigger;
            _solveLeversComplete = solveLeversComplete;
            _fleeStart = fleeStart;
            _fleeComplete = fleeComplete;
            _padlock01 = padlock01;
            _padlock02 = padlock02;
            _padlock03 = padlock03;
            _keypadPuzzle = keypadPuzzle;
            _mainHorrorDoorClosed = mainHorrorDoorClosed;
            _mainHorrorDoorOpened = mainHorrorDoorOpened;
            _stockroomHorrorDoorClosed = stockroomHorrorDoorClosed;
            _stockroomHorrorDoorOpened = stockroomHorrorDoorOpened;
            _finalHorrorDoorClosed = finalHorrorDoorClosed;
            _finalHorrorDoorOpened = finalHorrorDoorOpened;
            _keypad = keypad;
            _chain01Anim = chain01Anim;
            _chain02Anim = chain02Anim;
            _chain03Anim = chain03Anim;
            _mainHorrorDoorOpenedDynamicObject = mainHorrorDoorOpenedDynamicObject;
            _stockroomHorrorDoorOpenDynamicObject = stockroomHorrorDoorOpenDynamicObject;
            _finalHorrorDoorOpenedDynamicObject = finalHorrorDoorOpenedDynamicObject;
        }

        void IInitializable.Initialize()
        {
            _mainHorrorDoorOpenedDynamicObject.useEvent1.AddListener(OnMainDoorOpened);
            // _stockroomHorrorDoorOpenDynamicObject.useEvent1.AddListener(OnStockroomDoorOpened);
            _finalHorrorDoorOpenedDynamicObject.useEvent1.AddListener(OnFleeComplete);
        }

        public void StartFromBeginning()
        {
            _findDoor.TriggerObjective();
            ObjectsToggler.EnableObject(_doorFoundTrigger.gameObject);
            
            ObjectsToggler.SetLayerToInteract(_keypad);
            _padlockQuest.Start();
            _padlockQuest.OnQuestCompleted += OnThreePadlocksOpened;

        }

        private void OnThreePadlocksOpened()
        {
            ObjectsToggler.ToggleDoorGameObject(_mainHorrorDoorOpened, _mainHorrorDoorClosed);

            _openFirstDoor.TriggerObjective();
            _threeLocksComplete.TriggerObjective();
        }

        private void OnMainDoorOpened()
        {
            _openFirstDoorComplete.TriggerObjective();
            ObjectsToggler.EnableObject(_newWayOutTrigger.gameObject);
                
            
            _numpadQuest.Start();
            _numpadQuest.OnQuestCompleted += OnNumpadLocksOpened;
        }

        private void OnNumpadLocksOpened()
        {
            ObjectsToggler.ToggleDoorGameObject(_stockroomHorrorDoorOpened, _stockroomHorrorDoorClosed);
            // AudioClip. _stockroomHorrorDoorOpenDynamicObject.unlockSound

            _exploreCorridor.TriggerObjective();
            _solveNumpadComplete.TriggerObjective();
            ObjectsToggler.EnableObject(_exploreCorridorTrigger.gameObject);
        }

        private void OnSolveLeversComplete()
        {
            ObjectsToggler.ToggleDoorGameObject(_finalHorrorDoorOpened, _finalHorrorDoorClosed);
            
            _fleeStart.TriggerObjective();
            _solveLeversComplete.TriggerObjective();
        }

        private void OnFleeComplete()
        {
            _fleeStart.TriggerObjective();
        }
        
        void IDisposable.Dispose()
        {
            _padlockQuest.OnQuestCompleted -= OnThreePadlocksOpened;
            _numpadQuest.OnQuestCompleted -= OnNumpadLocksOpened;

        }
    }
}