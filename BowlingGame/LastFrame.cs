using System.Linq;

namespace BowlingGame;

public class LastFrame : Frame
{
    private readonly int[] _throws = new int[3];
    private int _throwIndex = 0;

    public LastFrame() : base(null)
    {
    }

    public override IFrame Roll(int pins)
    {
        _throws[_throwIndex++] = pins;

        if (_throwIndex > 1 || KnockedDownPins() < 10)
        {
            return null;
        }

        return this;
    }

    public override int Score()
    {
        return KnockedDownPins();
    }

    protected internal override int KnockedDownPinsFromNext()
    {
        return 0;
    }

    protected internal override int KnockedDownPins()
    {
        return _throws.Sum();
    }
}