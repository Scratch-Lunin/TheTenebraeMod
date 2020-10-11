using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using TenebraeMod.Buffs;

namespace TenebraeMod {
	public class TenebraeModPlayer : ModPlayer {
        public bool fleshGauntlet;

		public override void PreUpdateBuffs() {
			fleshGauntlet = false;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit) {
            if (Main.rand.NextBool(7)) {target.AddBuff(BuffType<Berserked>(),7*60);}
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit) {
            if (Main.rand.NextBool(7)) {target.AddBuff(BuffType<Berserked>(),7*60);}
        }

        public override void OnHitPvp(Item item, Player target, int damage, bool crit) {
            if (Main.rand.NextBool(7)) {target.AddBuff(BuffType<Berserked>(),7*60);}
        }

        public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit) {
            if (Main.rand.NextBool(7)) {target.AddBuff(BuffType<Berserked>(),7*60);}
        }

        public override void ModifyWeaponDamage(Item item, ref float add, ref float mult, ref float flat)
        {
            if (player.HasBuff(mod.BuffType("HolyRetribution")) && (item.type == ItemID.Excalibur || item.type == ItemID.TrueExcalibur))
                add += 0.2f;
        }

        public override float UseTimeMultiplier(Item item)
        {
            if (player.HasBuff(mod.BuffType("HolyRetribution")) && (item.type == ItemID.Excalibur || item.type == ItemID.TrueExcalibur))
            {
                return 1.2f;
            }
            return base.UseTimeMultiplier(item);
        }
    }
}