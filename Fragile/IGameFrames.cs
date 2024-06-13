namespace BowlingGame;

public interface IGameFrames
{
    void Add(IFrame frame);

    IFrame GetFrame(int round);

    bool HasNextRound(int round);

    int CurrentRound();

    void NextRound();
}