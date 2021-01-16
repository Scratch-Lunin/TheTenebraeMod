using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.NPCs
{
	// This ModNPC serves as an example of a complete AI example.
	public class DivineBeamer : ModNPC
	{
		private int timer;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flutter Slime"); // Automatic from .lang files
			Main.npcFrameCount[npc.type] = 6; // make sure to set this for your modnpcs.
		}

		public override void SetDefaults()
		{
			npc.width = 36;
			npc.height = 58;
			npc.aiStyle = 2;
			aiType = NPCID.DemonEye;
			npc.damage = 40;
			npc.defense = 20;
			npc.lifeMax = 800;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
			//npc.alpha = 175;
			//npc.color = new Color(0, 80, 255, 100);
			npc.value = 1050f;
		}

		public override bool PreAI()
		{
			npc.rotation = (Main.player[npc.target].Center - npc.Center).ToRotation() + MathHelper.Pi / 2;
			timer++;
			if (timer == 240)
			{
				timer = 0;
			}
			else if (timer % 60 == 0)
			{
				Projectile.NewProjectile(npc.Center, 12 * (Main.player[npc.target].Center - npc.Center) / (Main.player[npc.target].Center - npc.Center).Length(), ProjectileID.PinkLaser, 80, 6, Main.myPlayer);
			}

			return true;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			// we would like this npc to spawn in the overworld.
			return SpawnCondition.EnchantedSword.Chance *0.8f;
		}

		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			npc.frameCounter++;
			if (npc.frameCounter >= 8)
			{
				npc.frame.Y += frameHeight;
				npc.frameCounter = 0;
				if (npc.frame.Y >= Main.npcFrameCount[npc.type] * frameHeight)
				{
					npc.frame.Y = 0;
				}
			}
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			var GlowMask = mod.GetTexture("NPCs/DivineBeamer_glowmask");
			var Effects = npc.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(GlowMask, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame, Color.White, npc.rotation, npc.frame.Size() / 2, npc.scale, Effects, 0);
		}
	}
}