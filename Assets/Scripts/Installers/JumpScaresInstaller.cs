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


        [Title("Second chapter")]


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

            
        }

        private void InstallSecondChapter()
        {
            
        }
    }
}