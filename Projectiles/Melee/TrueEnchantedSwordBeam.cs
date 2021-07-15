using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles.Melee
{
    public class TrueEnchantedSwordBeam : ModProjectile
    {
        private float timer; // field
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 5;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 48;
            projectile.aiStyle = 27;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.damage = 100;
            projectile.timeLeft = 300;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];

            Color mainColor = Color.White;

            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Color color = mainColor * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                float scale = projectile.scale * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);

                float rotation;
                if (k + 1 >= projectile.oldPos.Length)
                {
                    rotation = (float)((projectile.oldPos[k] - projectile.position).ToRotation() + MathHelper.PiOver2 / 2);
                }
                else
                {
                    rotation = (float)((projectile.oldPos[k] - projectile.oldPos[k + 1]).ToRotation() + MathHelper.PiOver2 / 2);
                }

                spriteBatch.Draw(texture, projectile.Center - projectile.position + projectile.oldPos[k] - Main.screenPosition, new Rectangle(0, projectile.frame * texture.Height / Main.projFrames[projectile.type], texture.Width, texture.Height / Main.projFrames[projectile.type]), color, rotation, new Vector2(texture.Width / 2, texture.Height / Main.projFrames[projectile.type] / 2), scale, SpriteEffects.None, 0f);
            }

            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle(0, projectile.frame * texture.Height / Main.projFrames[projectile.type], texture.Width, texture.Height / Main.projFrames[projectile.type]), mainColor, projectile.rotation, new Vector2(texture.Width / 2, texture.Height / Main.projFrames[projectile.type] / 2), projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.position, new Vector3(0f, 0f, 1f)); //the Vector3 will be the color in rgb values, the vector2 will be your projectile's position
            Lighting.maxX = 200; //height 
            Lighting.maxY = 200; //width
            // wait 1 second
            if (timer < 40)
            {
                timer++;
                // while waiting the second
            }
            else if (timer < 40 + 30) // follow cursor
            {
                timer++;
                Vector2 dir = projectile.DirectionTo(Main.MouseWorld) * 8;
                projectile.velocity = Vector2.Lerp(projectile.velocity, dir, 0.1f);
            }
            else // normal movement
            {
            }

            if (++projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }

            if (Main.rand.Next(5) == 0) // only spawn 20% of the time
            {
                int choice = Main.rand.Next(3); // choose a random number: 0, 1, or 2
                if (choice == 0) // use that number to select dustID: 15, 57, or 58
                {
                    choice = 15;
                }
                else if (choice == 1)
                {
                    choice = 57;
                }
                else
                {
                    choice = 58;
                }
                // Spawn the dust
                Dust.NewDustPerfect(projectile.position, choice, null, 150, default(Color), 1f);
            }
        }

        public void Explode()
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int i = 0; i < 60; i++)
            {
                if (Main.rand.Next(5) == 0) // only spawn 20% of the time
                {
                    int choice = Main.rand.Next(3); // choose a random number: 0, 1, or 2
                    if (choice == 0) // use that number to select dustID: 15, 57, or 58
                    {
                        choice = 15;
                    }
                    else if (choice == 1)
                    {
                        choice = 57;
                    }
                    else
                    {
                        choice = 58;
                    }
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), (int)(0.25), (int)(0.25), choice, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].velocity *= 4f;
                    dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), (int)(0.25), (int)(0.25), choice, 0f, 0f, 100, default(Color), 1f);
                    Main.dust[dustIndex].velocity *= 2f;
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Explode();
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Explode();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Explode();
            return true;
        }
    }
}