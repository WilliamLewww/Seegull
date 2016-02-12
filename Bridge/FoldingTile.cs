using Newtonsoft.Json;
using OpenTK;
using System.Drawing;

[JsonObject(MemberSerialization.OptIn)]
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

    [JsonProperty]
    public float[,] MatrixPointFloat
    {
        get {
            float[,] points = new float[4, 3];
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    points[x, y] = Utlis.VectorToFloatArray(matrixPoint[x])[y];
                }
            }

            return points;
        }
    }

    [JsonProperty]
    public bool SlantDepth { get; set; }
    [JsonProperty]
    public bool SlantOpposite { get; set; }

    private Vector2 size;
    public Vector2 Size { get { return size; } set { size = value; } }

    [JsonProperty]
    public float Width { get { return size.X; } }
    [JsonProperty]
    public float Depth { get { return size.Y; } }

    private float scaleVal;
    [JsonProperty]
    public float Scale { get { return scaleVal; } }
    public float Height { get { if (SlantDepth) return Depth * Scale; else return Width * Scale; } }

    [JsonProperty]
    public float X { get { return position.X; } }
    [JsonProperty]
    public float Y { get { return position.Y; } }
    [JsonProperty]
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
                SlantDepth = true;
                break;
            case 1:
                matrixPoint = new Vector3[4]
                {
                new Vector3(0, originalY[0], 1),
                new Vector3(1, originalY[1], 1),
                new Vector3(1, .5f, 0),
                new Vector3(0, .5f, 0)
                };
                SlantDepth = true;
                SlantOpposite = true;
                break;
            case 2:
                matrixPoint = new Vector3[4]
                {
                new Vector3(0, .5f, 1),
                new Vector3(1, originalY[1], 1),
                new Vector3(1, originalY[2], 0),
                new Vector3(0, .5f, 0)
                };
                SlantOpposite = true;
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
                SlantDepth = true;
                break;
            case 1:
                matrixPoint = new Vector3[4]
                {
                new Vector3(0, originalY[0], 1),
                new Vector3(1, originalY[1], 1),
                new Vector3(1, scale, 0),
                new Vector3(0, scale, 0)
                };
                SlantDepth = true;
                SlantOpposite = true;
                break;
            case 2:
                matrixPoint = new Vector3[4]
                {
                new Vector3(0, scale, 1),
                new Vector3(1, originalY[1], 1),
                new Vector3(1, originalY[2], 0),
                new Vector3(0, scale, 0)
                };
                SlantOpposite = true;
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
                SlantOpposite = true;
                break;
            case 2:
                matrixPoint = new Vector3[4]
                {
                new Vector3(0, -.5f, 1),
                new Vector3(1, originalY[1], 1),
                new Vector3(1, originalY[2], 0),
                new Vector3(0, -.5f, 0)
                };
                SlantOpposite = true;
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
                SlantOpposite = true;
                break;
            case 2:
                matrixPoint = new Vector3[4]
                {
                new Vector3(0, -scale, 1),
                new Vector3(1, originalY[1], 1),
                new Vector3(1, originalY[2], 0),
                new Vector3(0, -scale, 0)
                };
                SlantOpposite = true;
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

    public virtual void Draw(Color color) { SpriteBatch.DrawTile(matrixPoint, SlantDepth, Size, position, color); }
}