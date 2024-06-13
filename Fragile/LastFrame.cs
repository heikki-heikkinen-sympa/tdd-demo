namespace BowlingGame;

public class LastFrame : IFrame
{
    private readonly int[] _throws = new int[3];
    private int _throwIndex = 0;

    public void Roll(int pins)
    {
        _throws[_throwIndex++] = pins;
    }

    public int Score()
    {
        return _throws[0] + _throws[1] + _throws[2];
    }

    public bool IsCompleted()
    {
        if (!IsStrike() && !IsSpare())
        {
            return _throwIndex > 1;
        }

        return _throwIndex > 2;
    }

    public bool IsStrike()
    {
        return _throws[0] == 10;
    }

    public bool IsSpare()
    {
        return !IsStrike() && _throws[0] + _throws[1] == 10;
    }
}