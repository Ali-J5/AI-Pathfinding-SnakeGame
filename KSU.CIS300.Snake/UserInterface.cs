/*
 * Author: Josh Weese
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSU.CIS300.Snake
{

    public partial class UserInterface : Form
    {
       
        public UserInterface()
        {
            InitializeComponent();
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

        private void NewGame(int size, int speed)
        {
            _game = new(size,speed, uxIsAI.Checked);
            uxPictureBox.Width = 600;
            uxPictureBox.Height = 600;
            UserInterface.size = 

        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {

        }


        private void UserInterface_KeyDown(object sender, KeyEventArgs e)
        {

        }

 
        private void EasyGame_Click(object sender, EventArgs e)
        {
        }


        private void NormalGame_Click(object sender, EventArgs e)
        {
        }

        private void HardGame_Click(object sender, EventArgs e)
        {
        }


        private void UserInterface_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }
    }
}
