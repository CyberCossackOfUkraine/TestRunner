public interface IControlStrategy
{
    void HandleInput();

    bool Left();

    bool Right();

    bool Up();

    bool Down();
}
