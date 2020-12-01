using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles
{
    public class TerraSphere : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Orb");
            Main.projFrames[projectile.type] = 17;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }

		public override void SetDefaults()
		{
            projectile.aiStyle = -1;
			projectile.alpha = 32;
			projectile.width = 34;
			projectile.height = 34;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.timeLeft = 480;
			projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.light = 1f;
        }

        private NPC target;

        public override void AI()
        {
            float speed = projectile.velocity.Length();

            ReTarget(800f);
            if (target != null)
            {
                projectile.velocity += (target.Center - projectile.Center).SafeNormalize(Vector2.Zero)/2;
            }

            projectile.velocity = projectile.velocity.SafeNormalize(Vector2.Zero) * speed * 1.01f;

            projectile.frameCounter++;
            if (projectile.frameCounter == 3)
            {
                projectile.frame = (projectile.frame + 1) % 17;
                projectile.frameCounter = 0;
            }

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.Pi;
        }

        private void ReTarget(float distance)
        {
            bool isTarget = false;
            int targetID = -1;
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5 && !Main.npc[k].immortal && Main.npc[k].chaseable)
                {
                    Vector2 newMove = Main.npc[k].Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        targetID = k;
                        distance = distanceTo;
                        isTarget = true;
                    }
                }
            }

            if (isTarget)
            {
                target = Main.npc[targetID];
            }
            else
            {
                target = null;
            }
        }

        public override bool PreKill(int timeLeft)
        {
            if (projectile.width == 34)
            {
                for (int i = 0; i < projectile.width; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 107, 0, 0, 100, Scale: 1.25f)];
                    dust.velocity = (dust.position - projectile.Center) / 6;
                }

                for (int i = 0; i < 2; i++)
                {
                    Projectile.NewProjectile(projectile.Center, new Vector2(12, 0).RotatedByRandom(MathHelper.TwoPi), ModContent.ProjectileType<TerraSphereShard1>(), projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
                    Projectile.NewProjectile(projectile.Center, new Vector2(12, 0).RotatedByRandom(MathHelper.TwoPi), ModContent.ProjectileType<TerraSphereShard2>(), projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
                    Projectile.NewProjectile(projectile.Center, new Vector2(12, 0).RotatedByRandom(MathHelper.TwoPi), ModContent.ProjectileType<TerraSphereShard3>(), projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
                }

                projectile.alpha = 255;
                projectile.timeLeft = 1;
                projectile.penetrate = -1;
                projectile.position -= new Vector2(96 - 34, 96 - 34);
                projectile.width = 96;
                projectile.height = 96;
                projectile.velocity = Vector2.Zero;

                Main.PlaySound(SoundID.NPCKilled, projectile.Center, 14);
                return false;
            }
            return true;
        }
    }

    public class BigTerraSphere : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Orb");
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.alpha = 32;
            projectile.width = 50;
            projectile.height = 50;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.timeLeft = 480;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = false;
            projectile.light = 1f;
        }

        public override void AI()
        {
            projectile.rotation += 0.02f;

            projectile.ai[0]++;
            if (projectile.ai[0] % 60 == 0)
            {
                Projectile.NewProjectile(projectile.Center, new Vector2(12, 0).RotatedBy(projectile.rotation), ModContent.ProjectileType<TerraSphereShard1>(), projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
                Projectile.NewProjectile(projectile.Center, new Vector2(12, 0).RotatedBy(projectile.rotation + MathHelper.TwoPi/3), ModContent.ProjectileType<TerraSphereShard2>(), projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
                Projectile.NewProjectile(projectile.Center, new Vector2(12, 0).RotatedBy(projectile.rotation - MathHelper.TwoPi / 3), ModContent.ProjectileType<TerraSphereShard3>(), projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
            }
        }

        public override bool PreKill(int timeLeft)
        {
            if (projectile.width == 50)
            {
                for (int i = 0; i < projectile.width; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 107, 0, 0, 100, Scale: 1.25f)];
                    dust.velocity = (dust.position - projectile.Center) / 6;
                }

                for (int i = 0; i < 4; i++)
                {
                    Projectile.NewProjectile(projectile.Center, new Vector2(12, 0).RotatedByRandom(MathHelper.TwoPi), ModContent.ProjectileType<TerraSphereShard1>(), projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
                    Projectile.NewProjectile(projectile.Center, new Vector2(12, 0).RotatedByRandom(MathHelper.TwoPi), ModContent.ProjectileType<TerraSphereShard2>(), projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
                    Projectile.NewProjectile(projectile.Center, new Vector2(12, 0).RotatedByRandom(MathHelper.TwoPi), ModContent.ProjectileType<TerraSphereShard3>(), projectile.damage / 2, projectile.knockBack / 2, projectile.owner);
                }

                projectile.alpha = 255;
                projectile.timeLeft = 1;
                projectile.penetrate = -1;
                projectile.position -= new Vector2(128 - 34, 128 - 34);
                projectile.width = 128;
                projectile.height = 128;
                projectile.velocity = Vector2.Zero;

                Main.PlaySound(SoundID.NPCKilled, projectile.Center, 14);
                return false;
            }
            return true;
        }
    }

    public class TerraSphereShard1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Shard");
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.alpha = 32;
            projectile.width = 12;
            projectile.height = 12;
            drawOriginOffsetY = -6;

            projectile.friendly = true;
            projectile.magic = true;
            projectile.timeLeft = 480;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            projectile.velocity.Y += 0.3f;
            projectile.rotation = projectile.velocity.ToRotation() - MathHelper.PiOver2;
        }
    }

    public class TerraSphereShard2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Shard");
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.alpha = 32;
            projectile.width = 10;
            projectile.height = 10;
            drawOffsetX = -8;
            drawOriginOffsetX = 4;

            projectile.friendly = true;
            projectile.magic = true;
            projectile.timeLeft = 480;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            projectile.velocity.Y += 0.3f;
            projectile.rotation = projectile.velocity.ToRotation();
        }
    }

    public class TerraSphereShard3 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Shard");
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.alpha = 32;
            projectile.width = 12;
            projectile.height = 12;
            drawOffsetX = -6;
            drawOriginOffsetX = 3;

            projectile.friendly = true;
            projectile.magic = true;
            projectile.timeLeft = 480;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            projectile.velocity.Y += 0.3f;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver4;
        }
    }
}
