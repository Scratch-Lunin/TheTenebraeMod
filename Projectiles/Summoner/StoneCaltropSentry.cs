using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles.Summoner
{
    public class StoneCaltropSentry : ModProjectile
	{
        private int attackCooldown;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stone Caltrop");
		}

		public override void SetDefaults()
		{
			projectile.width = 26;
			projectile.height = 24;
			projectile.penetrate = -1;
			projectile.sentry = true;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			projectile.minionSlots = 0.25f;
			projectile.timeLeft *= 2;
		}

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			damage += target.checkArmorPenetration(5);
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			return false;
        }

        public override void AI()
        {
			projectile.velocity.Y += 1f;
		}

        public override bool? CanCutTiles()
		{
			return false;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Bleeding, 120);
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}
		public override bool MinionContactDamage()
		{
			return attackCooldown >= 40 - 7;
		}
	}
}