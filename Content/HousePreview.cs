using EasyNPCHousing.Content.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace EasyNPCHousing.Content
{
    public class HousePreview : ModSystem
    {
        public static bool DrawPreview = false;
        public static bool CanBuildHouse = true;
        public static Point PreviewTilePos;

        private static int width = 5;
        private static int height = 12;
        private static Color color;
        private static bool occupied;

        public override void PostDrawTiles()
        {
            if (!DrawPreview)
                return;

            if (Main.LocalPlayer.HeldItem.type != ModContent.ItemType<EasyNPCHousingWand>())
                return;

            CanBuildHouse = true;

            Main.spriteBatch.Begin(
                    SpriteSortMode.Deferred,
                    BlendState.AlphaBlend,
                    Main.DefaultSamplerState,
                    DepthStencilState.None,
                    RasterizerState.CullCounterClockwise,
                    null,
                    Main.GameViewMatrix.TransformationMatrix                                    
                );                   
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int tileX = PreviewTilePos.X + i;
                    int tileY = PreviewTilePos.Y - j;

                    Vector2 screenPos = new Vector2(tileX * 16f, tileY * 16f) - Main.screenPosition;
                    color = Main.tile[tileX, tileY].HasTile ? Color.Red : Color.Green;
                    occupied = Main.tile[tileX, tileY].HasTile;
                    CanBuildHouse &= !occupied;
                    Main.spriteBatch.Draw(
                        TextureAssets.MagicPixel.Value,
                        new Rectangle((int)screenPos.X, (int)screenPos.Y, 16, 16),
                        color * 0.3f
                    );
                }
            }
            Main.spriteBatch.End();
            DrawPreview = false;
        }
    }
}