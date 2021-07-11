using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TenebraeMod.Projectiles.Melee
{
	public class WarriorsBaneSlash : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Warrior's Bane Slash");
			Main.projFrames[projectile.type] = 4;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 24;
			projectile.height = 50;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.timeLeft = 300;
			projectile.penetrate = 9999;
		}

		public override void AI()
        {
			projectile.rotation = projectile.velocity.ToRotation(); // projectile faces sprite right
			projectile.velocity *= 1.02f;
			// Loop through the 4 animation frames, spending 5 ticks on each.
			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= Main.projFrames[projectile.type])
				{
					projectile.frame = 0;
				}
			}

			//add lighting
			Lighting.AddLight(projectile.position, new Vector3(1f, 0f, 0f)); //the Vector3 will be the color in rgb values, the vector2 will be your projectile's position
			Lighting.maxX = 400; //height 
			Lighting.maxY = 400; //width
		}
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			damage = target.lifeMax;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{ // TODO: Add OnHitPlayer method for PVP?
			target.AddBuff(ModContent.BuffType<Buffs.WarriorsAnimosity>(), 600, true);
		}
	}
}