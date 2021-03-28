using TenebraeMod.Items.Weapons;
using TenebraeMod.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace TenebraeMod.Items.Misc
{
	public class InpuratusBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 40;
			item.height = 42;
			item.rare = ItemRarityID.Pink;
			item.expert = true;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
			player.TryGettingDevArmor();
			player.TryGettingDevArmor();
			player.QuickSpawnItem(ItemID.CursedFlame, 20 + Main.rand.Next(10));
			player.QuickSpawnItem(ItemID.RottenChunk, 50 + Main.rand.Next(10));

			var dropChooser = new WeightedRandom<int>();
			dropChooser.Add(ItemID.ClingerStaff, 5);
			dropChooser.Add(ModContent.ItemType<Items.Weapons.VileGlaive>(), 5);
			dropChooser.Add (ModContent.ItemType<Items.Weapons.CursedCannon>(), 5);
			player.QuickSpawnItem(dropChooser);
			int choice = dropChooser;

			player.QuickSpawnItem(ItemID.WormScarf);
		}

		public override int BossBagNPC => ModContent.NPCType<Inpuratus>();
	}
}