using GameTimeline.Quests.SecondChapter.StoreInspection;
using UnityEngine;

namespace GameTimeline
{
    public class SecondChapterTriggers
    {
        private ShopSwitcher _shopSwitcher;
        private StoreInspection _storeInspection;

        public SecondChapterTriggers(ShopSwitcher shopSwitcher, StoreInspection storeInspection)
        {
            _shopSwitcher = shopSwitcher;
            _storeInspection = storeInspection;
        }
        
        public void StartSecondChapterFirstQuest()
        {
            _shopSwitcher.SwitchGameToHorrorShop();
            _storeInspection.StartQuest();
        }
    }
}