namespace BowlingGame;

public interface IFrame
{
    void Roll(int pins);

    int Score();

    bool IsCompleted();

    bool IsStrike();

    bool IsSpare();
}