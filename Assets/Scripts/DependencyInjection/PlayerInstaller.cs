using Player;
using Player.Powerups;
using PlayerStateMachine;
using Settings;
using UnityEngine;

namespace DependencyInjection
{
    public class PlayerInstaller : BaseInstaller
    {
        [SerializeField]
        private Transform _transform;

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private CapsuleCollider _capsuleCollider;
        
        [SerializeField]
        private PlayerConfig _playerConfig;

        [SerializeField]
        private PlayerShooting _playerShooting;

        private void Reset()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
        }

        public override void InstallBindings()
        {
            BindBasicComponents();
            
            BindMovement();
            BindStateMachine();
            BindPowerups();
            BindSettings();
            BindShooting();
            
            BindInterfacesTo<PlayerInput>();
            BindInterfacesTo<PlayerGroundCheck>();
        }

        private void BindBasicComponents()
        {
            BindFromInstance(_transform);
            BindFromInstance(_rigidbody);
            BindFromInstance(_capsuleCollider);
        }

        private void BindMovement()
        {
            BindInterfacesTo<PlayerMovement>();
            BindInterfacesTo<PlayerVerticalMovement>();
            BindInterfacesTo<PlayerHorizontalSpeed>();
            
            BindInterfacesAndSelfTo<PlayerRotation>();
        }

        private void BindStateMachine()
        {
            Bind<PlayerStateFactory>();
            Bind<TransitionFactory>();
            Bind<PlayerStateController>();
        }

        private void BindPowerups()
        {
            BindInterfacesTo<PowerupsController>();
            Bind<PowerupsPicker>();
        }

        private void BindSettings()
        {
            BindInterfacesFromInstance(_playerConfig);
        }

        private void BindShooting()
        {
            BindFromInstance(_playerShooting);
        }
    }
}
