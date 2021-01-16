using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TenebraeMod.Projectiles
{
	public class SporeScepterCloud : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scepter Spore Cloud");
			Main.projFrames[projectile.type] = 3;
		}
		public override void SetDefaults()
		{
			projectile.width = 34;
			projectile.height = 36;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 200;
			projectile.extraUpdates = 1;
			projectile.light = 1f;
			projectile.tileCollide = false;
			projectile.alpha = 50;
			aiType = ProjectileID.Bullet;
		}

		public override void AI()
		{
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 3, projectile.velocity.X * -0.2f, projectile.velocity.Y * -0.2f, 100);
		}

		public override void Kill(int timeLeft)
		{
			Dust.NewDust(projectile.position - new Vector2(2f, 1f), projectile.width, projectile.height, 3, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 3.5f);
			Dust.NewDust(projectile.position - new Vector2(2f, 5f), projectile.width, projectile.height, 3, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 3.5f);
			Dust.NewDust(projectile.position - new Vector2(2f, 2f), projectile.width, projectile.height, 3, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 3.5f);
			Dust.NewDust(projectile.position - new Vector2(3f, 5f), projectile.width, projectile.height, 3, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 3.5f);
			Dust.NewDust(projectile.position - new Vector2(2f, 5f), projectile.width, projectile.height, 3, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 3.5f);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Poisoned, 100);
		}
	}
}