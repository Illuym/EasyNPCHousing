using Terraria;
using Terraria.ID;

namespace EasyNPCHousing.Content
{
    public class EasyNPCHousingBuilder
    {
        /// <summary>
        /// Starts the process to create an NPC house where the moust is currently hovering
        /// </summary>
        public static void BuildNPCHouse(int originX, int originY)
        {      
            int width = 5;
            int height = 12;
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
                if (i % 2 == 0)
                    WorldGen.PlaceTile(x + i, y, TileID.WoodBlock);            
                else               
                    WorldGen.PlaceTile(x + i, y, TileID.Platforms, style: 0);             
            }
        }

        /// <summary>
        /// Builds the ceiling of the NPC house
        /// </summary>
        private static void BuildCeiling(int x, int topY, int width)
        {
            for (int i = 0; i < width; i++)
            {
                WorldGen.PlaceTile(x + i, topY, TileID.WoodBlock);
            }
        }

        /// <summary>
        /// Builds the walls of the NPC house
        /// </summary>
        private static void BuildWalls(int x, int originY, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                WorldGen.PlaceTile(x, originY - i, TileID.WoodBlock);
                WorldGen.PlaceTile(x + width - 1, originY - i, TileID.WoodBlock);
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
            WorldGen.PlaceObject(x + 1, originY - 1, TileID.Chairs, style: 0, direction: 1);
            WorldGen.PlaceObject(x + 2, originY - 1, TileID.WorkBenches, style: 0);

            WorldGen.PlaceObject(x + 1, topY + 1, TileID.Torches, style: 0);
            WorldGen.PlaceObject(x + width - 2, topY + 1, TileID.Torches, style: 0);
        }
    }
}