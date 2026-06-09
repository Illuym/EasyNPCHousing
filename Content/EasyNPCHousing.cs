using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EasyNPCHousing.Content
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class EasyNPCHousing : Mod
	{
        /// <summary>
        /// Create the house on the server and update the area for all clients
        /// </summary>
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            int x = reader.ReadInt32();
            int y = reader.ReadInt32();

            if (Main.netMode == NetmodeID.Server)
            {
                EasyNPCHousingBuilder.BuildNPCHouse(x, y);
                NetMessage.SendTileSquare(-1, x, y, 100);
            }
        }     
    }
}