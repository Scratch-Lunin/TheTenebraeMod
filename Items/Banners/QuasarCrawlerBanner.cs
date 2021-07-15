using TenebraeMod.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Banners
{
    class QuasarCrawlerBanner : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Quasared Crawler Banner");
        }
        public override void SetDefaults()
        {
            item.width = 6;
            item.height = 14;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.rare = ItemRarityID.Blue;
            item.value = Item.buyPrice(0, 0, 10, 0);
            item.createTile = TileType<MonsterBanner>();
            item.placeStyle = 0;
        }
    }
}
