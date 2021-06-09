using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace TenebraeMod.Projectiles.Melee
{
	public class BinaryStarProjectile : ModProjectile
	{
        public double theta;
        public int timer;
        private NPC target;
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Binary Star");
		}

		public override void SetDefaults() {
			projectile.melee = true;
			projectile.aiStyle = 15;
			projectile.width = 46;
			projectile.height = 46;
			projectile.timeLeft = 3600;
			projectile.penetrate = -1;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
			projectile.light = 1f;
            timer = theta>1f ? 7 : 0;
		}

        public override bool PreAI() {
            return false;
        }

        public override void PostAI() {
            timer++;
            if (timer==28) {
                target = null;
                timer=0;
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
                if (projectile.friendly) {
                    target = Main.npc[targetID];
                    
                    Vector2 shotVelocity = target.Center-projectile.Center;
                    shotVelocity.Normalize();
                    shotVelocity*=16;
                    Projectile.NewProjectile(projectile.Center,shotVelocity,ProjectileType<BinaryStarFlare>(),(int)(4f/3f*projectile.damage),projectile.knockBack,projectile.owner);
                }
                projectile.friendly = true;
            }

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

            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
                return;
            }
            Main.player[projectile.owner].itemAnimation = 10;
            Main.player[projectile.owner].itemTime = 10;

            if (projectile.position.X + (float)(projectile.width / 2) > Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2))
            {
                Main.player[projectile.owner].ChangeDir(1);
                projectile.direction = 1;
            }
            else
            {
                Main.player[projectile.owner].ChangeDir(-1);
                projectile.direction = -1;
            }
		    Vector2 mountedCenter2 = Main.player[projectile.owner].MountedCenter;

            float num209 = mountedCenter2.X - projectile.Center.X;
            float num210 = mountedCenter2.Y - projectile.Center.Y;
           
            float dToPlayer = (projectile.Center-Main.player[projectile.owner].MountedCenter).Length();
                float maxDist = 400f;
            if (projectile.ai[0] == 0f)
            {
                projectile.tileCollide = true;
                if (dToPlayer > maxDist)
                {
                    projectile.ai[0] = 1f;
                    projectile.netUpdate = true;
                }
                else if (!Main.player[projectile.owner].channel)
                {
                    if (projectile.velocity.Y < 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y * 0.9f;
                    }
                    projectile.velocity.Y = projectile.velocity.Y + 1f;
                    projectile.velocity.X = projectile.velocity.X * 0.9f;
                }
                theta+=0.1;
                projectile.velocity+=0.5f*(Vector2.One.RotatedBy(theta));
            }
            else if (projectile.ai[0] == 1f)
            {
                float num213 = 14f / Main.player[projectile.owner].meleeSpeed;
                float num214 = 0.9f / Main.player[projectile.owner].meleeSpeed;
                Math.Abs(num209);
                Math.Abs(num210);
                if (projectile.ai[1] == 1f)
                {
                    projectile.tileCollide = false;
                }
                if (!Main.player[projectile.owner].channel)
                {
                    projectile.ai[1] = 1f;
                    if (projectile.tileCollide)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.tileCollide = false;
                    if (dToPlayer < 20f)
                    {
                        projectile.Kill();
                    }
                }
                if (!projectile.tileCollide)
                {
                    num214 *= 2f;
                }
                int num216 = 60;
                if (dToPlayer > (float)num216 || !projectile.tileCollide)
                {
                    dToPlayer = num213 / dToPlayer;
                    num209 *= dToPlayer;
                    num210 *= dToPlayer;
                    new Vector2(projectile.velocity.X, projectile.velocity.Y);
                    float num217 = num209 - projectile.velocity.X;
                    float num218 = num210 - projectile.velocity.Y;
                    float num219 = (float)Math.Sqrt(num217 * num217 + num218 * num218);
                    num219 = num214 / num219;
                    num217 *= num219;
                    num218 *= num219;
                    projectile.velocity.X = projectile.velocity.X * 0.98f;
                    projectile.velocity.Y = projectile.velocity.Y * 0.98f;
                    if (Main.player[projectile.owner].channel) {
                        projectile.velocity.X = projectile.velocity.X + num217/(50*dToPlayer);
                        projectile.velocity.Y = projectile.velocity.Y + num218/(50*dToPlayer);
                        theta+=0.1;
                        projectile.velocity+=0.5f*(Vector2.One.RotatedBy(theta));
                    } else {
                        projectile.velocity.X = projectile.velocity.X + num217;
                        projectile.velocity.Y = projectile.velocity.Y + num218;
                    }
                }
                else
                {
                    if (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) < 6f)
                    {
                        projectile.velocity.X = projectile.velocity.X * 0.96f;
                        projectile.velocity.Y = projectile.velocity.Y * 0.96f;
                    }
                    if (Main.player[projectile.owner].velocity.X == 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X * 0.96f;
                    }
                }

                if (target!=null) {
                    Vector2 targetDir = target.Center-projectile.Center;
                    targetDir.Normalize();
                    projectile.velocity += targetDir/2;
                }
            }

            projectile.rotation = (float)Math.Atan2(num210, num209) - projectile.velocity.X * 0.3f;
            return;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
			Vector2 playerCenter = Main.player[projectile.owner].MountedCenter;
			Vector2 center = projectile.Center;
			Vector2 distToProj = playerCenter - projectile.Center;
			float projRotation = distToProj.ToRotation();
			float distance = distToProj.Length();
			while (distance > 30f && !float.IsNaN(distance)) {
				distToProj.Normalize();                 //get unit vector
				distToProj *= 28f;                      //speed = 24
				center += distToProj;                   //update draw position
				distToProj = playerCenter - center;    //update distance
				distance = distToProj.Length();

				//Draw chain
				spriteBatch.Draw(mod.GetTexture("Projectiles/Melee/BinaryStarChain"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
					new Rectangle(0, 0, 28, 10), Color.White, projRotation,
					new Vector2(28 * 0.5f, 10 * 0.5f), 1f, SpriteEffects.None, 0f);
			}

            spriteBatch.Draw(mod.GetTexture("Projectiles/Melee/BinaryStarProjectile"), new Vector2(projectile.Center.X - Main.screenPosition.X, projectile.Center.Y - Main.screenPosition.Y),
                new Rectangle(0,0,projectile.width,projectile.height),Color.White,projectile.rotation,
                new Vector2(projectile.width*0.5f,projectile.height*0.5f),1f,SpriteEffects.None,0f);            
			return false;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.AddBuff(BuffID.Daybreak,5);
        }
	}
}