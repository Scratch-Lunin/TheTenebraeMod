using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles.Melee
{
    public class SkelerangProjectile : ModProjectile
    {
        public int boneTimer;
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 36;
            projectile.aiStyle = 3;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 600;
            projectile.light = 0.5f;
            projectile.extraUpdates = 1;
            projectile.maxPenetrate = 3;
        }
        public override void AI()
        {
            if (boneTimer < 40)
            {
                boneTimer += 1;
                if (boneTimer % 7 == 0)
                {
                    var proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, (float)-0.5, ModContent.ProjectileType<SkelerangBone>(), (int)(projectile.damage * 0.5), projectile.knockBack, projectile.owner, projectile.whoAmI);
                    Main.projectile[proj].rotation = Main.rand.Next(180); // Use an actual rotation instead of PI
                }
            }
            // Additional hooks/methods here.
        }
    }
    public class SkelerangBone : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 2;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Dig, projectile.position);
            projectile.Kill();
            return false;
        }
    }
}
