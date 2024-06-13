namespace BowlingGame;

public interface IFrameFactory
{
    IFrame CreateFrame(int round);
}