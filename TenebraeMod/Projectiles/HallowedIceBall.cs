using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles
{
  public class HallowedIceBall : ModProjectile
  {
  public override void SetStaticDefaults()
  {
  DisplayName.SetDefault("Hallowed Ice Ball");
  }

  public override void SetDefaults()
  {
  projectile.width = 15;
  projectile.height = 15;
  projectile.aiStyle = 1;
  projectile.friendly = true;  //Can the projectile deal damage to enemies?
  projectile.hostile = false;  //Can the projectile deal damage to the player?
  projectile.penetrate = 1;
  projectile.timeLeft = 25;
  projectile.ignoreWater = false;
  projectile.tileCollide = false;
  projectile.aiStyle = 2;   
  }
		public override void Kill(int timeLeft) {
							if (projectile.ai[1] == 0) {
				for (int i = 1; i < 3; i++) {
					// Random upward vector.
					Vector2 vel = new Vector2(Main.rand.NextFloat(-10, 10), Main.rand.NextFloat(-10, 10));
					Projectile.NewProjectile(projectile.Center, vel, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0, 1);
				}
			}
			Main.PlaySound(SoundID.Item27, projectile.position);
			for (int i = 0; i < 5; i++) {
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 58, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
				
			}
		}
	}
}