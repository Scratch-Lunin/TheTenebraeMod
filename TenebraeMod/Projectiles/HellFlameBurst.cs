using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles
{
    public class HellFlameBurst : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 16;
        }
        public override void SetDefaults()
        {
            projectile.width = 75;
            projectile.height = 156;
            projectile.alpha = 0;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.magic = true;
            projectile.aiStyle = 1;
            projectile.penetrate = 3;
            projectile.scale = 1.5f;
            projectile.timeLeft = 32;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
        }
        public override void AI()
        {
            if (++projectile.frameCounter >= 2)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 16)
                {
                    projectile.frame = 0;


                    projectile.velocity.Y = projectile.velocity.Y + -0.1f; // 0.1f for arrow gravity, 0.4f for knife gravity
                    if (projectile.velocity.Y > 16f) // This check implements "terminal velocity". We don't want the projectile to keep getting faster and faster. Past 16f this projectile will travel through blocks, so this check is useful.
                    {
                        projectile.velocity.Y = 16f;
                    }
                }
            }
            //add lighting
            Lighting.AddLight(projectile.position, new Vector3(2f, 0.5f, 0f)); //the Vector3 will be the color in rgb values, the vector2 will be your projectile's position
            Lighting.maxX = 400; //height 
            Lighting.maxY = 400; //width
        }
    }
}