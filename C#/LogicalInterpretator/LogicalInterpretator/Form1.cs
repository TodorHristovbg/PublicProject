using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace LogicalInterpretator
{
    public partial class Tree : Form
    {
        Panel panel;


        double xCord = 5;
        double yCord = 100;
        int treeHeight;

        Nodes root;
      


        internal Tree(Nodes Root)
        {

            InitializeComponents();
            root = Root;
            Text = "Binary Tree Viewer";
            Size = new Size(1920, 1080);
            treeHeight = Nodes.height(root);
        }

        private void InitializeComponents()
        {
            panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = Color.Gray;
            Controls.Add(panel);
            panel.AutoScroll = true;

            panel.Paint += new PaintEventHandler(Panel_Paint);
            Resize += new EventHandler(Form_Resize);
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            var scrollOffset = panel.AutoScrollPosition;
            g.TranslateTransform(scrollOffset.X, scrollOffset.Y + 20);

            if (root != null && root.input1 != null)
            {
                //var leftmostOffset = (int)(Math.Pow(2, treeHeight) * 2 * xCord) / 2;
                var rootCoord = new Point(panel.Width/2, (int)(yCord / 2));
                DrawTreeNode(g, rootCoord, root, 0);
            }

            UpdateArea();
        }

        private void DrawTreeNode(Graphics g, Point coord, Nodes node, int depth)
        {
            if (node == null)
                return;

            int nodeDiameter = 70;
            string display;
            System.Drawing.Font font = new System.Drawing.Font("Arial", 11, FontStyle.Regular);


            int ellipseX = coord.X - nodeDiameter / 2;
            int ellipseY = coord.Y - nodeDiameter / 2;

            if(node.operation!=null && node.Name != node.operation)
            {
                 display = node.operation;
                g.DrawString(node.Name, font, Brushes.Black, ellipseX - g.MeasureString(display,font).Width, ellipseY-nodeDiameter/2);
            } else
            {
                 display = node.Name;
            }
            
          

            var textSize = g.MeasureString(display, font);

            var symbolX = coord.X - textSize.Width / 2;
            var symbolY = coord.Y - textSize.Height / 2;
            
           
            if (node.Value == true)
            {
                g.FillEllipse(Brushes.Green, ellipseX, ellipseY, nodeDiameter,nodeDiameter);
            } else 
            if (node.Value == false)
            {
                g.FillEllipse(Brushes.Red, ellipseX, ellipseY, nodeDiameter, nodeDiameter);
            } else
            {
                g.DrawEllipse(Pens.Black, ellipseX, ellipseY, nodeDiameter, nodeDiameter);
            }

            if (node.operation == null)
            {
                g.DrawString(display, font, Brushes.Black, symbolX, symbolY);
            }
            g.DrawString(display, font, Brushes.Black, symbolX, symbolY);

            var xOffset = Math.Pow(2, treeHeight - depth+1) * xCord;
            var newY = (int)(((double)depth + 1) * yCord * 1.5);

            if (node.input1 != null)
            {
                int leftChildX;

                int endY;

                if (node.input2 == null)
                    leftChildX = coord.X;
                else
                    leftChildX = (int)(coord.X - xOffset);

                if (node.input1.operation != null)
                    endY = newY;
                else
                    endY = newY - nodeDiameter / 2;


                g.DrawLine(Pens.Black, coord.X, coord.Y + nodeDiameter / 2, leftChildX, newY - nodeDiameter / 2);
                var leftChildCoord = new Point(leftChildX, newY);
                DrawTreeNode(g, leftChildCoord, node.input1, depth + 1);
            }

            if (node.input2 != null)
            {
                int rightChildX;
                int endY;

                rightChildX = (int)(coord.X + xOffset);

                if (node.input2.operation != null)
                    endY = newY;
                else
                    endY = newY - nodeDiameter / 2;

                g.DrawLine(Pens.Black, coord.X, coord.Y + nodeDiameter / 2, rightChildX, newY - nodeDiameter / 2);
                var rightChildCoord = new Point(rightChildX, newY);
                DrawTreeNode(g, rightChildCoord, node.input2, depth + 1);
            }


        }
        internal void Form_Resize(object sender, EventArgs e)
        {
            panel.Invalidate();
            UpdateArea();
        }
        private void UpdateArea()
        {
            if (root == null) return;

            var maxTreeWidth = (int)(Math.Pow(2, treeHeight) * 2 * xCord);
            var maxTreeHeight = (int)(treeHeight * yCord * 1.5);

            panel.AutoScrollMinSize = new Size(
                maxTreeWidth,
                maxTreeHeight
            );

        }
        
    }

}

