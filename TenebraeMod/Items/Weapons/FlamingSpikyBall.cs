using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
 
namespace TenebraeMod.Items.Weapons
{
    public class FlamingSpikyBall : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Flaming Spiky Ball");
            Tooltip.SetDefault("'Flaming splinters!'");
        }

        public override void SetDefaults() {
            item.throwing = true;
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
            item.value = 2;
            item.rare = 0;
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("FlamingSpikyBallProjectile");
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<WoodenSpikyBall>(),10);
            recipe.AddIngredient(ItemID.Torch);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this,10);
            recipe.AddRecipe();
        }
    }

    internal class FlamingSpikyBallProjectile : ModProjectile {
		public override string Texture => "TenebraeMod/Items/Weapons/FlamingSpikyBall";
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Flaming Spiky Ball");
		}

		public override void SetDefaults() {
			projectile.throwing = true;
			projectile.aiStyle = -1;
			projectile.width = 14;
			projectile.height = 14;
			projectile.penetrate = 4;
			projectile.friendly = true;
			projectile.tileCollide = true;
            projectile.light = 0.8f;
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

            Dust dust = Main.dust[Dust.NewDust(projectile.position,projectile.width,projectile.height,DustID.Fire,Scale: 1.5f)];
            dust.noGravity = true;
            dust.velocity.Y = -4;
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

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.AddBuff(BuffID.OnFire,300);
        }

        public override void OnHitPvp(Player target, int damage, bool crit) {
            target.AddBuff(BuffID.OnFire,300);
        }
    }
}