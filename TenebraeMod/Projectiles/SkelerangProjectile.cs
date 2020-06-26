using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles
{
	public class SkelerangProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SkelerangProjectile");
		}

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

		// Additional hooks/methods here.
	}
}