using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace TenebraeMod.Projectiles
{
	public class PumpkinScythe : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.FlamingScythe);
			aiType = ProjectileID.FlamingScythe;
			projectile.width = 106;
			projectile.height = 84;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.melee = true;
		}
		public override void AI()
		{
			//add lighting
			Lighting.AddLight(projectile.position, new Vector3(1f, 0.5f, 0f)); //the Vector3 will be the color in rgb values, the vector2 will be your projectile's position
			Lighting.maxX = 400; //height 
			Lighting.maxY = 400; //width
		}
		// Additional hooks/methods here.
	}
}