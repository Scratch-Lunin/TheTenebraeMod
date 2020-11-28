using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles
{
    public class ShadeBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shade Ball");
            Main.projFrames[projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 7; // Added extra penetration in the event that it hits 0 before the explode code can run
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 160;
        }

        public override void AI()
        {
            if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3 || projectile.penetrate <= 2)
            {
                projectile.tileCollide = false;
                projectile.alpha = 255;
                projectile.position = projectile.Center;
                projectile.width = 150;
                projectile.height = 150;
                projectile.Center = projectile.position;
                projectile.damage = 32;
                projectile.knockBack = 5;
                projectile.penetrate = -1;
                if (projectile.timeLeft > 3)
                {
                    projectile.timeLeft = 3;
                }
            }
            else
            {
                if (Main.rand.NextBool(1))
                {
                    Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 27, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                }
                projectile.velocity.X = projectile.velocity.X * 0.97f;
                projectile.velocity.Y = projectile.velocity.Y * 0.97f;
                // Animate the projectile
                if (++projectile.frameCounter >= 7)
                {
                    projectile.frameCounter = 0;
                    if (++projectile.frame >= 6)
                    {
                        projectile.frame = 0;
                    }
                }
                // Add lighting
                Lighting.AddLight(projectile.position, new Vector3(0f, 0f, 0f)); // The vector3 will be the color in RGB values, the vector2 will be your projectile's position
                Lighting.maxX = 26; // Height 
                Lighting.maxY = 26; // Width
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 27, 0f, 0f, 100, default(Color), 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 4f;
                dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 27, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 2f;
            }
        }
    }
}