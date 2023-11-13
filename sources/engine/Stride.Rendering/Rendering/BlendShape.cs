using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Rendering
{
    public class BlendShape
    {


        public string Name { get; private set; }

        public BlendShape(string name)
        {
            Name = name;
        }

        public Dictionary<float, Shape> Shapes { get; private set; }

        public void AddShape(Shape shape, float weight)
        {
            if (shape != null)
            { (Shapes ??= new Dictionary<float, Shape>())[Math.Clamp(0f, 1f, weight)] = shape;
            }
        }

        

        public int GetShapeCount()
        {
            return Shapes==null ? 0 : Shapes.Count;
        }

        public Shape GetShapeAtIndex(int index)
        {
            return Shapes==null? null : Shapes[index];
        }

        public float[] GetShapeWeightsArray() 
        {
            if( Shapes==null) return null;
            float[] weights = new float[Shapes.Count];
            Shapes.Keys.CopyTo(weights, 0);
            return weights;
        }

    }
}
