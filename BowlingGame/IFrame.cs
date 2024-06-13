namespace BowlingGame;

public interface IFrame
{
    IFrame Roll(int pins);

    int Score();
}