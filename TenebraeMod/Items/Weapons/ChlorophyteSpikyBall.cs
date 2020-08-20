using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace TenebraeMod.Items.Weapons
{
    public class ChlorophyteSpikyBall : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Chlorophyte Spiky Ball");
        }

        public override void SetDefaults() {
            item.throwing = true;
            item.maxStack = 999;
            item.consumable = true;
            item.damage = 40;
            item.width = 14;
            item.height = 14;
            item.useTime = 15;
            item.useAnimation = 15;
            item.noUseGraphic = true;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = 20;
            item.rare = 7;
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("ChlorophyteSpikyBallProjectile");
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpikyBall,75);
            recipe.AddIngredient(ItemID.ChlorophyteBar);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this,75);
            recipe.AddRecipe();
        }
    }

    internal class ChlorophyteSpikyBallProjectile : ModProjectile {
        private NPC stuckTo;
        private Vector2 stuckToOffset;
		public override string Texture => "TenebraeMod/Items/Weapons/ChlorophyteSpikyBall";
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Chlorophyte Spiky Ball");
		}

		public override void SetDefaults() {
			projectile.throwing = true;
			projectile.aiStyle = -1;
			projectile.width = 14;
			projectile.height = 14;
			projectile.penetrate = 20;
			projectile.friendly = true;
			projectile.tileCollide = false;
            stuckTo = null;
		}

        public override void AI() {
            if (stuckTo == null) {
                try
                {
                    int num188 = (int)(projectile.position.X / 16f) - 1;
                    int num189 = (int)((projectile.position.X + (float)projectile.width) / 16f) + 2;
                    int num190 = (int)(projectile.position.Y / 16f) - 1;
                    int num191 = (int)((projectile.position.Y + (float)projectile.height) / 16f) + 2;
                    if (num188 < 0)
                    {
                        num188 = 0;
                    }
                    if (num189 > Main.maxTilesX)
                    {
                        num189 = Main.maxTilesX;
                    }
                    if (num190 < 0)
                    {
                        num190 = 0;
                    }
                    if (num191 > Main.maxTilesY)
                    {
                        num191 = Main.maxTilesY;
                    }
                    Vector2 val27 = default(Vector2);
                    for (int num192 = num188; num192 < num189; num192++)
                    {
                        for (int num193 = num190; num193 < num191; num193++)
                        {
                            if (Main.tile[num192, num193] != null && Main.tile[num192, num193].nactive() && (Main.tileSolid[Main.tile[num192, num193].type] || (Main.tileSolidTop[Main.tile[num192, num193].type] && Main.tile[num192, num193].frameY == 0)))
                            {
                                val27.X = num192 * 16;
                                val27.Y = num193 * 16;
                                if (projectile.position.X + (float)projectile.width > val27.X && projectile.position.X < val27.X + 16f && projectile.position.Y + (float)projectile.height > val27.Y && projectile.position.Y < val27.Y + 16f)
                                {
                                    projectile.velocity.X = 0f;
                                    projectile.velocity.Y = -0.2f;
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
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
            } else {
                projectile.velocity = stuckTo.Center+stuckToOffset-0.5f*projectile.Hitbox.Size()-projectile.position;
                if (!stuckTo.active) {
                    stuckTo = null;
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            stuckTo = target;
            stuckToOffset = projectile.Center-target.Center;
        }
    }
}