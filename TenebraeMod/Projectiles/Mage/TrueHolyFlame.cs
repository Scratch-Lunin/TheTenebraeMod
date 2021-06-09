using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles.Mage
{
	public class TrueHolyFlame : ModProjectile
	{
		float rotation;
		int sharpness = 3; // How sharp the dust spawn curve is
		float maxvalue = 6f; // The highest value dustcount can reach

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("True Holy Flame");
		}

		public override void SetDefaults()
		{
			projectile.alpha = 255;
			projectile.width = 40;
			projectile.height = 40;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.MaxUpdates = 5;
			projectile.penetrate = -1;
			rotation = Main.rand.NextFloat(-0.8f, 0.8f);
		}

		public override void AI() {
			AdaptedAIStyle();
			projectile.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(rotation));
			rotation *= 1.05f;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) { // TODO: Add OnHitPlayer method for PVP?
			target.AddBuff(ModContent.BuffType<Buffs.HolyFlames>(), 480, true);
		}

		private void AdaptedAIStyle() {
			// Most of this code was adapted from the vanilla shadowflame aistyle. The things you have to do just to change the dust...
			Vector2 center10 = projectile.Center;
			projectile.scale = 1f - projectile.localAI[0];
			projectile.width = (int)(20f * projectile.scale);
			projectile.height = projectile.width;
			projectile.position.X = center10.X - (float)(projectile.width / 2);
			projectile.position.Y = center10.Y - (float)(projectile.height / 2);
			if ((double)projectile.localAI[0] < 0.1)
			{
				projectile.localAI[0] += 0.005f;
			}
			else
			{
				projectile.localAI[0] += 0.0125f;
			}
			if (projectile.localAI[0] >= 0.95f)
			{
				projectile.Kill();
			}
			projectile.velocity.X += projectile.ai[0] * 1.5f;
			projectile.velocity.Y += projectile.ai[1] * 1.5f;
			if (projectile.velocity.Length() > 16f)
			{
				projectile.velocity.Normalize();
				projectile.velocity *= 16f;
			}
			projectile.ai[0] *= 1.05f;
			projectile.ai[1] *= 1.05f;
			if (projectile.scale < 1f)
			{
				int dustType = rotation > 0 ? mod.DustType("HolyflameDust") : mod.DustType("PinkHolyflameDust");
				float dustcount = ((float)Math.Pow(Main.gfxQuality, sharpness) * maxvalue) + 0.5f; // One self-taught maths lesson later...
				for (int num779 = 0; (float)num779 < projectile.scale * dustcount; num779++)
				{
					int num780 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustType, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.1f);
					Main.dust[num780].position = (Main.dust[num780].position + projectile.Center) / 2f;
					Main.dust[num780].noGravity = true;
					Dust dust = Main.dust[num780];
					dust.velocity *= 0.1f;
					dust = Main.dust[num780];
					dust.velocity -= projectile.velocity * (1.3f - projectile.scale);
					Main.dust[num780].fadeIn = 100 + projectile.owner;
					dust = Main.dust[num780];
					dust.scale += projectile.scale * 0.75f;
				}
			}
		}
	}
}