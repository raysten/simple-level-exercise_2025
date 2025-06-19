using UnityEngine;

namespace Player
{
    public interface IHorizontalInput
    {
        Vector3 HorizontalInput { get; }
        Vector2 HorizontalInputRaw { get; }
    }
}
