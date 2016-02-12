using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;

namespace Seagull
{
    class Game1 : GameWindow
    {
        Main main = new Main();

        public static int screenWidth, screenHeight, screenX, screenY;

        public Game1(int width, int height) : base(width, height)
        {
            screenWidth = Width;
            screenHeight = Height;

            screenX = X;
            screenY = Y;
        }

        protected override void OnResize(EventArgs e)
        {
            screenWidth = Width;
            screenHeight = Height;

            screenX = X;
            screenY = Y;

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            float FOVradians = MathHelper.DegreesToRadians(45);
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(FOVradians, Width / Height, 1, 4000);
            GL.MultMatrix(ref perspective);

            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            main.LoadContent();

            Input.Initialize(this);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            GL.ClearColor(Color.Black);

            CenterMouse();
            CursorVisible = false;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Input.KeyDown(Key.Escape))
                Exit();

            if (Input.KeyPress(Key.F12))
            {
                if (WindowState == WindowState.Normal)
                    WindowState = WindowState.Fullscreen;
                else
                    WindowState = WindowState.Normal;
            }

            main.Update(e.Time);

            CenterMouse();

            Input.Update();
        }

        private void CenterMouse()
        {
            Point p = new Point(Width / 2, Height / 2);
            OpenTK.Input.Mouse.SetPosition(p.X + X, p.Y + Y);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            main.Draw();

            SwapBuffers();
        }
    }
}
