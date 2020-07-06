using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles
{
	public class MecharangProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("MecharangProjectile");
		}

		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 36;
			projectile.aiStyle = 3;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = 6;
            projectile.timeLeft = 800;
            projectile.light = 0.8f;
            projectile.extraUpdates = 2;
			projectile.maxPenetrate = 6;
		}

		// Additional hooks/methods here.
	}
}