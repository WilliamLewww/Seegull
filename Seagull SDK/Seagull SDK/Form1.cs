using Newtonsoft.Json;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Seagull_SDK
{
    public partial class Form1 : Form
    {
        Main main = new Main();
        public static int screenWidth, screenHeight;

        public Form1()
        {
            InitializeComponent();

            screenWidth = glControl1.Width;
            screenHeight = glControl1.Height;
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            glControl1.Paint += GlControl1_Paint;
        }

        private void GlControl1_Paint(object sender, PaintEventArgs e)
        {
            glControl1.MakeCurrent();
            GL.ClearColor(Color.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            SpriteBatchSDK.Begin(screenWidth, screenHeight);
            main.DrawTop();

            glControl1.SwapBuffers();
        }

        private void glControl2_Load(object sender, EventArgs e)
        {
            glControl2.Paint += GlControl2_Paint;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Folding Tile")
            {
                Main.foldingTileList.Add(new FoldingTile(new Vector3(0, 0, 0), 100, 100));
                Main.selectedTile = Main.foldingTileList[Main.foldingTileList.Count - 1];
                textBox1.Text = Main.selectedTile.X.ToString();
                textBox2.Text = Main.selectedTile.Z.ToString();
                textBox3.Text = Main.selectedTile.Y.ToString();
                textBox4.Text = Main.selectedTile.Size.X.ToString();
                textBox5.Text = Main.selectedTile.Size.Y.ToString();
                glControl1.Invalidate();
                glControl2.Invalidate();
            }
            if (comboBox1.Text == "Rectangular Prism")
            {
                Main.rectangleList.Add(new RectangularPrismObject(new Vector3(0, 0, 0), new Vector3(100, 100, 100)));
                glControl1.Invalidate();
                glControl2.Invalidate();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            float.TryParse(textBox1.Text, out value);
            Main.selectedTile.position.X = value;
            glControl1.Invalidate();
            glControl2.Invalidate();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            float.TryParse(textBox2.Text, out value);
            Main.selectedTile.position.Z = value;
            glControl1.Invalidate();
            glControl2.Invalidate();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            float.TryParse(textBox3.Text, out value);
            Main.selectedTile.position.Y = value;
            glControl1.Invalidate();
            glControl2.Invalidate();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            float.TryParse(textBox4.Text, out value);
            Main.selectedTile.Size = new Vector2(value, Main.selectedTile.Size.Y);
            glControl1.Invalidate();
            glControl2.Invalidate();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            float value = 0;
            float.TryParse(textBox5.Text, out value);
            Main.selectedTile.Size = new Vector2(Main.selectedTile.Size.X, value);
            glControl1.Invalidate();
            glControl2.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float[] original = new float[4];
            float value = 0;
            float.TryParse(textBox6.Text, out value);
            switch (comboBox2.Text)
            {
                case "Up: Forward":
                    Main.selectedTile.TranslateUp(0, original, value);
                    break;
                case "Up: Back":
                    Main.selectedTile.TranslateUp(1, original, value);
                    break;
                case "Up: Left":
                    Main.selectedTile.TranslateUp(3, original, value);
                    break;
                case "Up: Right":
                    Main.selectedTile.TranslateUp(2, original, value);
                    break;
                case "Down: Forward":
                    Main.selectedTile.TranslateDown(0, original, value);
                    break;
                case "Down: Back":
                    Main.selectedTile.TranslateDown(1, original, value);
                    break;
                case "Down: Left":
                    Main.selectedTile.TranslateDown(3, original, value);
                    break;
                case "Down: Right":
                    Main.selectedTile.TranslateDown(2, original, value);
                    break;
            }
            glControl1.Invalidate();
            glControl2.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            File.WriteAllText("data.json", JsonConvert.SerializeObject(Main.foldingTileList));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Main.foldingTileList.Clear();
            Main.rectangleList.Clear();

            using (StreamReader file = File.OpenText("data.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                Main.foldingTileList = (List<FoldingTile>)serializer.Deserialize(file, typeof(List<FoldingTile>));
            }

            glControl1.Invalidate();
            glControl2.Invalidate();
        }

        private void GlControl2_Paint(object sender, PaintEventArgs e)
        {
            glControl2.MakeCurrent();
            GL.ClearColor(Color.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            SpriteBatchSDK.Begin(screenWidth, screenHeight);
            main.DrawSide();

            glControl2.SwapBuffers();
        }
    }
}
