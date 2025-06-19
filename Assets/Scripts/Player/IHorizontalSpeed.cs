namespace Player
{
    public interface IHorizontalSpeed
    {
        float CalculateGroundedSpeed();
        float CalculateSpeedWhenFalling();
    }
}
