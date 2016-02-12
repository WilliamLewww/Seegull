using OpenTK;
using System;
using System.Drawing;

namespace Seagull
{
    [Serializable()]
    class FoldingTile
    {
        public Vector3 position;
        public Vector3[] matrixPoint = new Vector3[4]
        {
        new Vector3(0, 0, 1),
        new Vector3(1, 0, 1),
        new Vector3(1, 0, 0),
        new Vector3(0, 0, 0)
        };

        public bool slantDepth = false;
        public bool slantOpposite = false;

        private Vector2 size;

        public Vector2 Size { get { return size; } set { size = value; } }

        public float Width { get { return size.X; } }
        public float Depth { get { return size.Y; } }

        private float scaleVal;
        public float Scale { get { return scaleVal; } }
        public float Height { get { if (slantDepth) return Depth * Scale; else return Width * Scale; } }

        public float X { get { return position.X; } }
        public float Y { get { return position.Y; } }
        public float Z { get { return position.Z; } }

        public float Left { get { return X; } }
        public float Right { get { return X + Width; } }
        public float Top { get { return Y; } }
        public float Bottom { get { return Y + Height; } }
        public float Front { get { return Z; } }
        public float Back { get { return Z + Depth; } }

        public void TranslateUp(int direction, float[] originalY)
        {
            switch (direction)
            {
                case 0:
                    matrixPoint = new Vector3[4]
                    {
                    new Vector3(0, .5f, 1),
                    new Vector3(1, .5f, 1),
                    new Vector3(1, originalY[2], 0),
                    new Vector3(0, originalY[3], 0)
                    };
                    slantDepth = true;
                    break;
                case 1:
                    matrixPoint = new Vector3[4]
                    {
                    new Vector3(0, originalY[0], 1),
                    new Vector3(1, originalY[1], 1),
                    new Vector3(1, .5f, 0),
                    new Vector3(0, .5f, 0)
                    };
                    slantDepth = true;
                    slantOpposite = true;
                    break;
                case 2:
                    matrixPoint = new Vector3[4]
                    {
                    new Vector3(0, .5f, 1),
                    new Vector3(1, originalY[1], 1),
                    new Vector3(1, originalY[2], 0),
                    new Vector3(0, .5f, 0)
                    };
                    slantOpposite = true;
                    break;
                case 3:
                    matrixPoint = new Vector3[4]
                    {
                    new Vector3(0, originalY[0], 1),
                    new Vector3(1, .5f, 1),
                    new Vector3(1, .5f, 0),
                    new Vector3(0, originalY[3], 0)
                    };
                    break;
            }
        }

        public void TranslateUp(int direction, float[] originalY, float scale)
        {
            scaleVal = scale;
            switch (direction)
            {
                case 0:
                    matrixPoint = new Vector3[4]
                    {
                    new Vector3(0, scale, 1),
                    new Vector3(1, scale, 1),
                    new Vector3(1, originalY[2], 0),
                    new Vector3(0, originalY[3], 0)
                    };
                    slantDepth = true;
                    break;
                case 1:
                    matrixPoint = new Vector3[4]
                    {
                    new Vector3(0, originalY[0], 1),
                    new Vector3(1, originalY[1], 1),
                    new Vector3(1, scale, 0),
                    new Vector3(0, scale, 0)
                    };
                    slantDepth = true;
                    slantOpposite = true;
                    break;
                case 2:
                    matrixPoint = new Vector3[4]
                    {
                    new Vector3(0, scale, 1),
                    new Vector3(1, originalY[1], 1),
                    new Vector3(1, originalY[2], 0),
                    new Vector3(0, scale, 0)
                    };
                    slantOpposite = true;
                    break;
                case 3:
                    matrixPoint = new Vector3[4]
                    {
                    new Vector3(0, originalY[0], 1),
                    new Vector3(1, scale, 1),
                    new Vector3(1, scale, 0),
                    new Vector3(0, originalY[3], 0)
                    };
                    break;
            }
        }

        public void TranslateDown(int direction, float[] originalY)
        {
            switch (direction)
            {
                case 0:
                    matrixPoint = new Vector3[4]
                    {
                    new Vector3(0, -.5f, 1),
                    new Vector3(1, -.5f, 1),
                    new Vector3(1, originalY[2], 0),
                    new Vector3(0, originalY[3], 0)
                    };
                    break;
                case 1:
                    matrixPoint = new Vector3[4]
                    {
                    new Vector3(0, originalY[0], 1),
                    new Vector3(1, originalY[1], 1),
                    new Vector3(1, -.5f, 0),
                    new Vector3(0, -.5f, 0)
                    };
                    slantOpposite = true;
                    break;
                case 2:
                    matrixPoint = new Vector3[4]
                    {
                    new Vector3(0, -.5f, 1),
                    new Vector3(1, originalY[1], 1),
                    new Vector3(1, originalY[2], 0),
                    new Vector3(0, -.5f, 0)
                    };
                    slantOpposite = true;
                    break;
                case 3:
                    matrixPoint = new Vector3[4]
                    {
                    new Vector3(0, originalY[0], 1),
                    new Vector3(1, -.5f, 1),
                    new Vector3(1, -.5f, 0),
                    new Vector3(0, originalY[3], 0)
                    };
                    break;
            }
        }

        public void TranslateDown(int direction, float[] originalY, float scale)
        {
            scaleVal = scale;
            switch (direction)
            {
                case 0:
                    matrixPoint = new Vector3[4]
                    {
                    new Vector3(0, -scale, 1),
                    new Vector3(1, -scale, 1),
                    new Vector3(1, originalY[2], 0),
                    new Vector3(0, originalY[3], 0)
                    };
                    break;
                case 1:
                    matrixPoint = new Vector3[4]
                    {
                    new Vector3(0, originalY[0], 1),
                    new Vector3(1, originalY[1], 1),
                    new Vector3(1, -scale, 0),
                    new Vector3(0, -scale, 0)
                    };
                    slantOpposite = true;
                    break;
                case 2:
                    matrixPoint = new Vector3[4]
                    {
                    new Vector3(0, -scale, 1),
                    new Vector3(1, originalY[1], 1),
                    new Vector3(1, originalY[2], 0),
                    new Vector3(0, -scale, 0)
                    };
                    slantOpposite = true;
                    break;
                case 3:
                    matrixPoint = new Vector3[4]
                    {
                    new Vector3(0, originalY[0], 1),
                    new Vector3(1, -scale, 1),
                    new Vector3(1, -scale, 0),
                    new Vector3(0, originalY[3], 0)
                    };
                    break;
            }
        }

        public FoldingTile(Vector3 positionArgs, int width, int height)
        {
            position = positionArgs;
            size = new Vector2(width, height);
        }

        public FoldingTile(Vector3 positionArgs, int width, int height, bool translateUp, int[] modifier)
        {
            position = positionArgs;
            size = new Vector2(width, height);

            float[] originalY = new float[4];

            for (int x = 0; x < modifier.Length; x++)
            {
                for (int y = 0; y < originalY.Length; y++)
                    originalY[y] = matrixPoint[y].Y;

                if (translateUp)
                    TranslateUp(modifier[x], originalY);
                else
                    TranslateDown(modifier[x], originalY);
            }
        }

        public FoldingTile(Vector3 positionArgs, int width, int height, bool translateUp, int[] modifier, float theta)
        {
            position = positionArgs;
            size = new Vector2(width, height);

            float[] originalY = new float[4];

            for (int x = 0; x < modifier.Length; x++)
            {
                for (int y = 0; y < originalY.Length; y++)
                    originalY[y] = matrixPoint[y].Y;

                if (translateUp)
                    TranslateUp(modifier[x], originalY, theta);
                else
                    TranslateDown(modifier[x], originalY, theta);
            }
        }

        public virtual void Draw(Color color) { SpriteBatch.DrawTile(matrixPoint, slantDepth, Size, position, color); }
    }
}