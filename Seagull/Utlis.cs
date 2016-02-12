using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Seagull
{
    static class Utlis
    {
        public static double GetReferenceAngle(double x)
        {
            while (x >= 360)
                x -= 360;

            while (x < 0)
                x += 360;

            return x;
        }

        public static double LimitAngle(double x, double min, double max)
        {
            if (x < min)
                return min;
            else if (x > max)
                return max;
            else
                return x;
        }

        public static float GetMaxValueVector(Vector3[] vectorList, int coordinate)
        {
            float tempMax = 0;

            for (int x = 0; x < vectorList.Length; x++)
            {
                if (x == 0)
                {
                    if (coordinate == 0) tempMax = vectorList[0].X;
                    if (coordinate == 1) tempMax = vectorList[0].Y;
                    if (coordinate == 2) tempMax = vectorList[0].Z;
                }

                if (coordinate == 0) if (vectorList[x].X > tempMax) tempMax = vectorList[x].X;
                if (coordinate == 1) if (vectorList[x].Y > tempMax) tempMax = vectorList[x].Y;
                if (coordinate == 2) if (vectorList[x].Z > tempMax) tempMax = vectorList[x].Z;
            }

            return tempMax;
        }

        public static float GetMinValueVector(Vector3[] vectorList, int coordinate)
        {
            float tempMin = 0;

            for (int x = 0; x < vectorList.Length; x++)
            {
                if (x == 0)
                {
                    if (coordinate == 0) tempMin = vectorList[0].X;
                    if (coordinate == 1) tempMin = vectorList[0].Y;
                    if (coordinate == 2) tempMin = vectorList[0].Z;
                }

                if (coordinate == 0) if (vectorList[x].X < tempMin) tempMin = vectorList[x].X;
                if (coordinate == 1) if (vectorList[x].Y < tempMin) tempMin = vectorList[x].Y;
                if (coordinate == 2) if (vectorList[x].Z < tempMin) tempMin = vectorList[x].Z;
            }

            return tempMin;
        }

        public static Matrix4 GetMatrix(Vector3 size, Vector3 position, double rotation)
        {
            Vector3[] vectors = new Vector3[24]
            {
                new Vector3(0,0,0), new Vector3(1,0,0),
                new Vector3(1,1,0), new Vector3(0,1,0),

                new Vector3(1,0,0), new Vector3(1,0,-1),
                new Vector3(1,1,-1), new Vector3(1,1,0),

                new Vector3(0,0,0), new Vector3(0,0,-1),
                new Vector3(1,0,-1), new Vector3(1,0,0),

                new Vector3(0,0,-1), new Vector3(0,0,0),
                new Vector3(0,1,0), new Vector3(0,1,-1),

                new Vector3(0,1,0), new Vector3(1,1,0),
                new Vector3(1,1,-1), new Vector3(0,1,-1),

                new Vector3(1,0,-1), new Vector3(0,0,-1),
                new Vector3(0,1,-1), new Vector3(1,1,-1),
            };

            GL.PushMatrix();

            GL.Rotate(rotation, 0, 1, 0);

            for (int x = 0; x < 24; x++)
            {
                vectors[x].X *= size.X;
                vectors[x].Y *= size.Y;
                vectors[x].Z *= size.Z;

                GL.Vertex3(vectors[x]);
            }

            Matrix4 matrix;
            GL.GetFloat(GetPName.ModelviewMatrix, out matrix);

            GL.PopMatrix();

            return matrix;
        }

        public static Vector3 GetTheoreticalPoints(Matrix4 matrix, Vector3 vector)
        {
            return Vector3.TransformVector(vector, matrix);
        }
    }
}
