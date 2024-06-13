using FluentAssertions;
using Moq;
using Xunit;

namespace BowlingGame.Tests;

public class FragileGameTests
{
    private readonly Mock<IFrameFactory> _frameFactory;
    private readonly Mock<IGameFrames> _gameFrames;

    public FragileGameTests()
    {
        _gameFrames = new Mock<IGameFrames>();
        _frameFactory = new Mock<IFrameFactory>();

    }

    [Fact]
    public void After_the_first_roll_game_score_is_the_number_of_pins_knocked_down()
    {
        // Arrange
        _gameFrames.Setup(m => m.CurrentRound()).Returns(1);
        var frame = new Mock<IFrame>();
        _frameFactory.Setup(m => m.CreateFrame(1)).Returns(frame.Object);
        _gameFrames.SetupSequence(m => m.GetFrame(1))
            .Returns((IFrame)null)
            .Returns(frame.Object);


        frame.Setup(m => m.IsCompleted()).Returns(false);
        frame.Setup(m => m.IsSpare()).Returns(false);
        frame.Setup(m => m.IsStrike()).Returns(false);
        frame.Setup(m => m.Score()).Returns(7);

        var game = new Game(_frameFactory.Object, _gameFrames.Object);

        // Act
        game.Roll(7);

        // Assert
        game.Score().Should().Be(7);
        frame.Verify(m => m.Roll(7), Times.Once);
        frame.Verify(m => m.Score(), Times.Once);
        _gameFrames.Verify(m => m.GetFrame(1), Times.Exactly(2));
        _gameFrames.Verify(m => m.Add(frame.Object), Times.Once);
    }

    [Fact]
    public void After_spare_the_bonus_score_is_the_number_of_pins_knocked_down_in_the_next_frame()
    {
        // Arrange
        _gameFrames.Setup(m => m.CurrentRound()).Returns(2);

        var spareFrame = new Mock<IFrame>();
        spareFrame.Setup(m => m.IsSpare()).Returns(true);
        spareFrame.Setup(m => m.Score()).Returns(10);
        _gameFrames.Setup(m => m.GetFrame(1))
            .Returns(spareFrame.Object);

        var nextFrame = new Mock<IFrame>();
        _frameFactory.Setup(m => m.CreateFrame(2)).Returns(nextFrame.Object);
        _gameFrames.SetupSequence(m => m.GetFrame(2))
            .Returns((IFrame)null)
            .Returns(nextFrame.Object)
            .Returns(nextFrame.Object);


        nextFrame.Setup(m => m.IsCompleted()).Returns(false);
        nextFrame.Setup(m => m.IsSpare()).Returns(false);
        nextFrame.Setup(m => m.IsStrike()).Returns(false);
        nextFrame.Setup(m => m.Score()).Returns(7);

        _gameFrames.Setup(m => m.HasNextRound(1)).Returns(true);
        _gameFrames.Setup(m => m.HasNextRound(2)).Returns(false);

        var game = new Game(_frameFactory.Object, _gameFrames.Object);

        // Act
        game.Roll(7);

        // Assert
        game.Score().Should().Be(10 + 7 + 7);
        nextFrame.Verify(m => m.Roll(7), Times.Once);
        nextFrame.Verify(m => m.Score(), Times.Exactly(2));
        _gameFrames.Verify(m => m.GetFrame(1), Times.Once);
        _gameFrames.Verify(m => m.GetFrame(2), Times.Exactly(3));
        _gameFrames.Verify(m => m.Add(nextFrame.Object), Times.Once);
    }

    [Fact]
    public void After_strike_the_bonus_score_is_the_number_of_pins_knocked_down_in_the_next_two_frames()
    {
        // Arrange
        _gameFrames.Setup(m => m.CurrentRound()).Returns(3);

        var strikeFrame = new Mock<IFrame>();
        strikeFrame.Setup(m => m.IsSpare()).Returns(false);
        strikeFrame.Setup(m => m.IsStrike()).Returns(true);
        strikeFrame.Setup(m => m.Score()).Returns(10);
        _gameFrames.Setup(m => m.GetFrame(1))
            .Returns(strikeFrame.Object);

        var nextFrame = new Mock<IFrame>();
        nextFrame.Setup(m => m.Score()).Returns(4);
        _gameFrames.Setup(m => m.GetFrame(2))
            .Returns(nextFrame.Object);

        var secondNextFrame = new Mock<IFrame>();
        _frameFactory.Setup(m => m.CreateFrame(3)).Returns(secondNextFrame.Object);
        _gameFrames.SetupSequence(m => m.GetFrame(3))
            .Returns((IFrame)null)
            .Returns(secondNextFrame.Object)
            .Returns(secondNextFrame.Object);


        secondNextFrame.Setup(m => m.IsCompleted()).Returns(false);
        secondNextFrame.Setup(m => m.IsSpare()).Returns(false);
        secondNextFrame.Setup(m => m.IsStrike()).Returns(false);
        secondNextFrame.Setup(m => m.Score()).Returns(7);

        _gameFrames.Setup(m => m.HasNextRound(1)).Returns(true);
        _gameFrames.Setup(m => m.HasNextRound(2)).Returns(true);
        _gameFrames.Setup(m => m.HasNextRound(3)).Returns(false);

        var game = new Game(_frameFactory.Object, _gameFrames.Object);

        // Act
        game.Roll(7);

        // Assert
        game.Score().Should().Be(10 + 4 + 7 + 4 + 7);
        secondNextFrame.Verify(m => m.Roll(7), Times.Once);
        secondNextFrame.Verify(m => m.Score(), Times.Exactly(2));
        nextFrame.Verify(m => m.Score(), Times.Exactly(2));
        _gameFrames.Verify(m => m.GetFrame(1), Times.Once);
        _gameFrames.Verify(m => m.GetFrame(2), Times.Exactly(2));
        _gameFrames.Verify(m => m.GetFrame(3), Times.Exactly(3));
        _gameFrames.Verify(m => m.Add(secondNextFrame.Object), Times.Once);
    }
}