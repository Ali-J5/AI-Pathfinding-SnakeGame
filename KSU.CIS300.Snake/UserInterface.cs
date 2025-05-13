/*
 * UserInterface.cs
 * Author: Josh Weese
 * Modified by: Ali Jebril
 * CIS300
 * Homework 5
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSU.CIS300.Snake
{
    /// <summary>
    /// UI for Snake Game
    /// </summary>
    public partial class UserInterface : Form
    {
       
        public UserInterface()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }
        /// <summary>
        /// This is the calculated size of a game square.
        /// </summary>
        private int _squareWidth;
        /// <summary>
        /// The width and height of the game in number of nodes/game squares.
        /// </summary>
        private int _size;
        /// <summary>
        /// Gives the UI access to informing the game.
        /// </summary>
        private Game _game;
        /// <summary>
        /// This will be used to give the snake color.
        /// </summary>
        private SolidBrush _bodyBrush = new(Color.Cyan);
        /// <summary>
        /// This will be used to give the food color.
        /// </summary>
        private SolidBrush _foodBrush = new(Color.Yellow);
        /// <summary>
        /// gives each snake square an outline.
        /// </summary>
        private Pen _pen = new(Color.Blue, 2);
        /// <summary>
        /// Allows the UserInterface to cancel or stop the async StartMoving.
        /// </summary>
        private CancellationTokenSource _cancelSource = new();
        /// <summary>
        /// Creates a new game with the given size and speed.
        /// </summary>
        /// <param name="size">Size of game board</param>
        /// <param name="speed">Snake speed</param>
        private void NewGame(int size, int speed)
        {
            _cancelSource.Cancel();
            _cancelSource = new CancellationTokenSource();

            _size = size;
            _game = new(size,speed, uxIsAI.Checked);
            uxPictureBox.Width = 600;
            uxPictureBox.Height = 600;

            this.Size = new(uxPictureBox.Width+25, uxPictureBox.Height + uxMenuStrip.Height+55);
            _squareWidth = uxPictureBox.Width / size;

            uxScore.DataBindings.Clear();
            uxScore.DataBindings.Add("Text", _game, "Score");

            Progress<SnakeStatus> progress = new();
            _cancelSource = new();
            progress.ProgressChanged += new EventHandler<SnakeStatus>(CheckProgress);
            _game.StartMoving(progress, _cancelSource.Token);

        }
        /// <summary>
        /// Checks and prompts Progress.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="status">Snake Status</param>
        private void CheckProgress(object sender, SnakeStatus status)
        {
            //Note that the Refresh call invalidates the state of the controls, forcing them to be redrawn.
            Refresh();
            if (status == SnakeStatus.Collision)
            {
                MessageBox.Show("Game over!");
            }
            else if (status == SnakeStatus.Win)
            {
                MessageBox.Show("Game Completed!");
            }
        }
        /// <summary>
        ///  Draws all of the game graphics.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (_game == null)
            {
                return;
            }

            Graphics graphics = e.Graphics;
            foreach (GameNode node in _game.GetSnakePath())
            {
                Rectangle rectangle = new(node.X*_squareWidth,node.Y*_squareWidth,_squareWidth,_squareWidth);
                graphics.FillRectangle(_bodyBrush, rectangle);
                graphics.DrawRectangle(_pen, rectangle);
            }
            GameNode food = _game.GetFood();
            if (food != null)
            {
                Rectangle rectangle = new(food.X * _squareWidth, food.Y * _squareWidth, _squareWidth, _squareWidth);
                graphics.FillEllipse(_foodBrush,rectangle);
                graphics.DrawEllipse(_pen, rectangle);
            }
        }

        /// <summary>
        /// Key Press Handler
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private void UserInterface_KeyDown(object sender, KeyEventArgs e)
        {
            if (_game != null && _game.Play && !uxIsAI.Checked)
            {
                if (e.KeyCode == Keys.Up)
                {
                    _game.MoveUp();
                }
                if (e.KeyCode == Keys.Down)
                {
                    _game.MoveDown();
                }
                if (e.KeyCode == Keys.Left)
                {
                    _game.MoveLeft();
                }
                if (e.KeyCode == Keys.Right)
                {
                    _game.MoveRight();
                }
                uxPictureBox.Refresh();
            }
        }
        /// <summary>
        /// Easy Mode
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private void EasyGame_Click(object sender, EventArgs e)
        {
            if (uxIsAI.Checked)
            {
                NewGame(10, (int)uxAIspeed.Value);

            }
            else
            {
                NewGame(10, 250);

            }
        }
        /// <summary>
        /// Normal Mode
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private void NormalGame_Click(object sender, EventArgs e)
        {
            if (uxIsAI.Checked)
            {
                NewGame(20, (int)uxAIspeed.Value);

            }
            else
            {
                NewGame(20,150);

            }
        }
        /// <summary>
        /// Hard Move
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private void HardGame_Click(object sender, EventArgs e)
        {
            if (uxIsAI.Checked)
            {
                NewGame(30, (int)uxAIspeed.Value);

            }
            else
            {
                NewGame(30,100);

            }
        }

        /// <summary>
        /// Hooking keys
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private void UserInterface_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }
    }
}
