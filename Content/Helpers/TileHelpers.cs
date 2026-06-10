using EasyNPCHousing.Content.Enums;
using Terraria;
using Terraria.ID;

namespace EasyNPCHousing.Content.Helpers
{
    /// <summary>
    /// I dont like this setup because it is very limiting and requires this class to keep track of what is wood/natural. But until I can come up
    /// with a better solution I am using this
    /// </summary>
    public class TileHelpers
    {
        //Default set of common wood types (its honestly all of them, other than modded ones)
        private static bool IsWood(ushort tileId)
        {
            return tileId == TileID.WoodBlock ||
             tileId == TileID.RichMahogany ||
             tileId == TileID.Ebonwood ||
             tileId == TileID.Shadewood ||
             tileId == TileID.Pearlwood ||
             tileId == TileID.BorealWood ||
             tileId == TileID.PalmWood ||
             tileId == TileID.DynastyWood ||
             tileId == TileID.SpookyWood ||
             tileId == TileID.AshWood;
        }

        //Default set of common natural block types.
        private static bool IsNatural(ushort tileId)
        {
            return tileId == TileID.Dirt ||
             tileId == TileID.Grass ||
             tileId == TileID.JungleGrass ||
             tileId == TileID.Stone ||
             tileId == TileID.Sand ||
             tileId == TileID.Mud ||
             tileId == TileID.HardenedSand ||
             tileId == TileID.Sandstone ||
             tileId == TileID.Bamboo ||
             tileId == TileID.Cactus ||
             tileId == TileID.Pumpkins ||
             tileId == TileID.SnowBlock ||
             tileId == TileID.IceBlock;
        }

        /// <summary>
        /// Checks if a tile is either wood or natural
        /// </summary>
        private static bool IsWoodOrNatural(ushort tileId)
        {
            return IsWood(tileId) || IsNatural(tileId);
        }

        /// <summary>
        /// Checks if a tile is allowed to be replaced based on current config settings
        /// </summary>
        public static bool IsTileAllowed(Tile tile, EasyNPCHousingConfig config)
        {
            if(!tile.HasTile)
                return true;

            ushort type = tile.TileType;
            
            return config.OverwriteMode switch
            {
                OverwriteableBlockGroups.None => false,
                OverwriteableBlockGroups.WoodOnly => IsWood(type),
                OverwriteableBlockGroups.NaturalBlocks => IsNatural(type),
                OverwriteableBlockGroups.WoodAndNaturalBlocks => IsWoodOrNatural(type),
                OverwriteableBlockGroups.Everything => true,
                OverwriteableBlockGroups.Custom => config.CustomAllowedTiles.Contains(type),
                _ => false
            };
        }
    }
}
