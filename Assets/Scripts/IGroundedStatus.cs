public interface IGroundedStatus
{
    bool IsGrounded { get; }
    void ChangeIsGrounded(bool isGrounded);
}
