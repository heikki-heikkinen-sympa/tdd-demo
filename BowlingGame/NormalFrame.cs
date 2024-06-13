namespace BowlingGame;

public class NormalFrame : Frame
{
    private readonly int[] _throws = new int[2];

    private int _throwIndex = 0;

    public NormalFrame(Frame nextFrame) : base(nextFrame)
    {
    }

    public override IFrame Roll(int pins)
    {
        _throws[_throwIndex++] = pins;
        return IsCompleted() ? NextFrame : this;
    }

    public override int Score()
    {
        var frameScore = KnockedDownPins();
        if (IsSpare())
        {
            return frameScore + KnockedDownPinsFromNext();
        }

        if (IsStrike())
        {
            return frameScore + KnockedDownPinsFromNext() + NextFrame.KnockedDownPinsFromNext();
        }

        return frameScore;
    }

    protected internal override int KnockedDownPinsFromNext()
    {
        return NextFrame?.KnockedDownPins() ?? 0;
    }

    protected internal override int KnockedDownPins()
    {
        return _throws[0] + _throws[1];
    }

    private bool IsCompleted() => _throwIndex > 1 || IsStrike();

    private bool IsSpare() => !IsStrike() && KnockedDownPins() == 10;

    private bool IsStrike() => _throws[0] == 10;
}