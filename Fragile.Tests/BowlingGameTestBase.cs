namespace BowlingGame.Tests;

public class BowlingGameTestBase
{
    protected Game NewGame()
    {
        return new Game(new FrameFactory(), new GameFrames());
    }

    protected void RollStrike(Game game)
    {
        game.Roll(10);
    }

    protected void RollFrame(Game game, int firstThrowPins, int secondThrowPins)
    {
        game.Roll(firstThrowPins);
        game.Roll(secondThrowPins);
    }
}