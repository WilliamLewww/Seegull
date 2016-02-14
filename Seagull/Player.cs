using OpenTK;
using OpenTK.Input;
using System;
using System.Drawing;

namespace Seagull
{
    class Player
    {
        public Vector3d position;
        public double facing, pitch;

        RectangularPrismObject camera;
        ControlledPhysicsEntity body;

        public Vector3d Position
        {
            get { return position; }
            set { position = value; }
        }

        public double Facing
        {
            get { return facing; }
            set { facing = Utils.GetReferenceAngle(value); }
        }

        public double Pitch
        {
            get { return pitch; }
            set { pitch = Utils.LimitAngle(value, -90, 90); }
        }

        public Vector3d Forwards
        {
            get
            {
                float r = MathHelper.DegreesToRadians((float)Facing);
                return new Vector3d(-Math.Sin(r), 0, -Math.Cos(r));
            }
        }

        public Vector3d Sideways
        {
            get
            {
                float r = MathHelper.DegreesToRadians((float)Facing);
                return new Vector3d(-Math.Cos(r), 0, Math.Sin(r));
            }
        }

        public void LoadContent()
        {
            position = new Vector3d(200, 200, 200);
            facing = 180;
            pitch = 0;

            camera = new RectangularPrismObject(new Vector3((float)position.X, (float)position.Y, (float)position.Z), new Vector3(10, 10, 10));
            body = new ControlledPhysicsEntity(new Vector3(25, 50, 25), new Vector3(2.5f, 50, 2.5f));
        }

        public void Update(double gameTime)
        {
            if (Input.KeyDown(Key.W))
                body.position += Forwards * 100 * gameTime;

            if (Input.KeyDown(Key.S))
                body.position -= Forwards * 100 * gameTime;

            if (Input.KeyDown(Key.A))
                body.position += Sideways * 100 * gameTime;

            if (Input.KeyDown(Key.D))
                body.position -= Sideways * 100 * gameTime;

            Facing += ((Mouse.GetCursorState().X - Game1.screenX) - (Game1.screenWidth / 2)) * -.025f;
            Pitch += ((Mouse.GetCursorState().Y - Game1.screenY) - (Game1.screenHeight / 2)) * -.025f;

            if (body.onGround == true)
            {
                if (Input.KeyDown(Key.Space))
                {
                    body.velocityY = 3;
                    body.onGround = false;
                    body.jumped = true;
                }
            }

            body.Update(gameTime, Environment.tileList.ToArray(), Facing);

            camera.position = new Vector3((float)body.position.X - (camera.Size.X - body.Size.X) / 2, (float)body.position.Y + body.Size.Y, (float)body.position.Z + (camera.Size.Z - body.Size.Z) / 2);
            position = new Vector3d(camera.position.X + camera.Size.X / 2, camera.position.Y + camera.Size.Y / 2, camera.position.Z - camera.Size.Z / 2);
        }

        public void GetTheoreticalPoints()
        {
            body.GetTheoreticalPoints();
        }

        public void Draw()
        {
            camera.Draw(Color.Red, Facing, Pitch);
            body.Draw(Color.MidnightBlue);
        }
    }
}
