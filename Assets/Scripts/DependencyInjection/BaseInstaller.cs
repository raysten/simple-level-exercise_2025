using UnityEngine;
using Zenject;

namespace DependencyInjection
{
    public class BaseInstaller : MonoInstaller
    {
        protected void Bind<T>()
        {
            Container.Bind<T>()
                     .AsSingle()
                     .NonLazy();
        }
        
        protected void BindFromInstance<T>(T instance)
        {
            Container.Bind<T>()
                     .FromInstance(instance)
                     .AsSingle()
                     .NonLazy();
        }
        
        protected void BindInterfacesTo<T>()
        {
            Container.BindInterfacesTo<T>()
                     .AsSingle()
                     .NonLazy();
        }
        
        protected void BindInterfacesAndSelfTo<T>()
        {
            Container.BindInterfacesAndSelfTo<T>()
                     .AsSingle()
                     .NonLazy();
        }
        
        protected void BindInterfacesFromInstance<T>(T instance)
        {
            Container.BindInterfacesTo<T>()
                     .FromInstance(instance)
                     .AsSingle()
                     .NonLazy();
        }
        
        protected void BindInterfacesFromPrefab<T>(T prefab) where T : Object
        {
            Container.BindInterfacesTo<T>()
                     .FromComponentInNewPrefab(prefab)
                     .AsSingle()
                     .NonLazy();
        }
    }
}
