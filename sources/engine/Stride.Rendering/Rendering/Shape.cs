using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Rendering
{
    // Represet one instance of blend shape and control points
    public class Shape
    {
        public string Name { get; private set; }

        //Index Of control points in shape
        public int[] Indices { get; private set; }

        //Array of position of each of the control point in mesh with index that set in array Indices of same length
        public Vector4[] Positions { get; private set; }


        public Shape(string name, int[] indices, Vector4[] positions) 
        {
            this.Name = name;
            this.Indices = indices;
            this.Positions = positions;
        }
    }
}
