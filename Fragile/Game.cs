namespace BowlingGame;

public class Game
{
    private readonly IFrameFactory _frameFactory;
    private readonly IGameFrames _gameFrames;


    public Game(IFrameFactory frameFactory, IGameFrames gameFrames)
    {
        _frameFactory = frameFactory;
        _gameFrames = gameFrames;
    }

    public void Roll(int pins)
    {
        var round = _gameFrames.CurrentRound();
        var frame = _gameFrames.GetFrame(round);
        if (frame is null)
        {
            frame = _frameFactory.CreateFrame(round);
            _gameFrames.Add(frame);
        }

        frame.Roll(pins);

        if (frame.IsCompleted())
        {
            _gameFrames.NextRound();
        }
    }

    public int Score()
    {
        var totalScore = 0;
        var currentRound = _gameFrames.CurrentRound();
        for (int round = 1; round <= currentRound; ++round)
        {
            var frame = _gameFrames.GetFrame(round);
            if (frame is null)
            {
                break;
            }

            totalScore += frame.Score();

            if (frame.IsSpare() && HasNextRound(round))
            {
                var nextFrame = _gameFrames.GetFrame(round + 1);
                totalScore += nextFrame?.Score() ?? 0;
            }

            if (frame.IsStrike() && HasNextRound(round + 1))
            {
                var nextFrame = _gameFrames.GetFrame(round + 1);
                totalScore += nextFrame?.Score() ?? 0;
                var secondNextFrame = _gameFrames.GetFrame(round + 2);
                totalScore += secondNextFrame?.Score() ?? 0;
            }
        }

        return totalScore;
    }

    private bool HasNextRound(int round) => _gameFrames.HasNextRound(round);
}