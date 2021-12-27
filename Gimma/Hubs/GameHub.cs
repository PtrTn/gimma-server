﻿using Gimma.Models;
using Microsoft.AspNetCore.SignalR;

namespace Gimma.Hubs
{
    public class GameHub : Hub
    {
        private List<Game> _games = new();

        public async Task CreateGame(string userName)
        {
            var host = new Player(userName, Context.ConnectionId);
            var game = new Game("my-game-id", host);
            
            _games.Add(game);
            
            await Clients.Caller.SendAsync("GameCreated", "my-game-id");
        }
        
        public async Task JoinGame(string userName, string gameId)
        {
            var player = new Player(userName, Context.ConnectionId);
            var game = _games.FirstOrDefault(o => o._gameId == gameId);
            if (game == null)
            {
                throw new Exception("Game not found");
            }

            game.Join(player);
            
            await Clients.Caller.SendAsync("GameJoined");
        }
        
        public async Task StartGame()
        {
            var game = _games.FirstOrDefault(g => g._host._connectionId == Context.ConnectionId);
            if (game == null)
            {
                throw new Exception("Game not found");
            }
            
            await Clients.Clients(game._players.Select(o => o._connectionId)).SendAsync("GameStarted");
        }
    }
}