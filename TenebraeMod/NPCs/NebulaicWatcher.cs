using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TenebraeMod.Items.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;

namespace TenebraeMod.NPCs
{
    public class NebulaicWatcher : ModNPC
    {
        private int timer;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flutter Slime"); // Automatic from .lang files
			Main.npcFrameCount[npc.type] = 3; // make sure to set this for your modnpcs.
        }

        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 58;
            npc.lifeMax = 1500;
            npc.damage = 120;
            npc.defense = 40;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.value = 2500f;
            npc.knockBackResist = 0.5f;
            npc.aiStyle = 2;
            aiType = NPCID.DemonEye;
            banner = npc.type;
            bannerItem = ItemType<NebulaicWatcherBanner>();
            npc.buffImmune[BuffID.Bleeding] = true;
            npc.buffImmune[BuffID.Confused] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Ichor] = true;
            npc.buffImmune[BuffID.Oiled] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.Wet] = true;
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
                Projectile.NewProjectile(npc.Center, 12 * (Main.player[npc.target].Center - npc.Center) / (Main.player[npc.target].Center - npc.Center).Length(), ProjectileID.NebulaLaser, 80, 6, Main.myPlayer);
            }

            return true;
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



        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneSkyHeight && NPC.downedMoonlord) ? (0.25f) : 0f;

        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(5) == 0)
                Item.NewItem(npc.getRect(), ItemID.SoulofLight);
            if (Main.rand.Next(5) == 0)
                Item.NewItem(npc.getRect(), ItemID.SoulofNight);
        }

        public override void SendExtraAI(System.IO.BinaryWriter writer)
        {
            writer.Write(timer);
        }

        public override void ReceiveExtraAI(System.IO.BinaryReader reader)
        {
            timer = reader.ReadInt32();
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            var GlowMask = mod.GetTexture("NPCs/NebulaicWatcher_glowmask");
            var Effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(GlowMask, npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame, Color.White, npc.rotation, npc.frame.Size() / 2, npc.scale, Effects, 0);
        }
    }
}

