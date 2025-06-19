using UnityEngine.InputSystem;

namespace DependencyInjection
{
    public interface IInputAsset
    {
        InputActionAsset InputAsset { get; }
    }
}
