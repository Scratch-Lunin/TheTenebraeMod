using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Items
{
	public class DebugPlayerHolyflames : ModItem
	{
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A potion for debugging the debuff Holy Flames on the player because for some reason the debuff won't appear in HERO's Mod");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.width = 48;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = ItemRarityID.Orange;
            item.value = Item.buyPrice(gold: 1);
            item.buffType = ModContent.BuffType<Buffs.HolyFlames>();
            item.buffTime = 3600;
        }
    }
}