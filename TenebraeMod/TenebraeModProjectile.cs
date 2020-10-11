using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod
{
    class TenebraeModProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.type == ProjectileID.NightBeam)
            {
                target.AddBuff(BuffID.CursedInferno, 240);

            }

            if (projectile.type == ProjectileID.TerraBeam)
            {
                target.AddBuff(BuffID.ShadowFlame, 60);
                target.AddBuff(BuffID.OnFire, 60);
                target.AddBuff(BuffID.Frostburn, 60);
                target.AddBuff(BuffID.CursedInferno, 60);
            }
        }
    }
}
