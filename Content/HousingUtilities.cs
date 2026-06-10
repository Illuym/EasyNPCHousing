using EasyNPCHousing.Content.Items;
using EasyNPCHousing.Content.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace EasyNPCHousing.Content
{
    /// <summary>
    /// Handles various tasks related to the house (drawing tile previews and checking if a house can be built on any given tile.
    /// </summary>
    public class HousingUtilities : ModSystem
    {
        public static bool DrawPreview = false;
        public static bool CanBuildHouse = true;
        public static Point PreviewTilePos;

        private static int width = 5;
        private static int height = 12;

        public override void PostDrawTiles()
        {
            if (!DrawPreview)
                return;

            if (Main.LocalPlayer.HeldItem.type != ModContent.ItemType<EasyNPCHousingWand>())
                return;

            var config = ModContent.GetInstance<EasyNPCHousingConfig>();

            CanBuildHouse = CheckIfCanBuildHouse(config);

            Main.spriteBatch.Begin(
                    SpriteSortMode.Deferred,
                    BlendState.AlphaBlend,
                    Main.DefaultSamplerState,
                    DepthStencilState.None,
                    RasterizerState.CullCounterClockwise,
                    null,
                    Main.GameViewMatrix.TransformationMatrix                                    
            );

            DrawHousePreview(config);
           
            Main.spriteBatch.End();
            DrawPreview = false;
        }

        private void DrawHousePreview(EasyNPCHousingConfig config)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int tileX = PreviewTilePos.X + i;
                    int tileY = PreviewTilePos.Y - j;

                    Tile tile = Main.tile[tileX, tileY];

                    bool canPlaceHere = !tile.HasTile || TileHelpers.IsTileAllowed(tile, config);

                    Color color = !tile.HasTile ? Color.Green : canPlaceHere ? Color.YellowGreen : Color.Red;

                    Vector2 screenPos = new Vector2(tileX * 16f, tileY * 16f) - Main.screenPosition;
                    Main.spriteBatch.Draw(
                        TextureAssets.MagicPixel.Value,
                        new Rectangle((int)screenPos.X, (int)screenPos.Y, 16, 16),
                        color * 0.3f
                    );
                }
            }
        }

        private static bool CheckIfCanBuildHouse(EasyNPCHousingConfig config)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int tileX = PreviewTilePos.X + i;
                    int tileY = PreviewTilePos.Y - j;

                    Tile tile = Main.tile[tileX, tileY];

                    if (!TileHelpers.IsTileAllowed(tile, config))
                        return false; 
                }
            }
            return true;
        }
    }
}