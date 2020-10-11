using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Items.Blocks
{
	public class NeoniteShard : ModItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("'Glows with neon red energy'");
        }

        public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.maxStack = 999;
			item.consumable = true;
			item.createTile = mod.TileType("Neonite");
			item.width = 24;
			item.height = 16;
			item.value = Item.sellPrice(0, 0, 10, 0);
			item.rare = ItemRarityID.Orange;
		}
	}
}