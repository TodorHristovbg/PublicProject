using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Kursova_rab.Form1;

namespace Kursova_rab
{
    public class GlobalStuff
    {
        public Color basecolor { get; set; } = Color.Black;
       
        public Color tempcolor { get; set; } = Color.Black;
        public int shapeindex { get; set; } = 0;
   
        public int ShapeDiam { get; set; }
      
        public int index { get; set; } = 0;

        




        public bool changecolor { get; set; } = false;
        public bool displayborder { get; set; } = false;
        public bool displayedges { get; set; } = false;
        public bool displayarea { get; set; } = false;
        public bool editMode { get; set; } = false;
        public bool isMouseDown { get; set; } = false;

        public GlobalStuff()
        {

        }


    }
}
