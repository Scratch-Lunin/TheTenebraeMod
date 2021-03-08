using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace TenebraeMod.Projectiles
{
	public class BinaryStarFlare : ModProjectile
	{

        private NPC target;
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Binary Star Flare");
			Main.projFrames[projectile.type] = 5;
		}

		public override void SetDefaults() {
			projectile.melee = true;
			projectile.aiStyle = -1;
			projectile.width = 46;
			projectile.height = 46;
			projectile.alpha = 32;
			projectile.timeLeft = 300;
			projectile.penetrate = 1;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = false;
			projectile.light = 1f;
		}

		public override void AI() 
		{
			if (++projectile.frameCounter >= 6)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= Main.projFrames[projectile.type])
				{
					projectile.frame = 0;
				}
			}
			projectile.rotation += 0.1f;

			Vector2 position69 = projectile.position;
			int width65 = projectile.width;
			int height65 = projectile.height;
			float speedX20 = projectile.velocity.X * 0.4f;
			float speedY24 = projectile.velocity.Y * 0.4f;
			Color newColor = default(Color);
			int num207 = Dust.NewDust(position69, width65, height65, 6, speedX20, speedY24, 100, newColor, 3f);
			Main.dust[num207].noGravity = true;
			Dust dust44 = Main.dust[num207];
			dust44.velocity.X = dust44.velocity.X * 2f;
			Dust dust45 = Main.dust[num207];
			dust45.velocity.Y = dust45.velocity.Y * 2f;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.AddBuff(BuffID.Daybreak,5);
        }
	}
}