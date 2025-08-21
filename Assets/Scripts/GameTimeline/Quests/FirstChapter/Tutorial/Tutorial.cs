using System.Collections;
using GameTimeline.Quests.FirstChapter.Tutorial.UI;
using GameTimeline.Quests.Presenters;
using Sirenix.OdinInspector;
using UHFPS.Runtime;
using UnityEngine;
using Utils;
using Zenject;

namespace GameTimeline.Quests.FirstChapter.Tutorial
{
    public class Tutorial : IInitializable
    {
        private readonly TutorialTimelineController _tutorialTimeline;
        private TutorialPlayerInputController _inputController;
        private NotificationExplainerOpener _notificationExplainerOpener;
        private FirstChapterTriggers _firstChapterTriggers;
        
        [Title("Tutorial Triggers")]
        private readonly ObjectiveTrigger _basicControlsGiver;
        private readonly ObjectiveTrigger _flashlightSearchGiver;
        private readonly ObjectiveTrigger _flashlightEquip;
        private readonly ObjectiveTrigger _wasdCompleter;
        private readonly ObjectiveTrigger _shiftCompleter;
        private readonly ObjectiveTrigger _tabCompleter;
        
        private GameObject _flashlightFound;
        private GameObject _flashlight;

        [Inject]
        public Tutorial(
            TutorialTimelineController tutorialTimelineController,
            FirstChapterTriggers firstChapterTriggers,
            TutorialPlayerInputController inputController,
            NotificationExplainerOpener notificationExplainerOpener,
            [Inject(Id = "_basicControlsGiver")] ObjectiveTrigger basicControlsGiver,
            [Inject(Id = "_flashlightSearchGiver")] ObjectiveTrigger flashlightSearchGiver,
            [Inject(Id = "_flashlightEquip")] ObjectiveTrigger flashlightEquip,
            [Inject(Id = "_wasdCompleter")] ObjectiveTrigger wasdCompleter,
            [Inject(Id = "_shiftCompleter")] ObjectiveTrigger shiftCompleter,
            [Inject(Id = "_tabCompleter")] ObjectiveTrigger tabCompleter,
            [Inject(Id = "_flashlightFound")] GameObject flashlightFound,
            [Inject(Id = "_flashlight")] GameObject flashlight
            
        )
        {
            _tutorialTimeline = tutorialTimelineController;
            _firstChapterTriggers = firstChapterTriggers;
            _inputController = inputController;
            _notificationExplainerOpener = notificationExplainerOpener;
            
            _basicControlsGiver = basicControlsGiver;
            _flashlightSearchGiver = flashlightSearchGiver;
            _flashlightEquip = flashlightEquip;
            _wasdCompleter = wasdCompleter;
            _shiftCompleter = shiftCompleter;
            _tabCompleter = tabCompleter;
            
            _flashlightFound = flashlightFound;
            _flashlight = flashlight;
        }

        public void Initialize()
        {
            _tutorialTimeline.SubscribeOnNewGameStarted(AssignBasicTutorialQuests);
            _tutorialTimeline.SubscribeOnNewGameStarted(PlayFirstVoiceLine);
            _tutorialTimeline.SubscribeOnWasdCompleted(CompleteWasd);
            _tutorialTimeline.SubscribeOnShiftCompleted(CompleteShift);
            _tutorialTimeline.SubscribeOnWasdAndShiftControlsCompleted(AssignFindingFlashlightQuest);
            _tutorialTimeline.SubscribeOnTabCompleted(CompleteTab);
            _tutorialTimeline.SubscribeOnFlashlightPickup(OnFlashlightPickup);
            _tutorialTimeline.SubscribeOnTutorialCompleted(CompleteTutorial);
        }

        private void AssignBasicTutorialQuests()
        {
            _basicControlsGiver.TriggerObjective();
            _notificationExplainerOpener.DrawUi(NotificationPresenter.INVENTORY_OPEN_EXPLANATION);

            _inputController.enabled = true;
        }

        private void PlayFirstVoiceLine()
        {
            MyСoroutine.Instance.ScheduleMethodCall(0.3f, DialogBrain.Instance.PlayNext);
        }

        private void CompleteWasd()
        {
            _wasdCompleter.TriggerObjective();
        }

        private void CompleteShift()
        {
            _shiftCompleter.TriggerObjective();
        }

        //TODO: Delete me
        private void CompleteTab()
        {
            _tabCompleter.TriggerObjective();
            // _notificationExplainerOpener.Dispose();
        }

        private void AssignFindingFlashlightQuest()
        {
            _inputController.enabled = false;
            
            ObjectsToggler.EnableObject(_flashlightFound);
            ObjectsToggler.SetLayerToInteract(_flashlight);
            
            _flashlightSearchGiver.TriggerObjective();
        }

        private void OnFlashlightPickup()
        {
            _flashlightEquip.TriggerObjective();
            _notificationExplainerOpener.DrawUi(NotificationPresenter.INVENTORY_OPEN_EXPLANATION);
            
            MyСoroutine.Instance.ScheduleCompleteTutorial(10f);
        }

        
        private void CompleteTutorial()
        {
            _inputController.enabled = false;
            _notificationExplainerOpener.Dispose();
            _firstChapterTriggers.CompleteTutorial();
        }
    }
}
