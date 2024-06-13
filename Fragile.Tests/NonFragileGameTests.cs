using FluentAssertions;
using Xunit;

namespace BowlingGame.Tests;

public class NonFragileGameTests : BowlingGameTestBase
{
    [Fact]
    public void After_the_first_roll_game_score_is_the_number_of_pins_knocked_down()
    {
        // Arrange
        var game = NewGame();

        // Act
        game.Roll(7);

        // Assert
        game.Score().Should().Be(7);
    }

    [Fact]
    public void After_spare_the_bonus_score_is_the_number_of_pins_knocked_down_in_the_next_frame()
    {
        // Arrange
        var game = NewGame();

        // Act
        RollFrame(game,3, 7);

        game.Roll(7);

        // Assert
        game.Score().Should().Be(10 + 7 + 7);

    }

    [Fact]
    public void After_strike_the_bonus_score_is_the_number_of_pins_knocked_down_in_the_next_two_frames()
    {
        // Arrange
        var game = NewGame();

        // Act
        RollStrike(game);

        RollFrame(game, 4, 0);

        game.Roll(7);

        // Assert
        game.Score().Should().Be(10 + 4 + 7 + 4 + 7);
    }
}