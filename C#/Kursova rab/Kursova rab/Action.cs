using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace Kursova_rab
{
    internal abstract class Action
    {

       internal GlobalStuff _stuff {  get; set; }



        private List <Shapes> _shapes = new List<Shapes>();
        internal abstract void Do();
        internal abstract void Undo();
        


        internal void Connect(List<Shapes> _Shapes, GlobalStuff stuff)
        {

            _shapes = _Shapes;
            _stuff = stuff;

        }

        internal class ReColor : Action
        {
            internal Color Color { get; set; }
            internal Color LastColor { get; set; }
            internal Color NewColor {  get; set; }

            


            internal int Index { get; set; }

            
            internal ReColor(List<Shapes> _Shapes, GlobalStuff Stuff)
            {

                Connect(_Shapes, Stuff);

                Index = _stuff.index;
             
                LastColor = _shapes.ElementAt(Index).lastcolor;
                Color = _shapes.ElementAt(Index).Color;
                NewColor = _stuff.basecolor;

                Do();

            }
            internal override void Do()
            {
                
               _shapes.ElementAt(Index).Color = NewColor;
               _shapes.ElementAt(Index).lastcolor = Color;
            }
            internal override void Undo()
            {
                _shapes.ElementAt(Index).Color = Color;
                _shapes.ElementAt(Index).lastcolor = LastColor;
               

            }

        }

        internal class Move : Action
        {
            internal int Index { get; set; }
            internal PointF Last {  get; set; }


            



            internal Move(List<Shapes> _Shapes, GlobalStuff Stuff)
            {

                Connect(_Shapes, Stuff);
                Index = _stuff.index;
                Last = _shapes.ElementAt(Index).Center;
                

            }
            internal override void Do()
            {

            }

            internal override void Undo()
            {
                _shapes.ElementAt(Index).MoveCenter(Last);
            }

        }

        internal class Border : Action
        {
            internal int Index { get; set; }

            internal Border(List<Shapes> _Shapes, GlobalStuff Stuff)
            {

                Connect(_Shapes, Stuff);
                Index = _stuff.index;
                Do();
            }
            internal override void Do()
            {
                _shapes.ElementAt(Index).Border = !_shapes.ElementAt(Index).Border;
                

            }
            internal override void Undo()
            {
                Do();
            }
        }

        internal class Edge : Action
        {
            internal int Index { get; set; }

            internal Edge(List<Shapes> _Shapes, GlobalStuff Stuff)
            {

                Connect(_Shapes, Stuff);
                Index = _stuff.index;
                Do();

            }

            internal override void Do()
            {
                _shapes.ElementAt(Index).Edges = !_shapes.ElementAt(Index).Edges;

            }

            internal override void Undo()
            {
                Do();
            }
        }
        internal class Area : Action
        {
            internal int Index { get; set; }

            internal Area (List<Shapes> _Shapes, GlobalStuff Stuff)
            {
                Connect(_Shapes, Stuff);
                Index = _stuff.index;
                Do();
            }

            internal override void Do()
            {
                _shapes.ElementAt(Index).Area = !_shapes.ElementAt(Index).Area;
                
            }

            internal override void Undo()
            {
                Do();
            }
        }

        internal class Create : Action
        {
            internal int Index { get; set; }

            internal Create (List<Shapes> _Shapes, GlobalStuff Stuff)
            {
                Connect(_Shapes, Stuff);
                Index = _stuff.index;

            }
            internal override void Do()
            {
                _shapes.RemoveAt(0);
            }

            internal override void Undo()
            {
                
                
                    _shapes.RemoveAt(_shapes.Count - 1);
                
            }
        }
    }

}
