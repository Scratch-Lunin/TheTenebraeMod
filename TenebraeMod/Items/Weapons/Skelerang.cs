using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace TenebraeMod.Items.Weapons
{
    public class Skelerang : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Skelerang");
            Tooltip.SetDefault("'It's just an extremely bent rib.'");
        }

        public override void SetDefaults() {
            item.melee = true;
            item.damage = 20;
            item.width = 20;
            item.height = 36;
            item.useTime = 25;
            item.useAnimation = 25;
            item.noUseGraphic = true;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = 8;
            item.rare = 3;
            item.shootSpeed = 12f;
            item.shoot = mod.ProjectileType ("SkelerangProjectile");
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
       class SkelerangDrops : GlobalNPC
{
	public override void NPCLoot(NPC npc)
	{
		if(npc.type == NPCID.SkeletronHead)
		{
			 if (Main.rand.NextFloat() < .105f) // 10.5% chance
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Skelerang"), 1);
                }
            // Place added drops specific to Frankenstein here
		}
		// Addtional if statements here if you would like to add drops to other vanilla npc.
	}
}
    }
}