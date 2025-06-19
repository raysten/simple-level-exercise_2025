using Framework;
using UnityEngine;

namespace DependencyInjection
{
    public class GameplayInstaller : BaseInstaller
    {
        [SerializeField]
        private Updater _updater;

        [SerializeField]
        private GameInitializer _initializer;

        public override void InstallBindings()
        {
            BindInterfacesFromPrefab(_updater);
            BindInterfacesFromInstance(_initializer);
        }
    }
}
