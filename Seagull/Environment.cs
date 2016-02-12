using OpenTK;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Seagull
{
    class Environment
    {
        public static List<RectangularPrismObject> rectangleList = new List<RectangularPrismObject>();
        public static List<FoldingTile> tileList = new List<FoldingTile>();

        public void LoadContent()
        {
            //using (Stream stream = File.Open("data.bin", FileMode.Open))
            //{
            //    BinaryFormatter formatter = new BinaryFormatter();

            //    var foldingTileList = (List<FoldingTile>)formatter.Deserialize(stream);
            //    foreach (FoldingTile foldingTile in foldingTileList)
            //    {
            //        tileList.Add(foldingTile);
            //    }
            //}

            int[] modifier = new int[1];
            modifier[0] = 0;
            int[] modifier2 = new int[1];
            modifier2[0] = 3;

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    tileList.Add(new FoldingTile(new Vector3(15 * x, (25f / 4) * y, 25 * y), 15, 25, true, modifier, .25f));
                }
            }

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    tileList.Add(new FoldingTile(new Vector3(25 * x, (25f / 2) * x, 15 * y), 25, 15, true, modifier2, .5f));
                }
            }

            //for (int x = 0; x < 10; x++)
            //{
            //    for (int y = 0; y < 10; y++)
            //    {
            //        rectangleList.Add(new RectangularPrismObject(new Vector3(25 * x, 0, 25 * y), new Vector3(25, 5, 25)));
            //    }
            //}
        }

        public void Draw()
        {
            foreach (FoldingTile tile in tileList)
                tile.Draw(Color.DarkSlateGray);

            //foreach (RectangularPrismObject rectangle in rectangleList)
            //    rectangle.Draw(Color.SlateGray);
        }
    }
}
