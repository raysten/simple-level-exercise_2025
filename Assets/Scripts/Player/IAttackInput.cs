namespace Player
{
    public interface IAttackInput
    {
        bool IsAttackPressed { get; }
        bool IsAlternativeAttackPressed { get; }
    }
}
