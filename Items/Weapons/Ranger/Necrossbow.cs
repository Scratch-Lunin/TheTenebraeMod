using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace TenebraeMod.Items.Weapons.Ranger
{
	public class Necrossbow : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Converts wooden arrows into bone arrows" + "\n33% chance not to consume ammo");
		}

		public override void SetDefaults()
		{
			item.damage = 50;
			item.ranged = true;
			item.width = 60;
			item.height = 24;
			item.useTime = 50;
			item.useAnimation = 50;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 7;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 8f;
			item.useAmmo = ItemID.WoodenArrow;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 30);
			recipe.AddIngredient(ItemID.Cobweb, 40);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() > .33f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileID.WoodenArrowFriendly)
			{
				type = ProjectileID.BoneArrow;
			}
			return true;
		}
	}
}