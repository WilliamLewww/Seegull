using System.Collections.Generic;
using System.Drawing;

namespace Seagull_SDK
{
    class Main
    {
        public static List<FoldingTile> foldingTileList = new List<FoldingTile>();
        public static List<RectangularPrismObject> rectangleList = new List<RectangularPrismObject>();

        public static FoldingTile selectedTile;

        public void DrawTop()
        {
            foreach (FoldingTile foldingTile in foldingTileList)
            {
                if (foldingTile == selectedTile)
                    SpriteBatchSDK.DrawFoldingTileTop(foldingTile, Color.Red);
                else
                    SpriteBatchSDK.DrawFoldingTileTop(foldingTile, Color.Black);
            }
        }

        public void DrawFront()
        {
            foreach (FoldingTile foldingTile in foldingTileList)
            {
                if (foldingTile == selectedTile)
                    SpriteBatchSDK.DrawFoldingTileFront(foldingTile, Color.Red);
                else
                    SpriteBatchSDK.DrawFoldingTileFront(foldingTile, Color.Black);
            }
        }

        public void DrawSide()
        {
            foreach (FoldingTile foldingTile in foldingTileList)
            {
                if (foldingTile == selectedTile)
                    SpriteBatchSDK.DrawFoldingTileSide(foldingTile, Color.Red);
                else
                    SpriteBatchSDK.DrawFoldingTileSide(foldingTile, Color.Black);
            }
        }
    }
}
