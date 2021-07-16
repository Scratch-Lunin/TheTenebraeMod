using TenebraeMod.Buffs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using static Terraria.ModLoader.ModContent;
using TenebraeMod.Items.Accessories;

namespace TenebraeMod 
{
    public class HandWarmerDrops : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == (147 | 184 | 150 | 206))
            {
                if (Main.rand.Next(100) == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1921, 1);
                }
            }

            if (npc.type == (243))
            {
                if (Main.rand.Next(4) == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1921, 1);
                }
            }
        }
    }
    public class TenebraeModNPC : GlobalNPC 
    {
        public override bool InstancePerEntity => true;

        /*public override void NPCLoot(NPC npc) {
            switch(npc.type) {
                case NPCID.BlackRecluse:
                    if (Main.expertMode ? Main.rand.NextBool(25) : Main.rand.NextBool(50)) {
                        Item.NewItem(npc.Hitbox,ItemType<VenomAntidote>());
                    }
                    break;
            }
        }*/
    }
}