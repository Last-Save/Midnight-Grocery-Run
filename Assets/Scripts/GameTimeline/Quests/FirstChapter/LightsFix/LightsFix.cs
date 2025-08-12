using GameTimeline.Quests.FirstChapter.Tutorial.UI;
using GameTimeline.Quests.Presenters;
using GameTimeline.TImeLines;
using Sirenix.OdinInspector;
using UHFPS.Runtime;
using UnityEngine;
using Zenject;

namespace GameTimeline.Quests.FirstChapter.LightsFix
{
    public class LightsFix : IInitializable
    {
        private readonly LightsFixTimelineController _timeline;
        private readonly Triggers _triggers;
        private readonly NotificationExplainerOpener _notificationExplainerOpener;

        [Title("Lights Fix Triggers")]
        private readonly ObjectiveTrigger _findElectricalPanelTrigger;
        private readonly ObjectiveTrigger _oneFuseFound;
        private readonly ObjectiveTrigger _findFusesTrigger;
        private readonly ObjectiveTrigger _insertFusesTrigger;
        private readonly ObjectiveTrigger _installFuses;

        private readonly GameObject _electricalPanelFoundTrigger;
        private GameObject[] _fuses;

        private int _fusesCollectedCount = 0;

        [Inject]
        public LightsFix(
            LightsFixTimelineController timeline,
            Triggers triggers,
            [Inject(Id = "_findElectricalPanel")] ObjectiveTrigger findElectricalPanelTrigger,
            [Inject(Id = "_oneFuseFound")] ObjectiveTrigger oneFuseFound,
            [Inject(Id = "_installFusesQuest")] ObjectiveTrigger installFuses,
            [Inject(Id = "_electricalPanelFoundTrigger")] GameObject electricalPanelFoundTrigger,
            [Inject(Id = "_fuses")] GameObject[] fuses
        )
        {
            _timeline = timeline;
            _triggers = triggers;

            _findElectricalPanelTrigger = findElectricalPanelTrigger;
            _oneFuseFound = oneFuseFound;
            _electricalPanelFoundTrigger = electricalPanelFoundTrigger;
            _installFuses = installFuses;
            
            _fuses = fuses;
        }

        public void Initialize()
        {
            _timeline.SubscribeOnFindElectricalPanel(OnFindElectricalPanel);
            _timeline.SubscribeOnElectricalPanelFound(OnElectricalPanelFound);
            _timeline.SubscribeOnFusesFound(OnFusesFound);
            _timeline.SubscribeOnFusesInserted(OnInsertFuses);
        }

        private void OnFindElectricalPanel()
        {
            _findElectricalPanelTrigger.TriggerObjective();
            ObjectsToggler.EnableObject(_electricalPanelFoundTrigger);
        }
        
        private void OnElectricalPanelFound()
        {
            foreach (GameObject fuse in _fuses)
            {
                ObjectsToggler.SetLayerToInteract(fuse);
                fuse.GetComponent<InteractableItem>().OnTakeEvent.AddListener(UpdateCollectedFuses);
            }
        }

        private void OnFusesFound()
        {
            _installFuses.TriggerObjective();
        }

        private void OnInsertFuses()
        {
            _triggers.CompleteLightsFix();
        }
        
        private void UpdateCollectedFuses()
        {
            _fusesCollectedCount++;

            if (_fusesCollectedCount >= 3)
            {
                _timeline.TriggerFusesFound();
            }
            
            _oneFuseFound.TriggerObjective();
        }
    }
}
