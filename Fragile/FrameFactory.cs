namespace BowlingGame;

public class FrameFactory : IFrameFactory
{
    public IFrame CreateFrame(int round)
    {
        return round < 10 ? new Frame() : new LastFrame();
    }
}