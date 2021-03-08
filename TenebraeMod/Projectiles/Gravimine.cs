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
	public class Gravimine : ModProjectile
	{

        private NPC target;
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Gravamine");
			Main.projFrames[projectile.type] = 8;
		}

		public override void SetDefaults() {
			projectile.ranged = true;
			projectile.aiStyle = -1;
			projectile.width = 58;
			projectile.height = 56;
			drawOffsetX = -6;
			drawOriginOffsetY = 11;
			drawOriginOffsetX = 0;
			projectile.alpha = 0;
			projectile.timeLeft = 400;
			projectile.penetrate = -1;
			projectile.friendly = true;
			projectile.tileCollide = true;
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

			projectile.ai[0]--;
			projectile.friendly = projectile.ai[0]<=0;
            if (projectile.timeLeft == 1) {
                Explode();
                return;
            }
			int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 0, Color.Black, 1f);

            projectile.velocity *= 0.95f;

            for (int k = 0; k < 200; k++) {
                if (Main.npc[k].active && !Main.npc[k].friendly && !Main.npc[k].immortal && !Main.npc[k].boss && !Main.npc[k].dontTakeDamage && Main.npc[k].knockBackResist!=0f) {
                    Vector2 force = projectile.Center-Main.npc[k].Center;
                    force /= (float)Math.Pow(force.Length(),3);
                    force *= 10000;
                    if (force.Length() > 0.1f) {
                        force.Normalize();
                    }
                    Main.npc[k].velocity+=force;
                }
            }
		}
		public override bool OnTileCollide(Vector2 oldVelocity) {
            Explode();
            return true;
        }

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			target.immune[projectile.owner] = 0;
			projectile.ai[0] = 10;
		}

        private void Explode() {
			Main.PlaySound(SoundID.Item62, projectile.position);
			projectile.alpha = 255;
            projectile.timeLeft = Math.Min(projectile.timeLeft,1);
            projectile.position = projectile.Center;
            projectile.velocity = Vector2.Zero;
			projectile.width = 255;
			projectile.height = 255;
			projectile.Center = projectile.position;

            SpawnDusts();
        }

        private void SpawnDusts() {
            //Shamelessly copied from ExampleMod
			// Smoke Dust spawn
			for (int i = 0; i < 50; i++) {
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			// Electric Dust spawn
			for (int i = 0; i < 40; i++) {
				int dustIndex = Dust.NewDust(projectile.position-(projectile.position-projectile.Center)/2, projectile.width/2, projectile.height/2, DustID.Electric, 0f, 0f, 100, default(Color), 1f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 3f;
			}
			// Large Smoke Gore spawn
			for (int g = 0; g < 2; g++) {
				int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
            }
        }
	}
}