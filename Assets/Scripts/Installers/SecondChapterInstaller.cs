using GameTimeline.Quests.SecondChapter.StoreInspection;
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
        
        public override void InstallBindings()
        {
            Container.Bind<StoreInspection>().AsSingle();
            Container.Bind<ObjectiveTrigger>().WithId("_inspectionQuestGiver").FromInstance(_inspectionQuestGiver);
            Container.Bind<ObjectiveTrigger>().WithId("_inspectionQuestCompleter").FromInstance(_inspectionQuestCompleter);

            InstallGoQuest();
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
    }
}