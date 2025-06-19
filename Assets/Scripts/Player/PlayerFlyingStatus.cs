using Framework;

namespace Player
{
    public class PlayerFlyingStatus : IFlyingStatus
    {
        private readonly IFlyingInput _flyingInput;
        
        public bool IsFlying { get; private set; }

        public PlayerFlyingStatus(
            IGameInitializer initializer, IUpdateProvider updateProvider, IFlyingInput flyingInput)
        {
            _flyingInput = flyingInput;
            
            initializer.OnGameInitialized += Initialize;
            
            void Initialize()
            {
                initializer.OnGameInitialized -= Initialize;
                initializer.OnGameDeinitialized += Deinitialize;

                updateProvider.OnUpdate += Update;
            }

            void Deinitialize()
            {
                initializer.OnGameDeinitialized -= Deinitialize;
                
                updateProvider.OnUpdate -= Update;
            }
        }

        private void Update()
        {
            if (_flyingInput.IsFlyPressed)
            {
                IsFlying = IsFlying == false;
            }
        }
    }
}
