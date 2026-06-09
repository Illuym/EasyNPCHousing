using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EasyNPCHousing.Content.Items
{
    /// <summary>
    /// This class holds properties for the NPC Housing item itself
    /// </summary>
    public class EasyNPCHousingWand : ModItem
    {
        //Default item price of the item (50 wood)
        private int defaultItemPrice = 50;

        private static bool holdingItem =>
            Main.LocalPlayer.HeldItem.type == ModContent.ItemType<EasyNPCHousingWand>();

        /// <summary>
        /// Default properties for the item
        /// </summary>
        public override void SetDefaults() 
        {
            Item.height = 32;
            Item.width = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.maxStack = 9999;
            Item.value = Item.buyPrice(0, 0, 5, 0);
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;                            
            Item.consumable = true;        
        }

        /// <summary>
        /// Adds the recipe for the item
        /// </summary>
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, defaultItemPrice)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        /// <summary>
        /// When the item is used, determine whehter this is a singleplayer/multiplayer world
        /// and call to create the house accordingly./> 
        /// </summary>
        public override bool? UseItem(Player player)
        {
            if(Main.myPlayer != player.whoAmI)
                   return true;

            if(!ModContent.GetInstance<EasyNPCHousingConfig>().ShouldOverwriteBlocks && !HousePreview.CanBuildHouse)
            {
                Main.NewText("You cannot build there. Some blocks will be overwritten. If you wish to be able to overwrite blocks please change this in the config.");
                Item.stack += 1;
                return true;
            }
            
            Point tilePos = Main.MouseWorld.ToTileCoordinates();

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write(tilePos.X);
                packet.Write(tilePos.Y);
                packet.Send();
            }
            else if(Main.netMode == NetmodeID.SinglePlayer)
            {
                EasyNPCHousingBuilder.BuildNPCHouse(tilePos.X, tilePos.Y);
            }             
            return true;           
        }

        public override void HoldItem(Player player)
        {
            if (Main.myPlayer != player.whoAmI)
                return;

            if(holdingItem)
            {
                HousePreview.PreviewTilePos = Main.MouseWorld.ToTileCoordinates();
                HousePreview.DrawPreview = true;
            }
        }
    }
}