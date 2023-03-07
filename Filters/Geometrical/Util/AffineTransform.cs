using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerGraphics0.Filters.Geometrical.Util
{
    public  class AffineTransform
    {
        public float[,] transformationMatrix;
        public AffineTransform(float[,] matrix)
        {
            if(matrix.GetLength(0) != 3 || matrix.GetLength(1) != 3)
            {
                throw new ArgumentException($"Affine transformation is only doable on 3x3 matrix. {matrix.GetLength(0)}x{matrix.GetLength(1)} was given");
            }
            transformationMatrix = matrix;
        }
        public (int, int) Apply(int x, int y){
            (int, int) result = (
                (int)(x* transformationMatrix[0, 0] + y* transformationMatrix[0, 1] + transformationMatrix[0, 2]),
                (int)(x* transformationMatrix[1, 0] + y* transformationMatrix[1, 1] + transformationMatrix[1, 2])
            );
            return result;
        }
        public static float[,] CalculateInverseMatrix(float[,] m)
        {
            float Det(float a11, float a12, float a21, float a22){
                return a11*a22 - a21*a12;
            }
            if(m.GetLength(0) != 3 || m.GetLength(1) != 3)
            {
                throw new ArgumentException($"Affine transformation is only doable on 3x3 matrix. {m.GetLength(0)}x{m.GetLength(1)} was given");
            }
            

            float invDet = 1/(  m[0,0]*m[1,1]*m[2,2]+
                                m[0,1]*m[1,2]*m[2,0]+
                                m[0,2]*m[1,0]*m[2,1]-
                                m[0,2]*m[1,1]*m[2,0]-
                                m[0,1]*m[1,0]*m[2,2]-
                                m[0,0]*m[1,2]*m[2,1]);
            var result = new float[3,3]{
                {   +invDet * Det(m[1,1], m[1,2],m[2,1],m[2,2]),
                    -invDet * Det(m[1,0], m[1,2],m[2,0],m[2,2]),
                    +invDet * Det(m[1,0], m[1,1],m[2,0],m[2,1])},
                {   -invDet * Det(m[0,1], m[0,2],m[2,1],m[2,2]),
                    +invDet * Det(m[0,0], m[0,2],m[2,0],m[2,2]),
                    -invDet * Det(m[0,0], m[0,1],m[2,0],m[2,1])},
                {   +invDet * Det(m[0,1], m[0,2],m[1,1],m[1,2]),
                    -invDet * Det(m[0,0], m[0,2],m[1,0],m[1,2]),
                    +invDet * Det(m[0,0], m[0,1],m[1,0],m[1,1])}
            };
            return result;
        }
    }
}