using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ShapesExperiment
{
    public class Shape
    {
        public int ShapeID { get; set; }
        public Bitmap Image { get; set; }

        public Shape(int id, Bitmap image)
        {
            this.ShapeID = id;
            this.Image = image;
        }
    }
}
