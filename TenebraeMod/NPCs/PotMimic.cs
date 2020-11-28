using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System;
using TenebraeMod.Items.Banners;

namespace TenebraeMod.NPCs
{
    class PotMimic : ModNPC
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pot Mimic");
			Main.npcFrameCount[npc.type] = 18;
		}

		public override void SetDefaults()
		{
			npc.width = 28;
			npc.height = 44;
			npc.aiStyle = 25;
			npc.damage = 30;
			npc.defense = 15;
			npc.lifeMax = 300;
			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath6;
			npc.value = 5000f;
			aiType = NPCID.Mimic;
			animationType = NPCID.Mimic;
			banner = npc.type;
			bannerItem = ItemType<PotMimicBanner>();
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.UndergroundMimic.Chance;
		}
	}
}

