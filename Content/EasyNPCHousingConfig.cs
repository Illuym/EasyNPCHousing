using EasyNPCHousing.Content.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace EasyNPCHousing.Content
{
    public class EasyNPCHousingConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [ReloadRequired]
        [DefaultValue(OverwriteableBlockGroups.None)]
        public OverwriteableBlockGroups OverwriteMode;

        [ReloadRequired]
        [Range(1, 6000)]
        public List<int> CustomAllowedTiles;
    }
}
