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
	public class VortexRocket : ModProjectile
	{

        private NPC target;
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Vortex Rocket");
		}

		public override void SetDefaults() {
			projectile.ranged = true;
			projectile.aiStyle = -1;
			projectile.width = 14;
			projectile.height = 14;
			drawOffsetX = 0;
			drawOriginOffsetY = 0;
			drawOriginOffsetX = 0;
			projectile.alpha = 0;
			projectile.timeLeft = 300;
			projectile.penetrate = -1;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
			projectile.light = 1f;
		}

		public override void AI() {
            projectile.rotation = projectile.velocity.ToRotation()+(float)Math.PI/2;
            if (projectile.timeLeft == 1) {
                Explode();
            } else if (projectile.timeLeft > 1) {
				int dustIndex = Dust.NewDust(projectile.position-(projectile.position-projectile.Center)/2, projectile.width/2, projectile.height/2, DustID.Electric, 0f, 0f, 100, default(Color), 0.8f);
				Main.dust[dustIndex].noGravity = true;

                if (target == null || !target.active || !target.chaseable || target.dontTakeDamage) {
                    float distance = 900f;
                    projectile.friendly = false;
                    int targetID = -1;
                    for (int k = 0; k < 200; k++) {
                        if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && !Main.npc[k].immortal && Main.npc[k].chaseable) {
                            Vector2 newMove = Main.npc[k].Center - Main.MouseWorld;
                            float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                            if (distanceTo < distance) {
                                targetID = k;
                                distance = distanceTo;
                                projectile.friendly = true;
                            }
                        }
                    }
                    if (!projectile.friendly) {
                        return;
                    }
                    projectile.friendly = true;

                    target = Main.npc[targetID];
                }

                if (target!=null) {
                    float dTheta = (target.Center-projectile.Center).ToRotation()-projectile.velocity.ToRotation();
                    if (dTheta > Math.PI) {
                        dTheta -= 2*(float)Math.PI;
                    } else if (dTheta < -Math.PI) {
                        dTheta += 2*(float)Math.PI;
                    }
                    if (Math.Abs(dTheta) > 0.02f) {
                        dTheta = (dTheta > 0) ? 0.02f : -0.02f;
                    }
                    projectile.velocity = projectile.velocity.RotatedBy(dTheta);
                }
            } else {
                projectile.width = 192;
                projectile.height = 192;
            }
		}

		public override void OnHitPlayer(Player target, int damage, bool crit) {
			Explode();
		}

		public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit) {
			target.immune[projectile.owner] = 0;
			Explode();
		}

        public override void OnHitPvp(Player target, int damage, bool crit) {
            Explode();
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            Explode();
            return true;
        }

        private void Explode() {
			Main.PlaySound(SoundID.Item62, projectile.position);
			projectile.alpha = 255;
            projectile.timeLeft = Math.Min(projectile.timeLeft,1);
            projectile.position = projectile.Center;
            projectile.velocity = Vector2.Zero;
			projectile.width = 192;
			projectile.height = 192;
			projectile.Center = projectile.position;

            SpawnDusts();
        }

        private void SpawnDusts() {
            //Shamelessly copied from ExampleMod
			// Smoke Dust spawn
			for (int i = 0; i < 25; i++) {
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			// Electric Dust spawn
			for (int i = 0; i < 20; i++) {
				int dustIndex = Dust.NewDust(projectile.position-(projectile.position-projectile.Center)/2, projectile.width/2, projectile.height/2, DustID.Electric, 0f, 0f, 100, default(Color), 1f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 3f;
			}
			// Large Smoke Gore spawn
			for (int g = 0; g < 1; g++) {
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