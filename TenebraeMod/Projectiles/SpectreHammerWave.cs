using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System.Diagnostics;

namespace TenebraeMod.Projectiles
{
	public class SpectreHammerWave : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.Homing[projectile.type] = true;
		}
		public override void SetDefaults() {
			projectile.width = 20;
			projectile.height = 20;
			projectile.alpha = 255;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.ranged = true;
					projectile.timeLeft = 200;

		}
public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.player[projectile.owner].statLife -= 2;   
        }
		public override void AI() {
			if (projectile.alpha > 70) {
				projectile.alpha -= 15;
				if (projectile.alpha < 70) {
					projectile.alpha = 70;
				}
			}
			if (projectile.localAI[0] == 0f) {
				AdjustMagnitude(ref projectile.velocity);
				projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 400f;
			bool target = false;
			for (int k = 0; k < 200; k++) {
				if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5) {
					Vector2 newMove = Main.npc[k].Center - projectile.Center;
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance) {
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}
			if (target) {
				AdjustMagnitude(ref move);
				projectile.velocity = (10 * projectile.velocity + move) / 11f;
				AdjustMagnitude(ref projectile.velocity);
			}
			if (projectile.alpha <= 100) {
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 261, 0f, 0f, 0, default(Color), 1f);
					Main.dust[dustIndex].scale = 4f + (float)Main.rand.Next(5) * 0.3f;
					Main.dust[dustIndex].fadeIn = 1.0f + (float)Main.rand.Next(5) * 0.3f;
					Main.dust[dustIndex].noGravity = true;
			}
		}

		private void AdjustMagnitude(ref Vector2 vector) {
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 6f) {
				vector *= 6f / magnitude;
			}
			}
		}
	}