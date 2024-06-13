using System.Linq;

namespace BowlingGame;

public class Game
{
    private readonly IFrame[] _frames = new IFrame[10];
    private IFrame _currentFrame;

    public Game()
    {
        Frame nextFrame = new LastFrame();
        Frame currentFrame = nextFrame;
        _frames[9] = nextFrame;
        for (int i = 8; i >= 0; --i)
        {
            currentFrame = new NormalFrame(nextFrame);
            _frames[i] = currentFrame;
            nextFrame = currentFrame;
        }

        _currentFrame = currentFrame;
    }

    public void Roll(int pins)
    {
        _currentFrame = _currentFrame.Roll(pins);
    }

    public int Score()
    {
        return _frames.Sum(f => f.Score());
    }
}