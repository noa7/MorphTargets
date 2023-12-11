using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core;

namespace Stride.Rendering
{
    // Each instace of class represents one blendshape in an FBXChannel in the mesh
   [DataContract]
    public class BlendShape
    {
        //Name as in source model file
        public string Name { get;  set; }

        public Dictionary<float, Shape> Shapes { get;  set; }

        public BlendShape()
        {
        }

        public Dictionary<float, Shape> Shapes { get; private set; }


        public void AddShape(Shape shape, float weight)
        {
            if (shape != null)
            }
        }

        

        public int GetShapeCount()
        {
        }

        public Shape GetShapeAtIndex(int index)
        {
        }

        public float[] GetShapeWeightsArray() 
        {
            float[] weights = new float[Shapes.Count];
            Shapes.Keys.CopyTo(weights, 0);
            return weights;
        }

    }
}
