using TenebraeMod.Buffs;
using TenebraeMod.Dusts;
using TenebraeMod.Items;
using TenebraeMod.NPCs;
using TenebraeMod.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TenebraeMod {
	public class TenebraeModPlayer : ModPlayer {
        public bool fleshGauntlet;
        public bool holyflames;
        int holydamage = 0;

        public override void ResetEffects() {
			holyflames = false;
		}

		public override void PreUpdateBuffs() {
			fleshGauntlet = false;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit) {
            if (Main.rand.NextBool(7)) {target.AddBuff(ModContent.BuffType<Berserked>(),7*60);}
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit) {
            if (Main.rand.NextBool(7)) {target.AddBuff(ModContent.BuffType<Berserked>(),7*60);}
        }

        public override void OnHitPvp(Item item, Player target, int damage, bool crit) {
            if (Main.rand.NextBool(7)) {target.AddBuff(ModContent.BuffType<Berserked>(),7*60);}
        }

        public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit) {
            if (Main.rand.NextBool(7)) {target.AddBuff(ModContent.BuffType<Berserked>(),7*60);}
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

        public override void UpdateBadLifeRegen() {
			if (holyflames) {
				if (holydamage > 480) { 
					holydamage = 480; // Line 30 divides this by 16 and the game will divide that by 2.
				}
            	if (player.lifeRegen > 0) {
					player.lifeRegen = 0;
				}
                player.lifeRegenTime = 0;
				player.lifeRegen -= (int)Math.Ceiling((float)holydamage / 16);
				holydamage++;
			}
			else {
				holydamage = 0;
			}
		}

		public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright) {
			if (holyflames) {
				if (Main.rand.NextBool(3) && drawInfo.shadow == 0f) {
					int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, ModContent.DustType<HolyflameDust>(), player.velocity.Y * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 5f);
					Main.dust[dust].velocity.Y += 0.2f;
                    Main.playerDrawDust.Add(dust);
				}
                r *= 0.7f;
				g *= 0.65f;
				b *= 0.3f;
				fullBright = true;
			}
		}
    }
}