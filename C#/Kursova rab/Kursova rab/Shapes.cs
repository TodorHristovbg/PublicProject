using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova_rab
{
    internal class Shapes
    {
        internal PointF[] points;
        internal PointF Center { get; set; }
        internal Color Color { get;  set; }

        internal Color lastcolor { get; set; } = Color.Black;
        internal float Size { get; set; }

        internal float Diam { get; set; }

        internal bool Border = false;

        internal bool Edges = false;

        internal bool Area = false;

        internal static SolidBrush basebrush { get; set; }

        internal void Fill(Graphics g)
        {


            basebrush = new SolidBrush(Color);


            g.FillPolygon(basebrush, points);


            if (Edges)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    basebrush.Color = Color.White;
                    RectangleF rect = new RectangleF(points[i].X - 3, points[i].Y - 3, 6, 6);
                    g.FillEllipse(basebrush, rect);
                }
            }
            if (Border)
            {
                //  basebrush.Color = Color.White;
                //  RectangleF rect2 = new RectangleF(Center.X - 3, Center.Y - 3, 6, 6);
                //  g.FillEllipse(basebrush, rect2);

                Pen pen = new Pen(Color.Black);
                g.DrawEllipse(pen, Center.X - Diam / 2, Center.Y - Diam / 2, Diam, Diam);
            }
            if (Area)
            {
                if (this.Color == Color.Black)
                {
                    basebrush.Color = Color.White;
                }
                else
                {
                    basebrush.Color = Color.Black;
                }

                Font drawFont = new Font("Arial", this.Diam / 6);

                g.DrawString(this.FindArea().ToString(), drawFont, basebrush, Center.X - Diam * 2 / 7, Center.Y - Diam / 8);
            }

        }
        internal virtual float FindArea()
        {
            float sidea = Formulas.Distance(this.points[0], this.points[1]);
            float sideb = Formulas.Distance(this.points[1], this.points[2]);




            return (int)(sidea * sideb) / 100;

        }

        internal virtual void MoveCenter(PointF newCenter)
        {
           

        }


    }
    internal class Square : Shapes
    {
        internal Square(PointF center, float size, Color color)
        {

            Center = center;
            Size = size;
            Color = color;
            MoveCenter(Center);
        }

        internal override void MoveCenter(PointF newCenter)
        {
            Center = newCenter;
            float ctoedge = (float)(Size * (Math.Sqrt(2) / 2));
            Diam = ctoedge * 2;
            float offset = (float)Math.Sqrt((ctoedge * ctoedge) / 2);
            points = new PointF[4];
            points[0] = new PointF(Center.X - offset, Center.Y - offset);
            points[1] = new PointF(Center.X - offset, Center.Y + offset);
            points[2] = new PointF(Center.X + offset, Center.Y + offset);
            points[3] = new PointF(Center.X + offset, Center.Y - offset);

        }
    }
    internal class Triangle : Shapes
    {
        internal Triangle(PointF center, float size, Color color)
        {
            Center = center;
            Size = size;
            Color = color;
            MoveCenter(Center);
            
        }
        internal override void MoveCenter(PointF newCenter)
        {
            Center = newCenter;
            float temp = (float)(Size * (Math.Sqrt(3) / 6));
            float temp2 = Size / 2;

            float ctoedge = (float)Math.Sqrt(temp * temp + temp2 * temp2);
            Diam = ctoedge * 2;

            float xoffset = (float)Math.Cos(Math.PI / 6) * ctoedge;
            float yoffset = (float)Math.Sin(Math.PI / 6) * ctoedge;



            points = new PointF[3];
            points[0] = new PointF(Center.X - xoffset, Center.Y + yoffset); //bottom left
            points[1] = new PointF(Center.X, Center.Y - ctoedge);
            points[2] = new PointF(Center.X + xoffset, Center.Y + yoffset); //bottom right

          
        }
        internal override float FindArea()
        {

            float sidea = Formulas.Distance(this.points[0], this.points[1]);
            float sideb = Formulas.Distance(this.points[1], this.points[2]);
            float sidec = Formulas.Distance(this.points[0], this.points[2]);
            float h = (sidea + sideb + sidec) / 2;

            float area = (float)Math.Sqrt(h * (h - sidea) * (h - sideb) * (h - sidec)) / 100;

            area = (float)Math.Round(area, 2);

            return area;
        }
    }
    internal class Rectangle : Shapes
    {
        internal Rectangle(PointF center, float size, Color color)
        {
            Center = center;
            Size = size;
            Color = color;
            MoveCenter(Center);
           

        }
        internal override void MoveCenter(PointF newCenter)
        {
            Center = newCenter;
            int sideb = (int)Size / 2;
            float ctoedge = (float)Math.Sqrt(Size * Size + sideb * sideb);

            float sin = sideb / ctoedge;
            float cos = Size / ctoedge;

            Diam = ctoedge;

            float xoffset = cos * ctoedge / 2;
            float yoffset = sin * ctoedge / 2;


            points = new PointF[4];
            points[0] = new PointF(Center.X - xoffset, Center.Y - yoffset);
            points[1] = new PointF(Center.X - xoffset, Center.Y + yoffset);
            points[2] = new PointF(Center.X + xoffset, Center.Y + yoffset);
            points[3] = new PointF(Center.X + xoffset, Center.Y - yoffset);
        }
    }

}
