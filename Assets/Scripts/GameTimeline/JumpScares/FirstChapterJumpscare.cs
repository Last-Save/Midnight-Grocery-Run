using UHFPS.Runtime;
using UnityEngine;
using Zenject;

namespace GameTimeline.JumpScares
{
    public class FirstChapterJumpscare
    {
        private JumpscareTrigger _babyCrying;
        
        public FirstChapterJumpscare(
            [Inject(Id = "_babyCrying")] JumpscareTrigger babyCrying
            )
        {
            _babyCrying = babyCrying;
        }

        public void StartBabyCrying()
        {
            _babyCrying.TriggerJumpscare();

            Debug.Log("*Ребенок плачет*");

            //TODO: Вставить реплику ГГ 
        }
        
    }
}