using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace TenebraeMod.Projectiles
{
	public class NeonBeam : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.PurpleLaser);
			aiType = ProjectileID.PurpleLaser;
			projectile.width = 1;
			projectile.height = 32;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.melee = true;
			projectile.light = .5f;
			projectile.ranged = true;
		}

		public override void AI()
		{
			//add lighting
			Lighting.AddLight(projectile.position, new Vector3(1f, 0.05f, 0f)); //the Vector3 will be the color in rgb values, the vector2 will be your projectile's position
			Lighting.maxX = 400; //height 
			Lighting.maxY = 400; //width
		}
	}
}
