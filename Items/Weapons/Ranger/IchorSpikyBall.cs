using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
 
namespace TenebraeMod.Items.Weapons.Ranger
{
    public class IchorSpikyBall : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ichor Spiky Ball");
            Tooltip.SetDefault("Sprays streams of ichor on hit");
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
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 1;
            item.value = 3;
            item.rare = ItemRarityID.LightRed;
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("IchorSpikyBallProjectile");
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<WoodenSpikyBall>(),25);
            recipe.AddIngredient(ItemID.Ichor);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this,25);
            recipe.AddRecipe();
        }
    }

    internal class IchorSpikyBallProjectile : ModProjectile {
		public override string Texture => "TenebraeMod/Items/Weapons/Ranger/IchorSpikyBall";
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Ichor Spiky Ball");
		}

		public override void SetDefaults() {
			projectile.thrown = true;
			projectile.aiStyle = -1;
			projectile.width = 14;
			projectile.height = 14;
			projectile.penetrate = 1;
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
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.AddBuff(BuffID.Ichor,300);
        }
        public override void OnHitPvp(Player target, int damage, bool crit) {
            target.AddBuff(BuffID.Ichor,300);
        }

        public override void Kill(int timeLeft) {
            for (int i=0; i<3; i++) {
                Projectile shot = Main.projectile[Projectile.NewProjectile(projectile.Center,new Vector2(0,-4).RotatedByRandom(Math.PI/2),ProjectileID.GoldenShowerFriendly,projectile.damage,projectile.knockBack,projectile.owner)];
                shot.magic = false;
                shot.thrown = true;
            }
        }
    }
}