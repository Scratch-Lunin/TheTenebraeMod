using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TenebraeMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using System;

namespace TenebraeMod.Items.Weapons.Melee
{
    public class TrueEnchantedSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a spread of sword beams that home towards the cursor" + "\n'Truly the most heroic of swords'");
        }

        public override void SetDefaults()
        {
            item.width = 56;
            item.height = 56;
            item.damage = 150;
            item.knockBack = 7f;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 20;
            item.useTime = 40;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.shoot = mod.ProjectileType("TrueEnchantedSwordBeam");
            item.shootSpeed = 8f;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = ItemRarityID.Cyan;
            // Set other item.X values here
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)

        {
            for (int i = 0; i < Main.rand.Next(3, 5); i++) //replace 3 with however many projectiles you like

            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(35)) * Main.rand.NextFloat(0.8f, 1.2f); /*12 is the spread in degrees, 
				although like with Set Spread it's technically a 24 degree spread due to the fact that it's randomly between 12 degrees above and 12 degrees below your cursor.*/
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI); //create the projectile
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.EnchantedSword);
            recipe.AddIngredient(ItemID.BrokenHeroSword);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}