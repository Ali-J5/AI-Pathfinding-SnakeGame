using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KSU.CIS300.Snake
{
    /// <summary>
    /// The communication between the UI and the game logic.
    /// </summary>
    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Keeps track of how many points the player has.
        /// </summary>
        private int _score;
        /// <summary>
        /// Indicates how many milliseconds the game should wait before ticks (controls how fast the snake moves).
        /// </summary>
        private int _delay;

        /// <summary>
        /// Indicates if the game should be controlled by the AI.
        /// </summary>
        private bool _isAI;
        /// <summary>
        /// This will store the AI path, if the AI is enabled.
        /// </summary>
        private Queue<Direction> _aiPath;
        /// <summary>
        /// Stores whether or not the game is currently being played (i.e. the game is not over).
        /// </summary>
        public bool Play;
        /// <summary>
        /// Triggers the data binding process.
        /// </summary>
        public int Score
        {
            get => _score;
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged("Score");              //check here
                }
            }
        }
        /// <summary>
        /// The reference to the game board object that contains the logic for moving the snake on the graph.
        /// </summary>
        public GameBoard Board { get; private set; }
        /// <summary>
        /// The size of the game to create.
        /// </summary>
        public int Size { get; private set; }
        /// <summary>
        /// The last direction that the snake successfully moved.
        /// </summary>
        public Direction LastDirection { get; set; }
        /// <summary>
        /// The most recent direction reported by the UI.
        /// </summary>
        public Direction KeyPress { get; private set; }
        /// <summary>
        /// The current status of the snake.
        /// </summary>
        public SnakeStatus Status { get; private set; }

        public Game(int size, int speed, bool isAI)
        {
            _isAI = isAI;
            Size = size;
            _score = 2;                           //ask if its right score
            Play = true;
            _delay = speed;

            Board = new(Size);
            Board.MoveSnake(Direction.Up);
            if (_isAI)
            {
                _aiPath = Board.FindLongestAiPath(); //ask here if its right method
            }
        }
        /// <summary>
        /// Acts as a game clock.
        /// </summary>
        /// <param name="progress">Progress done</param>
        /// <param name="cancelToken">UI Request</param>
        /// <returns>Task</returns>
        public async Task StartMoving(IProgress<SnakeStatus> progress, CancellationToken cancelToken)
        {
            while (Play && !cancelToken.IsCancellationRequested) // Correct format?
            {
                Status = Board.MoveSnake(KeyPress);
                progress.Report(Status);
                if (Status == SnakeStatus.Collision)
                {
                    Play = false;
                }
                if (Status == SnakeStatus.Moving)
                {
                    LastDirection = KeyPress;
                }
                if (Status == SnakeStatus.Eating)
                {
                    Score++;
                }
                if (Status == SnakeStatus.InvalidDirection)
                {
                    Board.MoveSnake(LastDirection);
                    if (Status == SnakeStatus.Collision)
                    {
                        Play = false;
                    }
                    if (Status == SnakeStatus.Eating)
                    {
                        Score++;
                    }
                }
                if (Status == SnakeStatus.Win)
                {
                    Score++;
                    Play = false;
                }
                await Task.Delay(_delay);
            }
        }
        /// <summary>
        /// What will call the property changed event with the appropriate property.
        /// </summary>
        /// <param name="propertyName">String name</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Result of the GetSnakePath method contained in the GameBoard class.
        /// </summary>
        /// <returns>List of path</returns>
        public List<GameNode> GetSnakePath()
        {
            return Board.GetSnakePath();
        }
        /// <summary>
        /// Food property of the game board.
        /// </summary>
        /// <returns>Food Property</returns>
        public GameNode GetFood()
        {
            if (Board.Food == null)
            {
                return null;
            }
            return Board.Food;
        }
        /// <summary>
        /// Sets the key press field to be the up direction.
        /// </summary>
        public void MoveUp()
        {
            KeyPress = Direction.Up;
        }
        /// <summary>
        /// Sets the key press field to be the down direction.
        /// </summary>
        public void MoveDown()
        {
            KeyPress = Direction.Down;
        }
        /// <summary>
        /// Sets the key press field to be the left direction.
        /// </summary>
        public void MoveLeft()
        {
            KeyPress = Direction.Left;
        }
        /// <summary>
        /// Sets the key press field to be the right direction.
        /// </summary>
        public void MoveRight()
        {
            KeyPress = Direction.Right;
        }
    }
}
