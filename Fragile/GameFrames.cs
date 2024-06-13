using System.Collections.Generic;

namespace BowlingGame;

public class GameFrames : IGameFrames
{
    private readonly List<IFrame> _frames = new(10);
    private int _round = 1;

    public void Add(IFrame frame)
    {
        _frames.Add(frame);
    }

    public IFrame GetFrame(int round)
    {
        return _frames.Count < round ? null : _frames[round - 1];
    }

    public bool HasNextRound(int round)
    {
        return round < _frames.Count;
    }

    public int CurrentRound()
    {
        return _round;
    }

    public void NextRound()
    {
        ++_round;
    }
}