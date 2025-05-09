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
        /// <summary>
        /// Reverses the given path from the destination to the head of the snake.
        /// </summary>
        /// <param name="path">Dictionary of paths</param>
        /// <param name="dest">Destination</param>
        /// <returns>List of directions of path</returns>
        private List<Direction> BuildPath(Dictionary<GameNode, (GameNode, Direction)> path, GameNode dest)              // ASK HERE IF THIS METHOD WORKS
        {
            List<Direction> directions = new();
            while (path[dest].Item1 != Head)
            {
                directions.Add(path[dest].Item2);
                dest = path[dest].Item1;
            }
            directions.Reverse();
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
            List<GameNode> snakeBody = GetSnakePath();

            GameNode nodeUp = GetNextNode(Direction.Up, source);
            if (!snakeBody.Contains(nodeUp) && nodeUp != null && nodeUp.Data != GridData.SnakeHead && nodeUp.Data != GridData.SnakeBody)
            {
                adj.Add((source, nodeUp, Direction.Up));
            }
            if (isTail && nodeUp == Tail)
            {
                adj.Add((source, nodeUp, Direction.Up));
            }
            adj.Add((source, nodeUp, Direction.Up));

            GameNode nodeDown = GetNextNode(Direction.Down, source);
            if (!snakeBody.Contains(nodeDown) && nodeDown != null && nodeDown.Data != GridData.SnakeHead && nodeDown.Data != GridData.SnakeBody)
            {
                adj.Add((source, nodeDown, Direction.Down));
            }
            if (isTail && nodeDown == Tail)
            {
                adj.Add((source, nodeDown, Direction.Up));
            }

            GameNode nodeRight = GetNextNode(Direction.Right, source);
            if (!snakeBody.Contains(nodeRight) && nodeRight != null && nodeRight.Data != GridData.SnakeHead && nodeRight.Data != GridData.SnakeBody)
            {
                adj.Add((source, nodeRight, Direction.Right));
            }
            if (isTail && nodeRight == Tail)
            {
                adj.Add((source, nodeRight, Direction.Up));
            }

            GameNode nodeLeft = GetNextNode(Direction.Left, source);
            if (!snakeBody.Contains(nodeLeft) && nodeLeft != null && nodeLeft.Data != GridData.SnakeHead && nodeLeft.Data != GridData.SnakeBody)
            {
                adj.Add((source, nodeLeft, Direction.Left));
            }
            if (isTail && nodeLeft == Tail)
            {
                adj.Add((source, nodeLeft, Direction.Up));
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
            bool isTail;
            if (dest == Tail)
            {
                isTail = true;
            }
            else
            {
                isTail = false;
            }
            Dictionary<GameNode, (GameNode, Direction)> paths = new();
            paths[Head] = (Head, Direction.None);
            Queue<(GameNode source, GameNode dest, Direction dir)> queue = new();
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
        /// <returns></returns>
        public Queue<Direction> FindLongestAiPath()     // Extremely stuck here
        {
            List<Direction> path = FindShortestAiPath(Tail);
            if (path.Count == 0)
            {
                return new();
            }
            bool[,] visited = new bool[_size, _size];
            GameNode current = Head;
            visited[current.X, current.Y] = true;
            Direction currentDir = Direction.None;
            foreach (Direction dir in path)
            {
                current = GetNextNode(dir, current);
                visited[current.X, current.Y] = true;
                currentDir = dir;
            }
            int index = 0;
            GameNode nextNode;
            Direction tempNextDir;
            List<Direction> build = new();
            while (true)
            {
                //nextNode = GetNextNode(currentDir, current);
                if (currentDir == Direction.Up || currentDir == Direction.Down)
                {
                    tempNextDir = _leftRight[index];
                    nextNode = GetNextNode(tempNextDir, current);
                    if (!visited[nextNode.X, nextNode.Y] && nextNode.X < _size && nextNode.Y < _size && nextNode.X >= 0 && nextNode.Y >= 0)
                    {
                        build.Add(currentDir);
                        if (index == 0)
                        {
                            build.Add(_leftRight[1]);
                        }
                        else
                        {
                            build.Add(_leftRight[0]);
                        }
                    }

                }
                if (currentDir == Direction.Left || currentDir == Direction.Right)
                {
                    tempDir = _upDown[index];
                    nextNode = GetNextNode(tempDir, current);
                    if (!visited[nextNode.X, nextNode.Y])
                    {

                    }
                }
                if (index == 1)
                {
                    current = nextNode;

                }

            }


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
