using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles.Melee
{
	// to investigate: Projectile.Damage, (8843)
	internal class GemHammerWave : ModProjectile
	{
		public override void SetDefaults() {
			// while the sprite is actually bigger than 15x15, we use 15x15 since it lets the projectile clip into tiles as it bounces. It looks better.
			projectile.width = 55;
			projectile.height = 55;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.aiStyle = 0;   
			projectile.melee = true;  		
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = false;  
			projectile.alpha = 255;			
		//Can the projectile collide with tiles?			
			// 5 second fuse.
		projectile.timeLeft = 15; 
		}
	

		public override void AI() {
			if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3) {
			}
			else {
				// Smoke and fuse dust spawn.
				if (Main.rand.NextBool()) {
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 59, 0f, 0f, 0, default(Color), 1f);
					Main.dust[dustIndex].scale = 0.1f + (float)Main.rand.Next(1) * 0.1f;
					Main.dust[dustIndex].fadeIn = .8f + (float)Main.rand.Next(1) * 0.1f;
					Main.dust[dustIndex].noGravity = true;
					dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 60, 0f, 0f, 0, default(Color), 1f);
					Main.dust[dustIndex].scale = 0.1f + (float)Main.rand.Next(1) * 0.1f;
					Main.dust[dustIndex].fadeIn = .8f + (float)Main.rand.Next(1) * 0.1f;
					Main.dust[dustIndex].noGravity = true;
					dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 61, 0f, 0f, 0, default(Color), 1f);
					Main.dust[dustIndex].scale = 0.1f + (float)Main.rand.Next(1) * 0.1f;
					Main.dust[dustIndex].fadeIn = .5f + (float)Main.rand.Next(1) * 0.1f;
					Main.dust[dustIndex].noGravity = true;
					dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 62, 0f, 0f, 0, default(Color), 1f);
					Main.dust[dustIndex].scale = 0.1f + (float)Main.rand.Next(1) * 0.1f;
					Main.dust[dustIndex].fadeIn = .5f + (float)Main.rand.Next(1) * 0.1f;
					Main.dust[dustIndex].noGravity = true;
					dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 63, 0f, 0f, 0, default(Color), 1f);
					Main.dust[dustIndex].scale = 0.1f + (float)Main.rand.Next(1) * 0.1f;
					Main.dust[dustIndex].fadeIn = .5f + (float)Main.rand.Next(1) * 0.1f;
					Main.dust[dustIndex].noGravity = true;
					dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 64, 0f, 0f, 0, default(Color), 1f);
					Main.dust[dustIndex].scale = 0.1f + (float)Main.rand.Next(1) * 0.1f;
					Main.dust[dustIndex].fadeIn = .9f + (float)Main.rand.Next(1) * 0.1f;
					Main.dust[dustIndex].noGravity = true;
				}
			}
}}}