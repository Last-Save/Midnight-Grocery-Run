using UHFPS.Runtime;
using Zenject;

namespace GameTimeline.Quests.SecondChapter.StoreInspection
{
    public class GetOut
    {
        
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

        public GetOut(
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
            [Inject(Id = "_fleeComplete")] ObjectiveTrigger fleeComplete
        )
        {
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
        }


        public void StarMainQuest()
        {
            _findDoor.TriggerObjective();
            ObjectsToggler.EnableObject(_doorFoundTrigger.gameObject);
        }
    }
}