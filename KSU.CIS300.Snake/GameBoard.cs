using System;
using System.Collections.Generic;
using System.IO;
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

        public GameBoard(int size)
        {
            _size = size;
            Grid = new GameNode[size, size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Grid[x, y] = new GameNode(x, y);
                }
            }
            int center = size / 2;
            GameNode centerNode = Grid[center, center];
            centerNode.Data = GridData.SnakeHead;
            Head = centerNode;
            Tail = centerNode;
            AddFood();
        }
        /// <summary>
        /// Randomly places the snake food on the board.
        /// </summary>
        public void AddFood()
        {
            bool loop = true;
            while (loop)
            {
                int x = _random.Next(_size);
                int y = _random.Next(_size);
                GameNode rand = Grid[x, y];
                if (rand.Data == GridData.Empty)
                {
                    rand.Data = GridData.SnakeFood;
                    Food = rand;
                    loop = false;
                }
            }

        }
        /// <summary>
        ///  Returns the node that would be next from current if were headed in the given direction.
        /// </summary>
        /// <param name="dir">Direction</param>
        /// <param name="current">Current Node</param>
        /// <returns>Next Node</returns>
        public GameNode GetNextNode(Direction dir, GameNode current)
        {
            int x = current.X;
            int y = current.Y;
            if (dir == Direction.Up)
            {
                y++;
            }
            else if (dir == Direction.Down)
            {
                y--;
            }
            else if (dir == Direction.Left)
            {
                x--;
            }
            else if (dir == Direction.Right)
            {
                x++;
            }
            else
            {
                return null;
            }

            if (x < 0 || y < 0 || x >= _size || y >= _size)
            {
                return null;
            }
            return Grid[x, y];
        }
        /// <summary>
        /// Main logic on how the snake moves through the game board.
        /// </summary>
        /// <param name="dir">Direction</param>
        /// <returns>Snake Status</returns>
        public SnakeStatus MoveSnake(Direction dir)
        {
            GameNode next = GetNextNode(dir, Head);
            if (next == null)
            {
                return SnakeStatus.Collision;
            }
            if (Head.SnakeEdge != null && next == Head.SnakeEdge)
            {
                return SnakeStatus.InvalidDirection;
            }
            if (next.Data == GridData.SnakeBody)
            {
                return SnakeStatus.Collision;
            }

            Head.Data = GridData.SnakeBody;
            Head.SnakeEdge = next;
            Head = next;
            Head.Data = GridData.SnakeHead;
            if (next.Data == GridData.SnakeFood)
            {
                SnakeSize++;
                if (SnakeSize == _size * _size)
                {
                    return SnakeStatus.Win;
                }
                AddFood();
                return SnakeStatus.Eating;
            }
            if (Head != Tail)
            {
                Tail.Data = GridData.Empty;
                GameNode temp = Tail.SnakeEdge;
                Tail.SnakeEdge = null;
                Tail = temp;
            }
            return SnakeStatus.Moving;
        }
        /// <summary>
        /// List of game nodes that contain the snake starting from the tail.
        /// </summary>
        /// <returns>List of game nodes</returns>
        public List<GameNode> GetSnakePath()
        {
            List<GameNode> snake = new();
            GameNode current = Tail;
            while (current!= null)
            {
                snake.Add(current);
                current = current.SnakeEdge;
            }
            return snake;
        }
        private List<Direction> BuildPath(Dictionary<GameNode, (GameNode, Direction)> path, GameNode dest)
        {

        }
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
    /// <summary>
    /// Possible Statuses.
    /// </summary>
    public enum SnakeStatus
    {
        Moving,
        InvalidDirection,
        Eating,
        Collision,
        Win
    }
}
