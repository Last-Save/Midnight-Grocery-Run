using GameTimeline.Quests.SecondChapter;
using UnityEngine;

namespace GameTimeline
{
    public class SecondChapterTriggers
    {
        private ShopSwitcher _shopSwitcher;
        private StoreInspection _storeInspection;
        private GetOut _getOut;

        public SecondChapterTriggers(ShopSwitcher shopSwitcher, StoreInspection storeInspection, GetOut getOut)
        {
            _shopSwitcher = shopSwitcher;
            _storeInspection = storeInspection;
            _getOut = getOut;
        }
        
        public void StartSecondChapterFirstQuest()
        {
            _shopSwitcher.SwitchGameToHorrorShop();
            _storeInspection.StartQuest(StartSecondChapterSecondQuest);
        }

        public void StartSecondChapterSecondQuest()
        {
            _getOut.StartFromBeginning();
        }
    }
}