using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Seagull
{
    class SpriteBatch
    {
        public static void DrawRectangle(Vector2 size, Vector2 position, Color color)
        {
            Vector2[] vectors = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 1)
            };

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(color);
            for (int x = 0; x < 4; x++)
            {
                vectors[x].X *= size.X;
                vectors[x].Y *= size.Y;
                vectors[x] += position;

                GL.Vertex2(vectors[x]);
            }
            GL.End();
        }

        public static void DrawTile(Vector3[] vectors, bool slantDepth, Vector2 size, Vector3 position, Color color)
        {
            Vector3[] tempVectors = new Vector3[vectors.Length];
            for (int x = 0; x < tempVectors.Length; x++)
                tempVectors[x] = vectors[x];

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(color);
            for (int x = 0; x < 4; x++)
            {
                tempVectors[x].X *= size.X;
                tempVectors[x].Z *= size.Y;
                if (slantDepth) tempVectors[x].Y *= size.Y;
                else tempVectors[x].Y *= size.X;
                tempVectors[x] += position;

                GL.Vertex3(tempVectors[x]);
            }
            GL.End();
        }

        public static Vector3[] DrawTile(Vector2 size, Vector3 position, Color color, bool draw)
        {
            Vector3[] vectors = new Vector3[4]
            {
                new Vector3(0, 0, 1),
                new Vector3(1, 0, 1),
                new Vector3(1, 0, 0),
                new Vector3(0, 0, 0)
            };

            if (draw)
            {
                GL.Begin(PrimitiveType.Quads);
                GL.Color3(color);
                for (int x = 0; x < 4; x++)
                {
                    vectors[x].X *= size.X;
                    vectors[x].Z *= size.Y;
                    vectors[x] += position;

                    GL.Vertex3(vectors[x]);
                }
                GL.End();
            }
            else
            {
                for (int x = 0; x < 4; x++)
                {
                    vectors[x].X *= size.X;
                    vectors[x].Z *= size.Y;
                    vectors[x] += position;

                    GL.Vertex3(vectors[x]);
                }
            }

            return vectors;
        }

        public static void DrawRectangularPrism(Vector3 size, Vector3 position, Color color)
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

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(color);
            for (int x = 0; x < 24; x++)
            {
                vectors[x].X *= size.X;
                vectors[x].Y *= size.Y;
                vectors[x].Z *= size.Z;
                vectors[x] += position;

                GL.Vertex3(vectors[x]);
            }
            GL.End();
        }

        public static void DrawRectangularPrism(Vector3 size, Vector3 position, double rotation, Color color)
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

            GL.Translate(position.X + size.X / 2, position.Y, position.Z - size.Z / 2);
            GL.Rotate(rotation, 0, 1, 0);
            GL.Translate(-size.X / 2, 0, size.Z / 2);

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(color);
            for (int x = 0; x < 24; x++)
            {
                vectors[x].X *= size.X;
                vectors[x].Y *= size.Y;
                vectors[x].Z *= size.Z;

                GL.Vertex3(vectors[x]);
            }
            GL.End();

            GL.PopMatrix();
        }

        public static void DrawRectangularPrism(Vector3 size, Vector3 position, double rotation, double rotationY, Color color)
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

            GL.Translate(position.X + size.X / 2, position.Y, position.Z - size.Z / 2);
            GL.Rotate(rotation, 0, 1, 0);
            GL.Rotate(rotationY, 1, 0, 0);
            GL.Translate(-size.X / 2, 0, size.Z / 2);

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(color);
            for (int x = 0; x < 24; x++)
            {
                vectors[x].X *= size.X;
                vectors[x].Y *= size.Y;
                vectors[x].Z *= size.Z;

                GL.Vertex3(vectors[x]);
            }
            GL.End();

            GL.PopMatrix();
        }
    }
}
