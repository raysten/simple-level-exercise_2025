namespace Player
{
    public interface IFlyingInput
    {
        bool IsFlyPressed { get; }
        float FlyUpDownInput { get; }
    }
}
