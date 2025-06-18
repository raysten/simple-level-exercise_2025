using UnityEngine;

namespace Platforms
{
    public interface IMovingPlatform
    {
        Vector3 Velocity { get; }
    }
}
