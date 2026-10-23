using Assets.Scripts.Gameplay.Services.Factories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core
{
    public class InitInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ShipFactory>().AsSingle();
        }
    }
}