namespace BowlingGame;

public abstract class Frame : IFrame
{
    protected readonly Frame NextFrame;

    public Frame(Frame nextFrame)
    {
        NextFrame = nextFrame;
    }

    public abstract IFrame Roll(int pins);

    public abstract int Score();

    protected internal abstract int KnockedDownPinsFromNext();

    protected internal abstract int KnockedDownPins();
}