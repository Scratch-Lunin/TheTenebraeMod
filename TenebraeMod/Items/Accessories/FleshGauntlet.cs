using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Accessories
{
    public class FleshGauntlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases melee knockback"+"\n12% increased melee speed"+"\n8% increased damage"+"\n8% increased critical strike chance"+"\nEnables autoswing for melee weapons"+"\nEnemies are more likely to target you"+"\nAttacks have a 10% chance to inflict Berserked for 7 seconds");
        }

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 42;
            item.value = 200000;
            item.rare = ItemRarityID.Lime;
            item.defense = 6;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.kbGlove = true;
		    player.meleeSpeed += 0.12f;
		    player.aggro += 400;
            player.meleeCrit += 8;
            player.rangedCrit += 8;
            player.magicCrit += 8;
            player.thrownCrit += 8;
            player.allDamage += 0.08f;
            player.GetModPlayer<TenebraeModPlayer>().fleshGauntlet=true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PowerGlove);
            recipe.AddIngredient(ItemID.FleshKnuckles);
            recipe.AddIngredient(ItemID.DestroyerEmblem);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}