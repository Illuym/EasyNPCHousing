using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace EasyNPCHousing.Content
{
    public class EasyNPCHousingConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [ReloadRequired]
        [DefaultValue(false)]
        public bool ShouldOverwriteBlocks;
    }
}
