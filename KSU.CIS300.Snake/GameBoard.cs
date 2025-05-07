using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSU.CIS300.Snake
{
    /// <summary>
    /// Responsible for making the snake grow or move into a new place on the board.
    /// </summary>
    public class GameBoard
    {
        /// <summary>
        /// This will return the game node that contains the food.
        /// </summary>
        public GameNode Food { get; set; }
        /// <summary>
        /// The array for storing the nodes of the game board.
        /// </summary>
        public GameNode[,] Grid { get; private set; }
        /// <summary>
        /// Maintains a reference to where the head of the snake is currently located.
        /// </summary>
        public GameNode Head { get; set; }
        /// <summary>
        /// Maintains a reference to where the tail of the snake is currently located.
        /// </summary>
        public GameNode Tail { get; set; }
        /// <summary>
        /// Keeps track of how big the snake is at any given time.
        /// </summary>
        public int SnakeSize { get; private set; }

        /// <summary>
        /// Keeps track of the dimension (n) of the board.
        /// </summary>
        private int _size;
        /// <summary>
        /// This array contains all four possible directions when finding the shortest path in the board.
        /// </summary>
        private Direction[] _aiDirection = { Direction.Up, Direction.Left, Direction.Right, Direction.Down };

        /// <summary>
        /// Array of left and right for AI when calculating the Hamiltonian path
        /// </summary>
        private Direction[] _leftRight = { Direction.Left, Direction.Right };

        /// <summary>
        /// Array of left and right for AI when calculating the Hamiltonian path.
        /// </summary>
        private Direction[] _upDown = { Direction.Up, Direction.Down };
        /// <summary>
        /// Used to place the food in a random location.
        /// </summary>
        private static Random _random = new Random();
    }
    /// <summary>
    /// Possible Directions.
    /// </summary>
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        None
    }
}
