using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Installers
{
    public class GameInstaller : MonoInstaller
    {
       

        public override void InstallBindings()
        {
            //ontainer.Bind<ShopManager>().AsSingle();
        }
    }
}