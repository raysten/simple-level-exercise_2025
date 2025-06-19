using UnityEngine;

namespace Settings
{
    public interface IGroundCheckConfig
    {
        LayerMask GroundCheckLayerMask { get; }
        float GroundCheckOffsetIntoCapsule { get; }
        float GroundDistance { get; }
    }
}
