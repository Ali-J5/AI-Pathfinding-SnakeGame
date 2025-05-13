/* GameNode.cs
 * Author: Ali Jebril
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSU.CIS300.Snake
{
    /// <summary>
    ///  Serves as a node in the graph that represents the game board.
    /// </summary>
    public class GameNode
    {
        /// <summary>
        /// The y-coordinate for this node.
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// The x-coordinate for this node.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// The information stored at this node.
        /// </summary>
        public GridData Data { get; set; }
        /// <summary>
        /// This edge represents a connection in the graph to another GameNode.
        /// </summary>
        public GameNode SnakeEdge { get; set; }
        public GameNode(int x , int y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Overriding ToString.
        /// </summary>
        /// <returns>The x,y coordinate with the data stored.</returns>
        public override string ToString()
        {
            return $"x:{X} & y:{Y} with Data:{Data}";
        }
    }
    /// <summary>
    /// Each node is only allowed to have a single piece of the game at any time.
    /// </summary>
    public enum GridData
    {
        Empty,
        SnakeHead,
        SnakeBody,
        SnakeFood
    }
}
