using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Enums;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles
{
	// The following laser shows a channeled ability, after charging up the laser will be fired
	// Using custom drawing, dust effects, and custom collision checks for tiles

	// Blatantly copied from my Polarities code which is blatantly copied from ExampleMod because I can't code to save my life

	public class QuasarCrawlerLaser : ModProjectile
	{
		// Use a different style for constant so it is very clear in code when a constant is used

		//The distance charge particle from the npc center
		private const float MOVE_DISTANCE = 20f;

		// The actual distance is stored in the ai0 field
		// By making a property to handle this it makes our life easier, and the accessibility more readable
		public float Distance {
			get => projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		//The npc which owns this laser
		public int owner {
			get => (int)projectile.ai[1];
			set => projectile.ai[1] = value;
		}
		
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Quasar Beam");
		}

		public override void SetDefaults() {
			projectile.width = 10;
			projectile.height = 10;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.hide = false;
			projectile.timeLeft = 2;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
			// We start drawing the laser
			DrawLaser(spriteBatch, Main.projectileTexture[projectile.type], Main.npc[owner].Center, projectile.velocity, 10, projectile.damage, -1.57f, 1f, 1000f, Color.White, (int)MOVE_DISTANCE);
			return false;
		}

		// The core function of drawing a laser
		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50) {
			float r = unit.ToRotation() + rotation;

			// Draws the laser 'body'
			for (float i = transDist; i <= Distance; i += step) {
				Color c = Color.White;
				var origin = start + i * unit;
				spriteBatch.Draw(texture, origin - Main.screenPosition,
					new Rectangle(0, 26, 28, 26), i < transDist ? Color.Transparent : c, r,
					new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
			}
			
			// Draws the laser 'tail'
			spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
				new Rectangle(0, 0, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
			
			// Draws the laser 'head'
			spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition,
				new Rectangle(0, 52, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
		}

		// Change the way of collision check of the projectile
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {

			NPC npc = Main.npc[owner];
			Vector2 unit = projectile.velocity;
			float point = 0f;
			// Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
			// It will look for collisions on the given line using AABB
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), npc.Center,
				npc.Center + unit * Distance, 22, ref point);
		}

		// The AI of the projectile
		public override void AI() {
			NPC npc = Main.npc[owner];
			projectile.position = npc.Center + projectile.velocity * MOVE_DISTANCE;
			projectile.timeLeft = 2;
			if (!npc.active) { projectile.Kill(); }

			SetLaserPosition(npc);
			SpawnDusts(npc);
			CastLights();
		}

		private void SpawnDusts(NPC npc)
		{
			Vector2 unit = projectile.velocity * -1;
			Vector2 dustPos = npc.Center + projectile.velocity * Distance;

			for (int i = 0; i < 2; ++i) {
				float num1 = projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
				float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
				Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
				Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, 133, dustVel.X, dustVel.Y)];
				dust.noGravity = true;
				dust.scale = 1f;
			}

			if (Main.rand.NextBool(5)) {
				Vector2 offset = projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * projectile.width;
				Dust dust = Main.dust[Dust.NewDust(dustPos + offset - Vector2.One * 4f, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.velocity *= 0.5f;
				dust.velocity.Y = -Math.Abs(dust.velocity.Y);
				unit = dustPos - npc.Center;
				unit.Normalize();
				dust = Main.dust[Dust.NewDust(Main.npc[projectile.owner].Center + 55 * unit, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.velocity = dust.velocity * 0.5f;
				dust.velocity.Y = -Math.Abs(dust.velocity.Y);
			}
		}


		/*
		 * Sets the end of the laser position based on where it collides with something
		 */
		private void SetLaserPosition(NPC npc)
		{
			for (Distance = MOVE_DISTANCE; Distance <= 2200f; Distance += 5f)
			{
				var start = npc.Center + projectile.velocity * Distance;
				if (!Collision.CanHit(npc.Center, 1, 1, start, 1, 1)) {
					Distance -= 5f;
					break;
				}
			}
		}

		private void CastLights()
		{
			// Cast a light along the line of the laser
			DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
			Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * (Distance - MOVE_DISTANCE), 26, DelegateMethods.CastLight);
		}

		public override bool ShouldUpdatePosition() => false;
	}
}
