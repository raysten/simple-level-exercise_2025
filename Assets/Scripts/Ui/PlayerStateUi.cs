using Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace Ui
{
    public class PlayerStateUi : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _textField;
        
        private IPlayerEvents _playerEvents;

        [Inject]
        private void Construct(IPlayerEvents playerEvents)
        {
            _playerEvents = playerEvents;
        }
        
        private void Awake()
        {
            _playerEvents.OnPlayerStateChanged += DisplayPlayerState;
        }

        private void DisplayPlayerState(string playerState)
        {
            _textField.SetText($"Player state: {playerState}");
        }

        private void OnDestroy()
        {
            _playerEvents.OnPlayerStateChanged -= DisplayPlayerState;
        }
    }
}
