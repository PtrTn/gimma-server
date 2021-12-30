using System;
using System.Collections.Generic;
using Gimma.Models;
using Xunit;

namespace Unit.Models;

public class RoundTests
{
    [Fact]
    public void SubmitAnswer_PlayerCanSubmit()
    {
        var round = new Round("blabla", new List<string> { "player-1" });
        round.SubmitAnswer(new Submission("player-1", "image-1"));

        Assert.Single(round.GetSubmissions());
    }
    
    [Fact]
    public void SubmitAnswer_PlayerShouldNotBeAbleToSubmitTwice()
    {
        var round = new Round("blabla", new List<string> { "player-1" });
        round.SubmitAnswer(new Submission("player-1", "image-1"));
        
        Assert.Throws<Exception>(() => round.SubmitAnswer(new Submission("player-1", "image-1")));
    }

    [Fact]
    public void IsComplete_IsTrueOnceAllPlayersHaveSubmitted()
    {
        var round = new Round("blabla", new List<string> { "player-1" });
        Assert.False(round.IsComplete());
        
        round.SubmitAnswer(new Submission("player-1", "image-1"));
        Assert.True(round.IsComplete());
    }
}