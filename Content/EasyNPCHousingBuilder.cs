using EasyNPCHousing.Content.Helpers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EasyNPCHousing.Content
{
    public static class EasyNPCHousingBuilder
    {
        private static int width = 5;
        private static int height = 12;
        /// <summary>
        /// Starts the process to create an NPC house where the moust is currently hovering
        /// </summary>
        public static void BuildNPCHouse(int originX, int originY)
        {               
            int topY = originY - (height - 1);
           
            BuildFloor(originX, originY, width);
            BuildCeiling(originX, topY, width);
            BuildWalls(originX, originY, width, height);
            BuildBackgroundWalls(originX, originY, width, height);
            PlaceFurniture(originX, originY, topY, width);                           
        }

        /// <summary>
        /// Builds the floor of the NPC house
        /// </summary>
        private static void BuildFloor(int x, int y, int width)
        {
            for (int i = 0; i < width; i++)
            {
                ushort tileType = (i % 2 == 0)
                    ? TileID.WoodBlock
                    : TileID.Platforms;

                TryPlace(x + i, y, false, tileType);
            }
        }

        /// <summary>
        /// Builds the ceiling of the NPC house
        /// </summary>
        private static void BuildCeiling(int x, int topY, int width)
        {
            for (int i = 0; i < width; i++)
            {
                TryPlace(x + i, topY, false, TileID.WoodBlock);
            }
        }

        /// <summary>
        /// Builds the walls of the NPC house
        /// </summary>
        private static void BuildWalls(int x, int originY, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                TryPlace(x, originY - i, false, TileID.WoodBlock);
                TryPlace(x + width - 1, originY - i, false, TileID.WoodBlock);
            }
        }

        /// <summary>
        /// Builds the background walls of the npc house
        /// </summary>
        private static void BuildBackgroundWalls(int x, int originY, int width, int height)
        {
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    WorldGen.PlaceWall(x + i, originY - j, WallID.Wood);
                }
            }
        }

        /// <summary>
        /// Places the furniture in the NPC house
        /// </summary>
        private static void PlaceFurniture(int x, int originY, int topY, int width)
        {
            TryPlace(x + 1, originY - 1, true, TileID.Chairs);
            TryPlace(x + 2, originY - 1, true, TileID.WorkBenches);

            TryPlace(x + 1, topY + 1, true, TileID.Torches);
            TryPlace(x + width - 2, topY + 1, true, TileID.Torches);
        }

        /// <summary>
        /// Attempts to kill (if there is one) and place a tile at the designated spot in the world
        /// </summary>
        private static bool TryPlace(int x, int y, bool isObject, ushort type)
        {
            var config = ModContent.GetInstance<EasyNPCHousingConfig>();
            Tile tile = Main.tile[x, y];

            if (tile.HasTile && !TileHelpers.IsTileAllowed(tile, config))
                return false;

            if(tile.HasTile)
                WorldGen.KillTile(x, y);

            return isObject
                ? WorldGen.PlaceObject(x, y, type, style: 0, direction: 1)
                : WorldGen.PlaceTile(x, y, type, style: 0);            
        }
    }
}