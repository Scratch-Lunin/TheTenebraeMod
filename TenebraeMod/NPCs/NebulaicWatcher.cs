using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace TenebraeMod.NPCs
{
    public class NebulaicWatcher : ModNPC
    {
        private int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nebulaic Watcher​");
        }

        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 34;
            npc.lifeMax = 1200;
            npc.damage = 80;
            npc.defense = 20;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 2500f;
            npc.knockBackResist = 0f;
            npc.aiStyle = 2;
            aiType = NPCID.DemonEye;

        }

        public override bool PreAI() {
            npc.rotation = (Main.player[npc.target].Center-npc.Center).ToRotation()+MathHelper.Pi/2;
            timer++;
            if (timer == 240) {
                timer = 0;
            } else if (timer % 60 == 0) {
                Projectile.NewProjectile(npc.Center,12*(Main.player[npc.target].Center-npc.Center)/(Main.player[npc.target].Center-npc.Center).Length(),ProjectileID.NebulaLaser,80,6,Main.myPlayer);
            }
            
            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
        }

        /*public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneSkyHeight && !Main.dayTime && NPC.downedMoonlord) ? (0.25f) : 0f;

        }*/
        /*public override void NPCLoot()
        {
            if (Main.rand.Next(5) == 0)
                Item.NewItem(npc.getRect(), ItemID.SoulofLight);
            if (Main.rand.Next(5) == 0)
                Item.NewItem(npc.getRect(), ItemID.SoulofNight);
        }*/

        public override void SendExtraAI(System.IO.BinaryWriter writer) {
            writer.Write(timer);
        }

        public override void ReceiveExtraAI(System.IO.BinaryReader reader) {
            timer = reader.ReadInt32();
        }
    }
}

   