using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Schema;
using static Kursova_rab.Action;

namespace Kursova_rab
{
    public partial class Form1 : Form
    {
        
        private readonly List<Shapes> _shapes;
        private List<Action> _actions;
        //public static Font drawFont = new Font("Arial", 16);
      
        GlobalStuff _stuff = new GlobalStuff();

        public Form1()
        {
            InitializeComponent();
            _shapes = new List<Shapes>();
            _actions = new List<Action>();
            
            base.KeyPreview = true;
            //this.KeyPreview = true;
            SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.AllPaintingInWmPaint,
            true);
            SelectSize.SelectedIndex = 4;
            UpdateStyles();
            
        }

        protected void OnKeyDown(object sender, KeyEventArgs e)
        {
            //base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Escape: this.Close(); break;
                case Keys.W: if (_stuff.shapeindex < 2) { _stuff.shapeindex++; } else { _stuff.shapeindex = 0; } break;
                case Keys.A:
                    colorDialog1.ShowDialog();
                    _stuff.basecolor = colorDialog1.Color;
                    label2.BackColor = colorDialog1.Color; break;
                case Keys.D: BoxBorder.Checked = !BoxBorder.Checked; BoxEdit.Checked = false; break;
                case Keys.C: BoxChangeColor.Checked = !BoxChangeColor.Checked; BoxEdit.Checked = false; break;
                case Keys.E: BoxEdges.Checked = !BoxEdges.Checked; BoxEdit.Checked = false; break;
                case Keys.R: BoxArea.Checked = !BoxArea.Checked; BoxEdit.Checked = false; break;
                case Keys.M:
                    BoxArea.Checked = false;
                    _stuff.displayarea = false;
                    BoxEdges.Checked = false;
                    _stuff.displayedges = false;
                    BoxBorder.Checked = false;
                    _stuff.displayborder = false;
                    BoxChangeColor.Checked = false;
                    _stuff.changecolor = false;
                    BoxEdit.Checked = !BoxEdit.Checked;
                    _stuff.tempcolor = Color.Black;
                    _stuff.basecolor = colorDialog1.Color;

                    break;
                case Keys.Z:
                    if (_actions.Count > 0)
                    {
                        _actions.Last().Undo();
                        _actions.RemoveAt(_actions.Count - 1);
                        Invalidate();
                    }
                     break;


            }

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // base.OnMouseClick(e);


            PointF click = new PointF(e.X, e.Y);



            for (_stuff.index = 0; _stuff.index < _shapes.Count; _stuff.index++)
            {
                if (Formulas.Distance(click, _shapes.ElementAt(_stuff.index).Center) < (_shapes.ElementAt(_stuff.index).Diam))
                {
                    if (Formulas.Distance(click, _shapes.ElementAt(_stuff.index).Center) < (_shapes.ElementAt(_stuff.index).Diam / 2))
                    {

                        if (_stuff.editMode)
                        {
                            _stuff.isMouseDown = true;
                            _actions.Add(new Move(_shapes, _stuff));
                            return;
                        }



                        if (_stuff.changecolor)
                        {
                            var action = new ReColor(_shapes,_stuff);
                            _actions.Add(action);
                        }
                        if (_stuff.displayborder)
                        {
                            var action = new Border(_shapes, _stuff);
                            _actions.Add(action);


                        }
                        if (_stuff.displayedges)
                        {
                            var action = new Edge(_shapes, _stuff);
                            _actions.Add(action);

                        }
                        if (_stuff.displayarea)
                        {
                            var action = new Area(_shapes, _stuff);
                            _actions.Add(action);
                        }
                    }

                    Invalidate();
                    return;
                }
            }


            var created = new Create(_shapes, _stuff);
            _actions.Add(created);


            switch (_stuff.shapeindex)
            {
                case 0:

                    var square = new Square(e.Location, _stuff.ShapeDiam, _stuff.basecolor);
                    _shapes.Add(square);
                    
                  
                    if (_stuff.displayedges)
                    {
                        var action = new Edge(_shapes, _stuff);
                        _actions.Add(action);
                    }
                    if (_stuff.displayborder)
                    {
                        var action = new Border(_shapes, _stuff);
                        _actions.Add(action);
                    }
                    if (_stuff.displayarea)
                    {
                        var action = new Area(_shapes, _stuff);
                        _actions.Add(action);

                    }
                    square.lastcolor = _stuff.tempcolor;
                  
                    break;
                case 1:

                    var triangle = new Triangle(e.Location, _stuff.ShapeDiam, _stuff.basecolor);
                    _shapes.Add(triangle);
                    if (_stuff.displayedges)
                    {
                        var action = new Edge(_shapes, _stuff);
                        _actions.Add(action);
                    }
                    if (_stuff.displayborder)
                    {
                        var action = new Border(_shapes, _stuff);
                        _actions.Add(action);
                    }
                    if (_stuff.displayarea)
                    {
                        var action = new Area(_shapes, _stuff);
                        _actions.Add(action);

                    }
                    triangle.lastcolor = _stuff.tempcolor;
                   
                    break;
                case 2:

                    var rectangle = new Rectangle(e.Location, _stuff.ShapeDiam, _stuff.basecolor);
                    _shapes.Add(rectangle);
                    if (_stuff.displayedges)
                    {
                        var action = new Edge(_shapes, _stuff);
                        _actions.Add(action);
                    }
                    if (_stuff.displayborder)
                    {
                        var action = new Border(_shapes, _stuff);
                        _actions.Add(action);
                    }
                    if (_stuff.displayarea)
                    {
                        var action = new Area(_shapes, _stuff);
                        _actions.Add(action);

                    }
                    rectangle.lastcolor = _stuff.tempcolor;
                    
                    break;
            }


            
            _stuff.index = _shapes.Count - 1;
            Refresh();
            return;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //  base.OnPaint(e);

            foreach (Shapes shapes in _shapes)
            {
                shapes.Fill(e.Graphics);

            }

        }

        private void BoxBorder_CheckedChanged(object sender, EventArgs e)
        {

            if (BoxBorder.Checked)
            {
                BoxBorder.BackColor = Color.Green;
            }
            else
            {
                BoxBorder.BackColor = Color.Transparent;
            }
            boxChecks();
        }

        private void BoxColorChange_CheckedChanged(object sender, EventArgs e)
        {

            if (BoxChangeColor.Checked)
            {
                BoxChangeColor.BackColor = Color.Green;
            }
            else
            {
                BoxChangeColor.BackColor = Color.Transparent;
            }
            boxChecks();
        }

        private void BoxDisplayEdges_CheckedChanged(object sender, EventArgs e)
        {
            if (BoxEdges.Checked)
            {
                BoxEdges.BackColor = Color.Green;
            }
            else
            {
                BoxEdges.BackColor = Color.Transparent;
            }
            boxChecks();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (BoxArea.Checked)
            {
                BoxArea.BackColor = Color.Green;
            }
            else
            {
                BoxArea.BackColor = Color.Transparent;
            }
            boxChecks();

        }
        private void BoxEdit_CheckedChanged(object sender, EventArgs e)
        {

            if (BoxEdit.Checked)
            {
                BoxEdit.BackColor = Color.Green;
                _stuff.editMode = true;

            }
            else
            {
                _stuff.editMode = false; ;
                BoxEdit.BackColor = Color.Transparent;
            }

        }

        private void SelectSize_SelectedIndexChanged(object sender, EventArgs e)
        {

            _stuff.ShapeDiam = int.Parse(SelectSize.Text) * 10;

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {


            if (_stuff.isMouseDown && _stuff.editMode)
            {
                PointF click = new PointF(e.X, e.Y);

                _shapes.ElementAt(_stuff.index).MoveCenter(click);
                
                Invalidate();

            }

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {

            if (_stuff.isMouseDown)
            {
                _stuff.isMouseDown = false;

            }
        }

        private void boxChecks()
        {
            _stuff.displayarea = BoxArea.Checked;
            _stuff.displayedges = BoxEdges.Checked;
            _stuff.displayborder = BoxBorder.Checked;
            _stuff.changecolor = BoxChangeColor.Checked;

        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }
    }
}