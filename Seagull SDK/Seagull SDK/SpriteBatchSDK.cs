using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace Seagull_SDK
{
    class SpriteBatchSDK
    {
        public static void Begin(int screenWidth, int screenHeight)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-screenWidth / 2, screenWidth / 2, screenHeight / 2, -screenHeight / 2, 0f, 1f);
        }

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

        public static void DrawFoldingTileTop(FoldingTile foldingTile, Color color)
        {
            Vector2[] vectors = new Vector2[4]
            {
                new Vector2(foldingTile.matrixPoint[0].X, foldingTile.matrixPoint[0].Z),
                new Vector2(foldingTile.matrixPoint[1].X, foldingTile.matrixPoint[1].Z),
                new Vector2(foldingTile.matrixPoint[2].X, foldingTile.matrixPoint[2].Z),
                new Vector2(foldingTile.matrixPoint[3].X, foldingTile.matrixPoint[3].Z)
            };

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(color);
            for (int x = 0; x < 4; x++)
            {
                vectors[x].X *= foldingTile.Size.X;
                vectors[x].Y *= foldingTile.Size.Y;
                vectors[x] += new Vector2(foldingTile.position.X, foldingTile.position.Z);

                GL.Vertex2(vectors[x]);
            }
            GL.End();
        }

        public static void DrawFoldingTileSide(FoldingTile foldingTile, Color color)
        {
            Vector3[] vectors = new Vector3[4]
            {
                new Vector3(foldingTile.matrixPoint[0].X, foldingTile.matrixPoint[0].Y, foldingTile.matrixPoint[0].Z),
                new Vector3(foldingTile.matrixPoint[1].X, foldingTile.matrixPoint[1].Y, foldingTile.matrixPoint[1].Z),
                new Vector3(foldingTile.matrixPoint[2].X, foldingTile.matrixPoint[2].Y, foldingTile.matrixPoint[2].Z),
                new Vector3(foldingTile.matrixPoint[3].X, foldingTile.matrixPoint[3].Y, foldingTile.matrixPoint[3].Z)
            };

            for (int x = 0; x < 4; x++)
            {
                vectors[x].X *= foldingTile.Size.X;
                vectors[x].Z *= foldingTile.Size.Y;
                if (foldingTile.SlantDepth) vectors[x].Y *= foldingTile.Size.Y;
                else vectors[x].Y *= foldingTile.Size.X;
                vectors[x] += foldingTile.position;
            }

            float minX = vectors[0].X, minY = vectors[0].Y;
            float maxX = vectors[0].X, maxY = vectors[0].Y;

            for (int x = 0; x < vectors.Length; x++)
            {
                if (vectors[x].X < minX) minX = vectors[x].X;
                if (vectors[x].Y < minY) minY = vectors[x].Y;
                if (vectors[x].X > maxX) maxX = vectors[x].X;
                if (vectors[x].Y > maxY) maxY = vectors[x].Y;
            }

            GL.LineWidth(2.5f);
            GL.Color3(color);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(minX, -minY); 
            GL.Vertex2(maxX, -maxY);
            GL.End();
        }
    }
}
