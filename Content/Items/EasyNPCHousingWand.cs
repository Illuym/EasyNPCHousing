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
        /// When the item is used, call <see cref="EasyNPCHousingBuilder.BuildNPCHouse(int, int)"/> 
        /// to build the house where the mouse is currently hovering
        /// </summary>
        public override bool? UseItem(Player player)
        {
            Point tilePos = Main.MouseWorld.ToTileCoordinates();
            EasyNPCHousingBuilder.BuildNPCHouse(tilePos.X, tilePos.Y);
            return true;
        }
    }
}