namespace BowlingGame;

public class Frame  : IFrame
{
    private readonly int[] _throws = new int[2];
    private int _throwIndex = 0;

    public void Roll(int pins)
    {
        _throws[_throwIndex++] = pins;
    }

    public int Score()
    {
        return _throws[0] + _throws[1];
    }

    public bool IsCompleted()
    {
        return IsStrike() || _throwIndex > 1;
    }

    public bool IsStrike()
    {
        return _throws[0] == 10;
    }

    public bool IsSpare()
    {
        return !IsStrike() && Score() == 10;
    }
}