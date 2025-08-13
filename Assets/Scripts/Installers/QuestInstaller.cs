using GameTimeline;
using GameTimeline.Quests.FirstChapter.LightsFix;
using GameTimeline.Quests.FirstChapter.Tutorial;
using GameTimeline.Quests.FirstChapter.Tutorial.UI;
using GameTimeline.Quests.OnItemPickups;
using GameTimeline.TImeLines;
using Sirenix.OdinInspector;
using UHFPS.Runtime;
using UnityEngine;
using Utils;
using Zenject;
using Tutorial = GameTimeline.Quests.FirstChapter.Tutorial.Tutorial;

namespace Installers
{
    public class QuestInstaller : MonoInstaller
    {
        [SerializeField] private TutorialPlayerInputController _inputController;
        [SerializeField] private TutorialPlayerTabInputController _tabInputController;
        [SerializeField] private GameObject _tabUI;
        
        [Title("Toggles Interactables")]
        [SerializeField] private GameObject _flashlight;
        [SerializeField] private GameObject[] _fuses;

        [Title("Toggles Triggers")]
        [SerializeField] private GameObject _flashlightFound;
        [SerializeField] private GameObject _electricalPanelFoundTrigger;
        [SerializeField] private GameObject _registerTrigger;
        
        [Title("Tutorial Triggers")]
        [SerializeField] private ObjectiveTrigger _basicControlsGiver;
        [SerializeField] private ObjectiveTrigger _flashlightSearchGiver;
        [SerializeField] private ObjectiveTrigger _flashlightEquip;
        [SerializeField] private ObjectiveTrigger _wasdCompleter;
        [SerializeField] private ObjectiveTrigger _shiftCompleter;
        [SerializeField] private ObjectiveTrigger _tabCompleter;
        
        [Title("UHFPS events")]
        // [SerializeField] private OnFlashlightPickup _onFlashlightPickup;
        
        [Title("Fix lights Triggers")]
        [SerializeField] private ObjectiveTrigger _findElectricalPanel;
        [SerializeField] private ObjectiveTrigger _oneFuseFound;
        [SerializeField] private ObjectiveTrigger _installFusesQuest;
        [SerializeField] private ObjectiveTrigger _fusesInstalledQuest;
        [SerializeField] private FuseboxPuzzle _fuseboxPuzzle;
        
        
        [Title("Register Quests")]
        [SerializeField] private ObjectiveTrigger _registerQuestStart;


        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LightsFix>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SecondChapterTriggers>().AsSingle().NonLazy();
            BindSceneQuests();
            BindLightsFix();
            BindRegisterQuest();
            InstallUi();
            InstallQuestsLogic();
            InstallTogglesInteractables();
            InstallTogglesTriggers();
            InstallUhfpsEvents();
            InstallElse();
        }
       

        private void InstallUhfpsEvents()
        {
            // Container.Bind<OnFlashlightPickup>().FromInstance(_onFlashlightPickup).AsSingle();
        }

        private void InstallTogglesTriggers()
        {
            Container.Bind<GameObject>().WithId("_flashlightFound").FromInstance(_flashlightFound);
            Container.Bind<GameObject>().WithId("_electricalPanelFoundTrigger").FromInstance(_electricalPanelFoundTrigger);
            Container.Bind<GameObject>().WithId("_registerTrigger").FromInstance(_registerTrigger);
        }

        private void InstallTogglesInteractables()
        {
            Container.Bind<GameObject>().WithId("_flashlight").FromInstance(_flashlight);
            Container.Bind<GameObject[]>().WithId("_fuses").FromInstance(_fuses);
        }

        private void InstallUi()
        {
            Container.Bind<GameObject>().WithId("_notificationsUi").FromInstance(_tabUI);
            Container.BindInterfacesAndSelfTo<NotificationExplainerOpener>().AsSingle();
        }

        private void BindSceneQuests()
        {
            BindTutorial();
        }

        private void InstallQuestsLogic()
        {
            Container.Bind<TutorialTimelineController>().To<TutorialTimelineController>().AsSingle();
            Container.BindInterfacesAndSelfTo<Tutorial>().AsSingle();
            Container.Bind<TutorialPlayerInputController>().FromInstance(_inputController).AsSingle();
            Container.Bind<TutorialPlayerTabInputController>().FromInstance(_tabInputController).AsSingle();
            Container.BindInterfacesAndSelfTo<FirstChapterTriggers>().AsSingle();
        }

        private void BindTutorial()
        {
            Container.Bind<ObjectiveTrigger>().WithId("_basicControlsGiver").FromInstance(_basicControlsGiver);
            Container.Bind<ObjectiveTrigger>().WithId("_flashlightSearchGiver").FromInstance(_flashlightSearchGiver);
            Container.Bind<ObjectiveTrigger>().WithId("_flashlightEquip").FromInstance(_flashlightEquip);
            Container.Bind<ObjectiveTrigger>().WithId("_wasdCompleter").FromInstance(_wasdCompleter);
            Container.Bind<ObjectiveTrigger>().WithId("_shiftCompleter").FromInstance(_shiftCompleter);
            Container.Bind<ObjectiveTrigger>().WithId("_tabCompleter").FromInstance(_tabCompleter);
        }
        
        private void BindLightsFix()
        {
            Container.Bind<LightsFixTimelineController>().AsSingle();
            
            Container.Bind<ObjectiveTrigger>().WithId("_findElectricalPanel").FromInstance(_findElectricalPanel);
            Container.Bind<ObjectiveTrigger>().WithId("_oneFuseFound").FromInstance(_oneFuseFound);
            Container.Bind<ObjectiveTrigger>().WithId("_installFusesQuest").FromInstance(_installFusesQuest);
            Container.Bind<ObjectiveTrigger>().WithId("_fusesInstalledQuest").FromInstance(_fusesInstalledQuest);
            Container.Bind<FuseboxPuzzle>().FromInstance(_fuseboxPuzzle);
        }

        private void BindRegisterQuest()
        {
            Container.Bind<ObjectiveTrigger>().WithId("_registerQuestStart").FromInstance(_registerQuestStart);
        }
        
        private void InstallElse()
        {
            Container.Bind<OnTriggerEnterEmitter>().WithId("_onElectricalPanelTriggerEntered").FromInstance(_electricalPanelFoundTrigger.GetComponent<OnTriggerEnterEmitter>());
        }
    }
}