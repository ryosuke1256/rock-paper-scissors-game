using System;

namespace Source {
  class Client {
    private static void Main(string[] args) {
      Console.WriteLine("Let's start a rock-paper-scissors game.");
      Console.WriteLine();
      Console.WriteLine("Please choose a number from the following options.");
      Console.WriteLine("1. Rock, 2. Paper, 3. Scissors");
      Console.Write("Your choice is : ");
      var input = Console.ReadLine();
      if(!int.TryParse(input, out int choice) || !(1 <= choice && choice <= 3)) {
        throw new InvalidOperationException("Invalid input. Please enter a valid number (1, 2, or 3).");
      }
      var game = new RockPaperScissorsGame();
      game.GenerateRandomChoice();
      Result result = game.Judge(userChoice: choice - 1);
      DisplayResult(result: result, opponentChoice: game.getChoice() + 1);
    }

    private static void DisplayResult(Result result, int opponentChoice) {
      Console.WriteLine($"Opponent choice is : {opponentChoice}");
      switch(result) {
        case Result.Win:
          Console.WriteLine("You win!");
          break;
        case Result.Lose:
          Console.WriteLine("You lose...");
          break;
        case Result.Draw:
          Console.WriteLine("It's a draw");
          break;
        default:
          throw new InvalidProgramException("Unexpected result");
      }
    }
  }

  enum Choices { Rock, Paper, Scissors }

  enum Result { Win, Lose, Draw }

  sealed class RockPaperScissorsGame {
    private readonly Random _random;
    private int? _choice = null;

    public RockPaperScissorsGame() {
      _random = new Random();
    }

    public void GenerateRandomChoice() {
      _choice = _random.Next(0,3);
    }

    public int getChoice() {
      if(_choice == null) throw new InvalidOperationException("Opponent choice is null");
      return (int) _choice;
    }

    public Result Judge(int userChoice) {
      if(_choice == null) throw new InvalidOperationException("Opponent choice is null");

      if(userChoice == _choice) return Result.Draw;

      if((userChoice == 0 && _choice == 2) ||
        (userChoice == 1 && _choice == 0) ||
        (userChoice == 2 && _choice == 1)) {
        return Result.Win;
      }

      if((userChoice == 0 && _choice == 1) ||
        (userChoice == 1 && _choice == 2) ||
        (userChoice == 2 && _choice == 0)) {
        return Result.Lose;
      };

      throw new InvalidOperationException("Invalid operation");
    }
  }
}