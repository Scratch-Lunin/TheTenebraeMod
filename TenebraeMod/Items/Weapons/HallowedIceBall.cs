using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;


		namespace TenebraeMod.Items.Weapons
		{
		public class HallowedIceBall : ModItem
		{
		public override void SetStaticDefaults() 
			{
			DisplayName.SetDefault("Hallowed Ice Ball");
			}

		public override void SetDefaults() 
			{
			item.damage = 5;
			item.ranged = true;
			item.maxStack = 999;
			item.consumable = true;
			item.width = 0;
			item.height = 0;
			item.useTime = 2;
			item.useAnimation = 2;
			item.useStyle = 1;
			item.knockBack = 1;
			item.value = 10;
			item.rare = 1;
			item.shoot = mod.ProjectileType("HallowedIceBall");
			item.shootSpeed = 8f;	
			item.UseSound = SoundID.Item1;
			item.ammo = mod.ItemType("IceBall");			
			item.noMelee = true;
			item.noUseGraphic = true;



			}
		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Snowball, 30);		
			recipe.AddIngredient(ItemID.PinkIceBlock, 5);		
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 30);
			recipe.AddRecipe();
		}
	}
}