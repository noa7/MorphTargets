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
         //   Name = name;
        }



        public void AddShape(Shape shape, float weight)
        {
            if (shape != null)
            {
                (Shapes ??= new Dictionary<float, Shape>())[Math.Clamp(0f, 1f, weight)] = shape;
            }
        }


        public int GetShapeCount()
        {
            return Shapes == null ? 0 : Shapes.Count;
        }

        public Shape GetShapeAtIndex(int index)
        {
            return Shapes == null ? null : Shapes[index];
        }

        public float[] GetShapeWeightsArray()
        {
            if (Shapes == null) return null;
            float[] weights = new float[Shapes.Count];
            Shapes.Keys.CopyTo(weights, 0);
            return weights;
        }

    }
}
