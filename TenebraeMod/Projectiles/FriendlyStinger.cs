using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles
{
	public class FriendlyStinger : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Stinger");
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 24;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
			projectile.timeLeft = 180;
		}

		public override void AI()
        {
			// Make the projectile face where it's heading
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			if (Main.rand.Next(2) == 0) {
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 18, 0f, 0f, 0, default(Color), 0.75f);
			}
        }
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			target.AddBuff(20, 600, true);
		}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			Main.PlaySound(SoundID.Dig, projectile.position);
			return true;
		}
	}
}