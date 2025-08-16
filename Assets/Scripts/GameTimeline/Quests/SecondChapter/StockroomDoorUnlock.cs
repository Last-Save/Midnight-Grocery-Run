using System;
using UHFPS.Runtime;
using UnityEngine;

namespace GameTimeline.Quests.SecondChapter
{
    public class StockroomDoorUnlock : MonoBehaviour, IDynamicUnlock
    {
        private void Awake()
        {
            OnTryUnlock(gameObject.GetComponent<DynamicObject>());
        }

        public void OnTryUnlock(DynamicObject dynamicObject)
        {
            dynamicObject.dynamicStatus = DynamicObject.DynamicStatus.Normal;
            dynamicObject.SetOpenState();
        }
    }
}