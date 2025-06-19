namespace DependencyInjection
{
    public interface IHorizontalSpeedConfig
    {
        float HorizontalMovementSpeed { get; }
        float SprintSpeed { get; }
        float HorizontalSpeedWhenFalling { get; }
    }
}
