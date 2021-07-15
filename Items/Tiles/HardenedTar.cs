using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Items.Tiles
{
    public class HardenedTar : ModItem
    {
        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.maxStack = 999;
			item.consumable = true;
            item.createTile = mod.TileType("HardenedTar");
			item.width = 16;
			item.height = 16;
			item.value = 0;
        }
    }
}