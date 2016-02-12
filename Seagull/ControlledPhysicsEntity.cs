using OpenTK;
using System;
using System.Drawing;

namespace Seagull
{
    class ControlledPhysicsEntity
    {
        public Vector3d position;
        private Vector3 size;

        public Vector3[] theoreticalPoint = new Vector3[8];
        public Vector3[] actualPoint = new Vector3[8];

        public bool onGround, jumped;
        public float velocityY;

        public double rotation;

        public Vector3 Size { get { return size; } }

        public float Width { get { return size.X; } }
        public float Height { get { return size.Y; } }
        public float Depth { get { return size.Z; } }

        public float X { get { return (float)position.X; } }
        public float Y { get { return (float)position.Y; } }
        public float Z { get { return (float)position.Z; } }

        public float Left { get { return Supplements.GetMinValueVector(actualPoint, 0); } }
        public float Right { get { return Supplements.GetMaxValueVector(actualPoint, 0); } }
        public float Top { get { return Supplements.GetMinValueVector(actualPoint, 1); ; } }
        public float Bottom { get { return Supplements.GetMaxValueVector(actualPoint, 1); ; } }
        public float Front { get { return Supplements.GetMaxValueVector(actualPoint, 2); ; } }
        public float Back { get { return Supplements.GetMinValueVector(actualPoint, 2); ; } }

        public double Rotation { get { return rotation; } }

        public ControlledPhysicsEntity(Vector3 positionArgs, Vector3 sizeArgs)
        {
            position = new Vector3d(positionArgs.X, positionArgs.Y, positionArgs.Z);
            size = sizeArgs;
        }

        public void CheckCollision(RectangularPrismObject[] rectangle)
        {
            bool x = false, y = false, z = false;

            for (int a = 0; a < rectangle.Length; a++)
            {
                if (Left <= rectangle[a].Right &&
                    Right >= rectangle[a].Left)
                    x = true;
                if (Top <= rectangle[a].Bottom &&
                    Bottom >= rectangle[a].Top)
                    y = true;
                if (Front >= rectangle[a].Back &&
                    Back <= rectangle[a].Front)
                    z = true;
            }

            Console.WriteLine(x + ":" + y + ":" + z);
            Console.WriteLine(Top + ":" + Bottom + ":" + rectangle[0].Top + ":" + rectangle[0].Bottom);
        }

        public virtual bool Intersects(RectangularPrismObject rectangle)
        {
            if (Left <= rectangle.Right &&
                Right >= rectangle.Left &&
                Top <= rectangle.Bottom &&
                Bottom >= rectangle.Top &&
                Front >= rectangle.Back &&
                Back <= rectangle.Front)
            {
                return true;
            }

            return false;
        }

        public virtual bool Intersects(FoldingTile tile, out float collisionY)
        {
            float bottomMidpoint;

            if (tile.SlantDepth)
                bottomMidpoint = (Z - tile.Z) + (Width / 2);
            else
                bottomMidpoint = (X - tile.X) + (Width / 2);

            if (Left <= tile.Right &&
                Right >= tile.Left &&
                Top <= tile.Bottom &&
                Bottom >= tile.Top &&
                Front >= tile.Front &&
                Back <= tile.Back)
            {
                if (tile.SlantOpposite) collisionY = tile.Y - (bottomMidpoint * tile.Scale);
                else collisionY = tile.Y + (bottomMidpoint * tile.Scale);
                return true;
            }

            collisionY = -1;
            return false;
        }

        public void Update(double gameTime, RectangularPrismObject[] staticEnvironment, double rotationArgs)
        {
            rotation = rotationArgs;
            position.Y += velocityY;

            if (onGround)
            {
                jumped = false;
                velocityY = 0;
            }
            else
                velocityY -= (float)(9.8 * gameTime);

            onGround = false;

            foreach (RectangularPrismObject rectangleObject in staticEnvironment)
            {
                if (Intersects(rectangleObject))
                {
                    onGround = true;

                    if (jumped == false)
                    {
                        position = new Vector3d(position.X, rectangleObject.Bottom, position.Z);
                    }
                }
            }
        }

        public void Update(double gameTime, FoldingTile[] staticEnvironment, double rotationArgs)
        {
            rotation = rotationArgs;
            position.Y += velocityY;

            if (Input.KeyDown(OpenTK.Input.Key.Space))
                position.Y += 1;
            if (Input.KeyDown(OpenTK.Input.Key.ControlLeft))
                position.Y -= 1;

            if (onGround)
            {
                jumped = false;
                velocityY = 0;
            }
            else
                velocityY -= (float)(9.8 * gameTime);

            onGround = false;

            foreach (FoldingTile foldingTile in staticEnvironment)
            {
                float collisionY;
                if (Intersects(foldingTile, out collisionY))
                {
                    onGround = true;

                    if (jumped == false)
                    {
                        position = new Vector3d(position.X, collisionY, position.Z);
                    }
                }
            }
        }

        public void GetTheoreticalPoints()
        {
            theoreticalPoint[0] = Supplements.GetTheoreticalPoints(Supplements.GetMatrix(Size, new Vector3(X, Y, Z), Rotation), new Vector3(Size.X / 2, 0, Size.Z / 2));
            theoreticalPoint[1] = Supplements.GetTheoreticalPoints(Supplements.GetMatrix(Size, new Vector3(X, Y, Z), Rotation + 90), new Vector3(Size.X / 2, 0, Size.Z / 2));
            theoreticalPoint[2] = Supplements.GetTheoreticalPoints(Supplements.GetMatrix(Size, new Vector3(X, Y, Z), Rotation + 180), new Vector3(Size.X / 2, 0, Size.Z / 2));
            theoreticalPoint[3] = Supplements.GetTheoreticalPoints(Supplements.GetMatrix(Size, new Vector3(X, Y, Z), Rotation + 270), new Vector3(Size.X / 2, 0, Size.Z / 2));
            theoreticalPoint[4] = Supplements.GetTheoreticalPoints(Supplements.GetMatrix(Size, new Vector3(X, Y, Z), Rotation), new Vector3(Size.X / 2, Size.Y, Size.Z / 2));
            theoreticalPoint[5] = Supplements.GetTheoreticalPoints(Supplements.GetMatrix(Size, new Vector3(X, Y, Z), Rotation + 90), new Vector3(Size.X / 2, Size.Y, Size.Z / 2));
            theoreticalPoint[6] = Supplements.GetTheoreticalPoints(Supplements.GetMatrix(Size, new Vector3(X, Y, Z), Rotation + 180), new Vector3(Size.X / 2, Size.Y, Size.Z / 2));
            theoreticalPoint[7] = Supplements.GetTheoreticalPoints(Supplements.GetMatrix(Size, new Vector3(X, Y, Z), Rotation + 270), new Vector3(Size.X / 2, Size.Y, Size.Z / 2));

            for (int x = 0; x < theoreticalPoint.Length; x++)
            {
                actualPoint[x] = new Vector3((float)position.X + theoreticalPoint[x].X + (Size.X / 2), (float)position.Y + theoreticalPoint[x].Y, (float)position.Z + theoreticalPoint[x].Z - (Size.Z / 2));
            }
        }

        public void Draw(Color color)
        {
            SpriteBatch.DrawRectangularPrism(size, new Vector3((float)position.X, (float)position.Y, (float)position.Z), Rotation, color);
            //for (int x = 0; x < 8; x++)
            //{
            //    SpriteBatch.DrawRectangularPrism(new Vector3(1, 1, 1), actualPoint[x], Color.Purple);
            //}
        }
    }
}
