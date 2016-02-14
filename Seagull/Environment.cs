using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Seagull
{
    class Environment
    {
        public static List<FoldingTile> tileList = new List<FoldingTile>();
        public static List<RectangularPrismObject> rectangleList = new List<RectangularPrismObject>();

        public void LoadContent()
        {
            using (StreamReader file = File.OpenText("data.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                tileList = (List<FoldingTile>)serializer.Deserialize(file, typeof(List<FoldingTile>));
            }
        }

        public void Draw()
        {
            foreach (FoldingTile tile in tileList)
                tile.Draw(Color.DarkSlateGray);

            foreach (RectangularPrismObject rectangle in rectangleList)
                rectangle.Draw(Color.SlateGray);
        }
    }
}
