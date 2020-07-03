using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace TenebraeMod.Items.Weapons
{
    public class WoodenSpikyBall : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Wooden Spiky Ball");
            Tooltip.SetDefault("'Ouch, splinters!'");
        }

        public override void SetDefaults() {
            item.ranged = true;
            item.maxStack = 999;
            item.consumable = true;
            item.damage = 5;
            item.width = 14;
            item.height = 14;
            item.useTime = 15;
            item.useAnimation = 15;
            item.noUseGraphic = true;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = 1;
            item.rare = 0;
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("WoodenSpikyBallProjectile");
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("Wood");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this,25);
            recipe.AddRecipe();
        }
    }

    internal class WoodenSpikyBallProjectile : ModProjectile {
		public override string Texture => "TenebraeMod/Items/Weapons/WoodenSpikyBall";
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Wooden Spiky Ball");
		}

		public override void SetDefaults() {
			projectile.ranged = true;
			projectile.aiStyle = -1;
			projectile.width = 14;
			projectile.height = 14;
			projectile.penetrate = 4;
			projectile.friendly = true;
			projectile.tileCollide = true;
		}

        public override void AI() {
            projectile.ai[0] += 1f;
			if (projectile.ai[0] > 5f)
			{
				projectile.ai[0] = 5f;
				if (projectile.velocity.Y == 0f && projectile.velocity.X != 0f)
				{
					projectile.velocity.X = projectile.velocity.X * 0.97f;
					if ((double)projectile.velocity.X > -0.01 && (double)projectile.velocity.X < 0.01)
					{
						projectile.velocity.X = 0f;
						projectile.netUpdate = true;
					}
				}
				projectile.velocity.Y = projectile.velocity.Y + 0.2f;
			}
			projectile.rotation += projectile.velocity.X * 0.1f;
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            if (projectile.velocity.X != oldVelocity.X) {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y) {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            projectile.velocity.Y += 0.2f;
            projectile.velocity *= 0.5f;
            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough) {
            fallThrough = false;
            return true;
        }
    }
}