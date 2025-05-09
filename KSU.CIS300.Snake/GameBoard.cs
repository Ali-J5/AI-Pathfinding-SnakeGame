using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            SnakeSize = 1;
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
            if (dir == Direction.Up && y-1 >= 0)
            {
                y--;
            }
            else if (dir == Direction.Down && y+1<_size)
            {
                y++;
            }
            else if (dir == Direction.Left && x-1 >= 0)
            {
                x--;
            }
            else if (dir == Direction.Right && x+1 <_size)
            {
                x++;
            }
            else
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
            if (next.SnakeEdge == Head)
            {
                return SnakeStatus.InvalidDirection;
            }
            if (next.Data == GridData.SnakeBody)
            {
                return SnakeStatus.Collision;
            }

            GridData data = next.Data;
            next.Data = GridData.SnakeHead;
            Head.Data = GridData.SnakeBody;
            Head.SnakeEdge = next;
            if (data == GridData.SnakeFood)
            {
                Head = next;
                SnakeSize++;
                if (SnakeSize == Grid.Length)
                {
                    return SnakeStatus.Win;
                }
                AddFood();
                return SnakeStatus.Eating;
            }
            else
            {
                if (Head != Tail)
                {
                    Tail.Data = GridData.Empty;
                    GameNode temp = Tail.SnakeEdge;
                    Tail.SnakeEdge = null;
                    Tail = temp;
                }
                else
                {
                    SnakeSize++;
                }
                Head = next;
                return SnakeStatus.Moving;
            }
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
        /// <summary>
        /// Reverses the given path from the destination to the head of the snake.
        /// </summary>
        /// <param name="path">Dictionary of paths</param>
        /// <param name="dest">Destination</param>
        /// <returns>List of directions of path</returns>
        private List<Direction> BuildPath(Dictionary<GameNode, (GameNode, Direction)> path, GameNode dest)              // ASK HERE IF THIS METHOD WORKS
        {
            List<Direction> directions = new();
            Stack<Direction> stack = new Stack<Direction>();
            while (dest != Head)
            {
                stack.Push(path[dest].Item2);
                dest = path[dest].Item1;
            }
            while (stack.Count > 0)
            {
                directions.Add(stack.Pop());
            }
            return directions;
        }
        /// <summary>
        /// Custom method for finding adjacent 'Edges'.
        /// </summary>
        /// <param name="source">Source node</param>
        /// <param name="isTail">If the destination is the Tail</param>
        /// <returns>List of adjacent edges</returns>
        public List<(GameNode, GameNode, Direction)> AdjacentEdges(GameNode source, bool isTail)
        {
            List<(GameNode, GameNode, Direction)> adj = new();
            GameNode next;
            foreach (Direction dir in _aiDirection)
            {
                next = GetNextNode(dir, source);
                if (next != null && ((isTail && next == Tail && Tail.SnakeEdge != source) || next.Data != GridData.SnakeBody) && next.Data != GridData.SnakeHead)
                {
                    adj.Add((source, next, dir));
                }
            }
            return adj;
        }
        /// <summary>
        /// Calculates the shortest path from the head of the snake to the destination.
        /// </summary>
        /// <param name="dest">Destintation node</param>
        /// <returns>Shortest path</returns>
        public List<Direction> FindShortestAiPath(GameNode dest)
        {
            bool isTail = Tail == dest;
            Dictionary<GameNode, (GameNode, Direction)> paths = new();
            paths[Head] = (Head, Direction.None);
            Queue<(GameNode, GameNode, Direction)> queue = new();
            foreach ((GameNode, GameNode, Direction) tup in AdjacentEdges(Head, isTail))
            {
                queue.Enqueue(tup);
            }
            while (queue.Count > 0)
            {
                (GameNode, GameNode, Direction) t = queue.Dequeue();
                GameNode d = t.Item2;
                if (!paths.ContainsKey(d))
                {
                    paths[d] = (t.Item1, t.Item3);
                    if (t.Item2 == dest)
                    {
                        return BuildPath(paths, dest);
                    }
                    foreach ((GameNode, GameNode, Direction) tup2 in AdjacentEdges(t.Item2, isTail))
                    {
                        queue.Enqueue(tup2);
                    }
                }
            }
            return new();
        }
        /// <summary>
        /// Used to find the Hamiltonian path.
        /// </summary>
        /// <returns>Longest Path</returns>
        public Queue<Direction> FindLongestAiPath()     // Extremely stuck here
        {
            List<Direction> path = FindShortestAiPath(Tail);
            if (path.Count == 0)
            {
                return null;
            }
            bool[,] visited = new bool[_size, _size];
            Direction[] exitDir = null;
            GameNode current = Head;
            visited[current.X, current.Y] = true;
            foreach (Direction dir in path)
            {
                current = GetNextNode(dir, current);
                visited[current.X, current.Y] = true;
            }
            int index = 0;
            GameNode nextNode;
            current = Head;
            while (index < path.Count)
            {
                Direction currentDir = path[index];
                nextNode = GetNextNode(currentDir, current);
                if (currentDir == Direction.Left || currentDir == Direction.Right)
                {
                    exitDir = _upDown;
                }
                else
                {
                    exitDir = _leftRight;
                }

                bool extend = false;
                foreach (Direction dir in exitDir)
                {
                    GameNode currentExit = GetNextNode(dir, current);
                    GameNode nextExit = GetNextNode(dir, nextNode);
                    if (currentExit != null && nextExit != null &&
                        !visited[currentExit.X, currentExit.Y] &&
                        !visited[nextExit.X, nextExit.Y])
                    {
                        visited[currentExit.X, currentExit.Y] = true;
                        visited[nextExit.X, nextExit.Y] = true;
                        path.Insert(index, dir);
                        path.Insert(index + 2, OppositeDir(dir));
                        extend = true;
                        break;
                    }
                }
                if (!extend)
                {
                    current = nextNode;
                    index++;
                }
            }
            while(current != Head)
            {
                if (current.SnakeEdge.X - current.X == 0)
                {
                    if (current.SnakeEdge.Y - current.Y == -1)
                    {
                        path.Add(Direction.Up);
                    }
                    else
                    {
                        path.Add(Direction.Down);
                    }
                }
                else
                {
                    if (current.SnakeEdge.X - current.X == -1)
                    {
                        path.Add(Direction.Right);
                    }
                    else
                    {
                        path.Add(Direction.Left);
                    }
                }
                current = current.SnakeEdge;
            }
            Queue<Direction> queue = new Queue<Direction>();
            foreach(Direction dir in path)
            {
                queue.Enqueue(dir);
            }
            return queue;


        }
        /// <summary>
        /// Finds the opposite direction
        /// </summary>
        /// <param name="dir">Direction</param>
        /// <returns>Opposite</returns>
        private Direction OppositeDir(Direction dir)
        {
            if (dir == Direction.Left)
            {
                return Direction.Right;
            }
            if (dir == Direction.Right)
            {
                return Direction.Left;
            }
            if (dir == Direction.Down)
            {
                return Direction.Up;
            }
            
            return Direction.Down;
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
