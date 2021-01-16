using Terraria;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TenebraeMod.Projectiles
{
	public class SporeScepterProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scepter Spore");
		}
		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = 3;
			projectile.timeLeft = 40;
			projectile.light = 1f;
			projectile.tileCollide = false;
			aiType = ProjectileID.Bullet;
		}

		public override void AI()
		{
			projectile.velocity *= 0.99f;
			projectile.rotation += (float)projectile.direction * 0.1f;
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 3, projectile.velocity.X * -0.2f, projectile.velocity.Y * -0.2f, 100);
		}

		public override void Kill(int timeLeft)
		{
			Dust.NewDust(projectile.position - new Vector2(2f, 1f), projectile.width, projectile.height, 3, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 3.5f);
			Dust.NewDust(projectile.position - new Vector2(2f, 5f), projectile.width, projectile.height, 3, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 3.5f);
			Dust.NewDust(projectile.position - new Vector2(2f, 2f), projectile.width, projectile.height, 3, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 3.5f);
			Dust.NewDust(projectile.position - new Vector2(3f, 5f), projectile.width, projectile.height, 3, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 3.5f);
			Dust.NewDust(projectile.position - new Vector2(4f, 5f), projectile.width, projectile.height, 3, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 3.5f);
			Dust.NewDust(projectile.position - new Vector2(2f, 6f), projectile.width, projectile.height, 3, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 3.5f);
			Dust.NewDust(projectile.position - new Vector2(2f, 5f), projectile.width, projectile.height, 3, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 3.5f);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("SporeScepterCloud"), projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0]);
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Poisoned, 100);
		}
	}
}