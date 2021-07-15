using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace TenebraeMod.Items.Weapons.Mage
{
    public class NebulaVaporwave : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'/music plays'");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 40;
            item.value = 100000;
            item.rare = ItemRarityID.Red;
            item.damage = 150;
            item.magic = true;
            item.knockBack = 4f;
            item.holdStyle = 0;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.shootSpeed = 10f;
            item.shoot = mod.ProjectileType("Vaporwave");
            item.noMelee = true;
            item.mana = 20;
			item.useAnimation = 20;
			item.useTime = 5;
			item.UseSound = SoundID.Item34;
			item.reuseDelay = 25;
            item.autoReuse = true;
        }
public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 2 + Main.rand.Next(1);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(25)); 
			
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentNebula, 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}