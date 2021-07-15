using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles.Melee
{
	public class SlimeSpike : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Friendly Slime Spike");
		}

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 20;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.aiStyle = 1;
			projectile.damage = 10;
		}



		public override void PostAI()
		{
			if (Main.rand.NextBool())
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 253);
				dust.noGravity = false;
				dust.scale = 0.4f;
			}
		}
	}
}