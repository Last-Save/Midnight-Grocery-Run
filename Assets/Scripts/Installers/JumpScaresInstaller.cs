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

        [Title("First chapter")]


        public override void InstallBindings()
        {
            Container.Bind<FirstChapterJumpscare>().AsSingle();
            
            InstallFirstChapter();
            InstallSecondChapter();
        }

        private void InstallFirstChapter()
        {
            Container.Bind<JumpscareTrigger>().WithId("_babyCrying").FromInstance(_babyCrying);
        }

        private void InstallSecondChapter()
        {
            
        }
    }
}