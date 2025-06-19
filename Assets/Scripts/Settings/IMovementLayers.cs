using UnityEngine;

namespace Settings
{
    public interface IMovementLayers
    {
        LayerMask PlayerMovementCollisionMask { get; }
        LayerMask MovingPlatformLayerMask { get; }
    }
}
