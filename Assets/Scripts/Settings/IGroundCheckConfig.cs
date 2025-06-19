using UnityEngine;

namespace DependencyInjection
{
    public interface IGroundCheckConfig
    {
        LayerMask GroundCheckLayerMask { get; }
        float GroundCheckOffsetIntoCapsule { get; }
        float GroundDistance { get; }
    }
}
