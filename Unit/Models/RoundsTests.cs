using System;
using System.Collections.Generic;
using Gimma.Models;
using Xunit;

namespace Unit.Models;

public class RoundsTests
{
    [Fact]
    public void StartNextRound_ChangesRound()
    {
        var round1 = new Round("blabla1", new List<string> { "player-1" });
        var round2 = new Round("blabla2", new List<string> { "player-1" });
        var rounds = new Rounds(new List<Round> { round1, round2 });

        Assert.Equal(round1, rounds.GetCurrentRound());
        rounds.StartNextRound();
        Assert.Equal(round2, rounds.GetCurrentRound());
    }

    [Fact]
    public void StartNextRound_CantAdvanceToNonExistingRound()
    {
        var round = new Round("blabla", new List<string> { "player-1" });
        var rounds = new Rounds(new List<Round> { round });

        Assert.Throws<Exception>(() => rounds.StartNextRound());
    }
}