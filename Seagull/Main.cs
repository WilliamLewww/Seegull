using OpenTK.Graphics.OpenGL;

namespace Seagull
{
    class Main
    {
        Player player;
        Environment environment;

        public void LoadContent()
        {
            player = new Player();
            player.LoadContent();

            environment = new Environment();
            environment.LoadContent();
        }

        public void Update(double gameTime)
        {
            player.Update(gameTime);
        }

        public void Draw()
        {
            GL.PushMatrix();

            GL.Rotate(-player.Pitch, 1, 0, 0);
            GL.Rotate(-player.Facing, 0, 1, 0);
            GL.Translate(-player.Position);

            player.Draw();
            environment.Draw();

            GL.PopMatrix();

            GL.PushMatrix();
            player.GetTheoreticalPoints();
            GL.PopMatrix();
        }
    }
}
