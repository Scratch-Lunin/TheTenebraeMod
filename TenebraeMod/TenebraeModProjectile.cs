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
        }
    }
}
