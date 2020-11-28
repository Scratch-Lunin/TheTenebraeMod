using TenebraeMod.Dusts;
using TenebraeMod.Items;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.NPCs
{
	public class NPCDebuffs : GlobalNPC
	{
		public override bool InstancePerEntity => true;

		public bool holyflames = false;
		int timepassed = 0;

		public override void ResetEffects(NPC npc) {
			holyflames = false;
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage) {
			if (holyflames) {
				if (timepassed > 960) { 
					timepassed = 960; // Line 30 divides this by 16 and the game will divide that by 2.
				}
            	if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= (int)Math.Ceiling((float)timepassed / 16);
				timepassed++;
			}
			else {
				timepassed = 0;
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor) {
			if (holyflames) {
				if (Main.rand.Next(4) < 3) {
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<HolyflameDust>(), npc.velocity.Y * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 5f);
					Main.dust[dust].velocity.Y += 0.2f;
				}
				Lighting.AddLight(npc.position, 0.7f, 0.65f, 0.3f);
			}
		}
	}
}