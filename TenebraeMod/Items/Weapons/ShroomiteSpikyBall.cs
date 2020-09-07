using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
 
namespace TenebraeMod.Items.Weapons
{
    public class ShroomiteSpikyBall : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Shroomite Spiky Ball");
        }

        public override void SetDefaults() {
            item.thrown = true;
            item.maxStack = 999;
            item.consumable = true;
            item.damage = 15;
            item.width = 14;
            item.height = 14;
            item.useTime = 15;
            item.useAnimation = 15;
            item.noUseGraphic = true;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = 500;
            item.rare = 8;
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("ShroomiteSpikyBallProjectile");
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpikyBall,75);
            recipe.AddIngredient(ItemID.ShroomiteBar);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this,25);
            recipe.AddRecipe();
        }
    }

    internal class ShroomiteSpikyBallProjectile : ModProjectile {
		public override string Texture => "TenebraeMod/Items/Weapons/ShroomiteSpikyBall";
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Shroomite Spiky Ball");
		}

		public override void SetDefaults() {
			projectile.thrown = true;
			projectile.aiStyle = -1;
			projectile.width = 14;
			projectile.height = 14;
			projectile.penetrate = 1;
            projectile.maxPenetrate = 1;
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
            if (projectile.velocity.Length() < 1f) {
                projectile.alpha = Math.Min(200,projectile.alpha+4);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            if (projectile.velocity.X != oldVelocity.X) {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y) {
                projectile.velocity.Y = -oldVelocity.Y;
                projectile.velocity.Y += 0.2f;
                projectile.velocity.Y *= 0.9f;
            } else {
                projectile.velocity.Y += 0.2f;
            }
            projectile.velocity *= 0.9f;
            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough) {
            fallThrough = false;
            return true;
        }

        public override void Kill(int timeLeft) {
            for (int i=0; i<16; i++) {
                Projectile shot = Main.projectile[Projectile.NewProjectile(projectile.Center,new Vector2(0,-4).RotatedByRandom(Math.PI),ProjectileType<ShroomiteSpikyBallSpore>(),projectile.damage,projectile.knockBack,projectile.owner)];
            }
        }
    }

    internal class ShroomiteSpikyBallSpore : ModProjectile {
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Spore");
		}

		public override void SetDefaults() {
			projectile.thrown = true;
			projectile.aiStyle = -1;
			projectile.width = 12;
			projectile.height = 12;
			projectile.penetrate = 1;
			projectile.friendly = true;
			projectile.tileCollide = false;
            projectile.timeLeft = 256-64;
            projectile.light = 0.5f;
            projectile.alpha = 64;
		}

        public override void AI() {
            projectile.velocity *= 0.99f;
            projectile.rotation = projectile.velocity.ToRotation()+(float)Math.PI/2;
            projectile.alpha++;
        }
    }
}