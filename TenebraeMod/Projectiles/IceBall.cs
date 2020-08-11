using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles
{
  public class IceBall : ModProjectile
  {
  public override void SetStaticDefaults()
  {
  DisplayName.SetDefault("IceBall");
  }

  public override void SetDefaults()
  {
  projectile.width = 15;
  projectile.height = 15;
  projectile.aiStyle = 1;
  projectile.friendly = true;  //Can the projectile deal damage to enemies?
  projectile.hostile = false;  //Can the projectile deal damage to the player?
  projectile.penetrate = 2;
  projectile.timeLeft = 600;
  projectile.ignoreWater = false;
  projectile.tileCollide = true;
  projectile.aiStyle = 2;   
  }
  
		public override void Kill(int timeLeft) {
			// If we are the original projectile, spawn the 5 child projectiles
			if (projectile.ai[1] == 0) {
				for (int i = 0; i < 0; i++) {
				}
			}
			Main.PlaySound(SoundID.Item27, projectile.position);
			for (int i = 0; i < 5; i++) {
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 211, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
}}}}