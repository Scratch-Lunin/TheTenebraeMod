using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using TenebraeMod.NPCs;
using Microsoft.Xna.Framework;


namespace TenebraeMod.Items.Weapons.Melee
{
	public class PotMimicSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Clay-More"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Use <right> to long jump");
		}

		public override void SetDefaults()
		{
			item.damage = 25;
			item.melee = true;
			item.width = 58;
			item.height = 56;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 3;
			item.value = 300000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item1;
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.useTime = 50;
				item.useAnimation = 50;
				item.damage = 35;
				player.velocity += new Vector2(6 * player.direction, -6);
				Dust.NewDust(player.position - new Vector2(2f, 1f), player.width + 4, player.height + 4, 31, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3.5f);
				Dust.NewDust(player.position - new Vector2(2f, 5f), player.width + 4, player.height + 4, 31, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3.5f);
				Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 31, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3.5f);
				Dust.NewDust(player.position - new Vector2(3f, 5f), player.width + 4, player.height + 4, 31, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3.5f);
				Dust.NewDust(player.position - new Vector2(4f, 5f), player.width + 4, player.height + 4, 31, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3.5f);
				Dust.NewDust(player.position - new Vector2(2f, 6f), player.width + 4, player.height + 4, 31, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3.5f);
				Dust.NewDust(player.position - new Vector2(2f, 5f), player.width + 4, player.height + 4, 31, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3.5f);

			}
			else
			{
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.useTime = 20;
				item.useAnimation = 20;
				item.damage = 25;
			}
			return base.CanUseItem(player);
		}

		public class PotMimicSwordDrops : GlobalNPC
		{
			public override void NPCLoot(NPC npc)
			{
				if (npc.type == ModContent.NPCType<PotMimic>())
				{
					if (Main.rand.NextFloat() < 1f) // 13.23% chance
					{
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PotMimicSword"), 1);
					}
				}
			}
		}
	}
}