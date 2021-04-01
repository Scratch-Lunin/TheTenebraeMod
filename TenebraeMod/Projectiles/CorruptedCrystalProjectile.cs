using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles
{
    public class CorruptedCrystalProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupted Crystal");
        }
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 34;
            projectile.aiStyle = 1;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 1800;
            projectile.extraUpdates = 1;
            projectile.tileCollide = true;
            projectile.light = 0.75f;
            projectile.coldDamage = true;
            aiType = ProjectileID.Bullet;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 100, true);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, ProjectileID.BlowupSmoke, projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0]);
            return true;
        }
        public override void AI()
        {
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 61, projectile.velocity.X * -0.2f, projectile.velocity.Y * -0.2f, 100);
            if (projectile.ai[0] != 0)
            {
                if (projectile.localAI[0] == 0)
                {
                    projectile.localAI[0] = projectile.position.X;
                }
                if (projectile.localAI[1] == 0)
                {
                    projectile.localAI[1] = projectile.position.Y;
                }
                float freq = 0.15f;
                float mag = 40f;
                int time = 1800 - projectile.timeLeft;
                Vector2 pos = new Vector2(projectile.localAI[0], projectile.localAI[1]);
                Vector2 dir = projectile.velocity;
                dir.Normalize();
                Vector2 axis = dir.RotatedBy(90 * projectile.ai[0] * 0.0174f);
                Vector2 wave = axis * (float)Math.Sin(time * freq) * mag;
                projectile.position = pos + wave;
                projectile.localAI[0] = projectile.position.X - wave.X + projectile.velocity.X;
                projectile.localAI[1] = projectile.position.Y - wave.Y + projectile.velocity.Y;
            }
        }
    }
}

