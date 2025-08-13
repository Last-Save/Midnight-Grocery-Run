using GameTimeline.Quests.SecondChapter.StoreInspection;
using UHFPS.Runtime;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SecondChapterInstaller : MonoInstaller
    {
        [SerializeField] private ObjectiveTrigger _inspectionQuestGiver; 
        [SerializeField] private ObjectiveTrigger _inspectionQuestCompleter; 
        
        public override void InstallBindings()
        {
            Container.Bind<StoreInspection>().AsSingle();
            Container.Bind<ObjectiveTrigger>().WithId("_inspectionQuestGiver").FromInstance(_inspectionQuestGiver);
            Container.Bind<ObjectiveTrigger>().WithId("_inspectionQuestCompleter").FromInstance(_inspectionQuestCompleter);
        }
    }
}