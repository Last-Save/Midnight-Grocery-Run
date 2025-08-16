using GameTimeline.Quests.SecondChapter;
using Sirenix.OdinInspector;
using UHFPS.Runtime;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SecondChapterInstaller : MonoInstaller
    {
        [SerializeField] private ObjectiveTrigger _inspectionQuestGiver; 
        [SerializeField] private ObjectiveTrigger _inspectionQuestCompleter;

        [Title("GOQuest")] 
        [SerializeField] private ObjectiveTrigger _findDoor; 
        [SerializeField] private ObjectiveTrigger _doorFoundTrigger;
        [SerializeField] private ObjectiveTrigger _threeLocksComplete;
        [SerializeField] private ObjectiveTrigger _openFirstDoor;
        [SerializeField] private ObjectiveTrigger _openFirstDoorComplete;
        [SerializeField] private ObjectiveTrigger _newWayOutTrigger;
        [SerializeField] private ObjectiveTrigger _solveNumpadComplete;
        [SerializeField] private ObjectiveTrigger _exploreCorridor;
        [SerializeField] private ObjectiveTrigger _exploreCorridorTrigger;
        [SerializeField] private ObjectiveTrigger _solveLeversComplete;
        [SerializeField] private ObjectiveTrigger _fleeStart;
        [SerializeField] private ObjectiveTrigger _fleeComplete;
        
        [Title("Interactables DO")]
        [SerializeField] private TimedInteractEvent _padlock01;
        [SerializeField] private TimedInteractEvent _padlock02;
        [SerializeField] private TimedInteractEvent _padlock03;
        [SerializeField] private KeypadPuzzle _keypadPuzzle;

        [Title("Animators")]
        [SerializeField] private Animator _chain01Anim;
        [SerializeField] private Animator _chain02Anim;
        [SerializeField] private Animator _chain03Anim;


        [Title("Interactables GO")]
        [SerializeField] private GameObject _mainHorrorDoorClosed;
        [SerializeField] private GameObject _mainHorrorDoorOpened;
        [SerializeField] private GameObject _stockroomHorrorDoorClosed;
        [SerializeField] private GameObject _stockroomHorrorDoorOpened;
        [SerializeField] private GameObject _finalHorrorDoorClosed;
        [SerializeField] private GameObject _finalHorrorDoorOpened;
        [SerializeField] private GameObject _keypad;
        
        [Title("Doors dynamic objects")]
        [SerializeField] private DynamicObject _mainHorrorDoorOpenedDynamicObject;
        [SerializeField] private DynamicObject _stockroomHorrorDoorOpenDynamicObject;
        [SerializeField] private DynamicObject _finalHorrorDoorOpenedDynamicObject;
        
        public override void InstallBindings()
        {
            Container.Bind<StoreInspection>().AsSingle();
            Container.BindInterfacesAndSelfTo<GetOut>().AsSingle();
            Container.Bind<PadlockQuest>().AsSingle();
            Container.Bind<NumpadQuest>().AsSingle();
            Container.Bind<ObjectiveTrigger>().WithId("_inspectionQuestGiver").FromInstance(_inspectionQuestGiver);
            Container.Bind<ObjectiveTrigger>().WithId("_inspectionQuestCompleter").FromInstance(_inspectionQuestCompleter);
            
            InstallGoQuest();
            InstallInteractablesDo();
            InstallAnimations();
            InstallInteractablesGo();
            InstallDoorsDO();
        }
        

        private void InstallGoQuest()
        {
            Container.Bind<ObjectiveTrigger>().WithId("_findDoor").FromInstance(_findDoor);
            Container.Bind<ObjectiveTrigger>().WithId("_doorFoundTrigger").FromInstance(_doorFoundTrigger);
            Container.Bind<ObjectiveTrigger>().WithId("_threeLocksComplete").FromInstance(_threeLocksComplete);
            Container.Bind<ObjectiveTrigger>().WithId("_openFirstDoor").FromInstance(_openFirstDoor);
            Container.Bind<ObjectiveTrigger>().WithId("_openFirstDoorComplete").FromInstance(_openFirstDoorComplete);
            Container.Bind<ObjectiveTrigger>().WithId("_newWayOutTrigger").FromInstance(_newWayOutTrigger);
            Container.Bind<ObjectiveTrigger>().WithId("_solveNumpadComplete").FromInstance(_solveNumpadComplete);
            Container.Bind<ObjectiveTrigger>().WithId("_exploreCorridor").FromInstance(_exploreCorridor);
            Container.Bind<ObjectiveTrigger>().WithId("_exploreCorridorTrigger").FromInstance(_exploreCorridorTrigger);
            Container.Bind<ObjectiveTrigger>().WithId("_solveLeversComplete").FromInstance(_solveLeversComplete);
            Container.Bind<ObjectiveTrigger>().WithId("_fleeStart").FromInstance(_fleeStart);
            Container.Bind<ObjectiveTrigger>().WithId("_fleeComplete").FromInstance(_fleeComplete);
        }
        
        private void InstallInteractablesDo()
        {
            Container.Bind<TimedInteractEvent>().WithId("_padlock01").FromInstance(_padlock01);
            Container.Bind<TimedInteractEvent>().WithId("_padlock02").FromInstance(_padlock02);
            Container.Bind<TimedInteractEvent>().WithId("_padlock03").FromInstance(_padlock03);
            Container.Bind<KeypadPuzzle>().FromInstance(_keypadPuzzle);
        }
        
        private void InstallAnimations()
        {
            Container.Bind<Animator>().WithId("_chain01Anim").FromInstance(_chain01Anim);
            Container.Bind<Animator>().WithId("_chain02Anim").FromInstance(_chain02Anim);
            Container.Bind<Animator>().WithId("_chain03Anim").FromInstance(_chain03Anim);
        }
        
        private void InstallInteractablesGo()
        {
            Container.Bind<GameObject>().WithId("_mainHorrorDoorClosed").FromInstance(_mainHorrorDoorClosed);
            Container.Bind<GameObject>().WithId("_mainHorrorDoorOpened").FromInstance(_mainHorrorDoorOpened);
            Container.Bind<GameObject>().WithId("_stockroomHorrorDoorClosed").FromInstance(_stockroomHorrorDoorClosed);
            Container.Bind<GameObject>().WithId("_stockroomHorrorDoorOpened").FromInstance(_stockroomHorrorDoorOpened);
            Container.Bind<GameObject>().WithId("_finalHorrorDoorClosed").FromInstance(_finalHorrorDoorClosed);
            Container.Bind<GameObject>().WithId("_finalHorrorDoorOpened").FromInstance(_finalHorrorDoorOpened);
            
            Container.Bind<GameObject>().WithId("_keypad").FromInstance(_keypad);

        }

        private void InstallDoorsDO()
        {
            Container.Bind<DynamicObject>().WithId("_mainHorrorDoorOpenedDynamicObject").FromInstance(_mainHorrorDoorOpenedDynamicObject);
            Container.Bind<DynamicObject>().WithId("_stockroomHorrorDoorOpenDynamicObject").FromInstance(_stockroomHorrorDoorOpenDynamicObject);
            Container.Bind<DynamicObject>().WithId("_finalHorrorDoorOpenedDynamicObject").FromInstance(_finalHorrorDoorOpenedDynamicObject);
        }
    }
}