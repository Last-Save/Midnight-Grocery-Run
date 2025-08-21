using GameTimeline;
using GameTimeline.JumpScares;
using GameTimeline.Quests.FirstChapter.LightsFix;
using Sirenix.OdinInspector;
using UHFPS.Runtime;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class JumpScaresInstaller : MonoInstaller
    {
        [Title("First chapter")]
        [SerializeField] private JumpscareTrigger _babyCrying;
        
        [Title("First chapter GameObjects")]
        [SerializeField] private GameObject _cartTrigger;
        [SerializeField] private GameObject _cartScreamer;


        [Title("Second chapter facial")]
        [SerializeField] private JumpscareTrigger _firstDirectJumpScareGeneric;
        [SerializeField] private JumpscareTrigger _secondDirectJumpScareGeneric;
        [SerializeField] private JumpscareTrigger _thirdDirectJumpScareGeneric;
        [SerializeField] private JumpscareTrigger _fourthDirectJumpScareGeneric;

        

        public override void InstallBindings()
        {
            Container.Bind<FirstChapterJumpscare>().AsSingle();
            
            InstallFirstChapter();
            InstallSecondChapter();
        }

        private void InstallFirstChapter()
        {
            Container.Bind<JumpscareTrigger>().WithId("_babyCrying").FromInstance(_babyCrying);
            
            //Install GameObjects
            Container.Bind<GameObject>().WithId("_cartTrigger").FromInstance(_cartTrigger);
            Container.Bind<GameObject>().WithId("_cartScreamer").FromInstance(_cartScreamer);
        }

        private void InstallSecondChapter()
        {
            Container.Bind<JumpscareTrigger>().WithId("_firstDirectJumpScareGeneric").FromInstance(_firstDirectJumpScareGeneric);
            Container.Bind<JumpscareTrigger>().WithId("_secondDirectJumpScareGeneric").FromInstance(_secondDirectJumpScareGeneric);
            Container.Bind<JumpscareTrigger>().WithId("_thirdDirectJumpScareGeneric").FromInstance(_thirdDirectJumpScareGeneric);
            Container.Bind<JumpscareTrigger>().WithId("_fourthDirectJumpScareGeneric").FromInstance(_fourthDirectJumpScareGeneric);

        }
    }
}