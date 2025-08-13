using GameTimeline;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ShopLocationInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _normalShop;
        [SerializeField] private GameObject _horrorShop;
        [SerializeField] private GameObject _player;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ShopSwitcher>().AsSingle();
            Container.Bind<GameObject>().WithId("_normalShop").FromInstance(_normalShop);
            Container.Bind<GameObject>().WithId("_horrorShop").FromInstance(_horrorShop);
            Container.Bind<GameObject>().WithId("_player").FromInstance(_player);
        }
    }
}