using GameTimeline.Quests;
using ModestTree;
using UnityEngine;
using Zenject;

namespace GameTimeline
{
    public class ShopSwitcher
    {
        private GameObject _normalShop;
        private GameObject _horrorShop;
        private GameObject _player;
        
        private bool _isHorrorShopCurrent;
        
        private readonly Vector3 _normalShopPlayerPosition = new(-50.98f, 0f, 3.421f);
        private readonly Quaternion _normalShopPlayerRotation = Quaternion.Euler(0f, 0f, 0f);
        private readonly Vector3 _horrorShopPlayerPosition = new(-10.159f, 0f, 21.06f);
        private readonly Quaternion _horrorShopPlayerRotation = Quaternion.Euler(0f, 0f, 0f);

        public ShopSwitcher
        (
            [Inject(Id = "_normalShop")] GameObject normalShop, 
            [Inject(Id = "_horrorShop")] GameObject horrorShop, 
            [Inject(Id = "_player")] GameObject player
        )
        {
            _normalShop = normalShop;
            _horrorShop = horrorShop;
            _player = player;
        }
            
        //TODO:
        // void IInitializable.Initialize()
        // {
        //     _isHorrorShopCurrent = true;
        //     
        //     if (_isHorrorShopCurrent)
        //     {
        //         ToggleToHorrorShop();
        //         return;
        //     }
        //     
        //     ToggleToNormalShop();
        // }
        
        public void SwitchGameToNormalShop()
        {
            ToggleToNormalShop();
            MovePlayerToNormalShopStartPosition();
        }

        public void SwitchGameToHorrorShop()
        {
            ToggleToHorrorShop();
            MovePlayerToHorrorShopStartPosition();
        }

        private void ToggleToNormalShop()
        {
            ObjectsToggler.EnableObject(_normalShop);
            ObjectsToggler.DisableObject(_horrorShop);
        }

        private void ToggleToHorrorShop()
        {
            ObjectsToggler.EnableObject(_horrorShop);
            ObjectsToggler.DisableObject(_normalShop);
        }

        private void MovePlayerToNormalShopStartPosition()
        {
            _player.transform.position = _normalShopPlayerPosition;
            _player.transform.rotation = _normalShopPlayerRotation;
        }

        private void MovePlayerToHorrorShopStartPosition()
        {
            _player.transform.position = _horrorShopPlayerPosition;
            _player.transform.rotation = _horrorShopPlayerRotation;
        }
    }
}