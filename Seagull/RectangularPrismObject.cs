using OpenTK;
using System;
using System.Drawing;

namespace Seagull
{
    [Serializable()]
    class RectangularPrismObject
    {
        public Vector3 position;
        private Vector3 size;
        private Color colorProperty;

        public Vector3 Size { get { return size; } }
        public Color ColorProperty { get { return colorProperty; } }

        public float Width { get { return size.X; } }
        public float Height { get { return size.Y; } }
        public float Depth { get { return size.Z; } }

        public float X { get { return position.X; } }
        public float Y { get { return position.Y; } }
        public float Z { get { return position.Z; } }

        public float Left { get { return X; } }
        public float Right { get { return X + Width; } }
        public float Top { get { return Y; } }
        public float Bottom { get { return Y + Height; } }
        public float Front { get { return Z; } }
        public float Back { get { return Z - Depth; } }

        public RectangularPrismObject(Vector3 positionArgs, Vector3 sizeArgs)
        {
            position = positionArgs;
            size = sizeArgs;
        }

        public RectangularPrismObject(Vector3 positionArgs, Vector3 sizeArgs, Color colorArgs)
        {
            position = positionArgs;
            size = sizeArgs;
            colorProperty = colorArgs;
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

        public virtual void Draw() { SpriteBatch.DrawRectangularPrism(Size, position, colorProperty); }
        public virtual void Draw(Color color) { SpriteBatch.DrawRectangularPrism(Size, position, color); }
        public virtual void Draw(Color color, double rotationX, double rotationY) { SpriteBatch.DrawRectangularPrism(Size, position, rotationX, rotationY, color); }
    }
}
