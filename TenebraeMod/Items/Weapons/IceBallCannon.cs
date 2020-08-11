
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;


namespace TenebraeMod.Items.Weapons
{
	public class IceBallCannon : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Ice Ball Cannon"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("20% chance not to consume ammo");
		}

		public override void SetDefaults() 
		{
			item.damage = 35;
			item.ranged = true;
			item.width = 66;
			item.height = 31;
			item.useTime = 12;
			item.useAnimation = 12;
			item.useStyle = 5;
			item.knockBack = 4;
			item.value = 10000;
			item.noMelee = true;
			item.rare = 5;
			item.UseSound = SoundID.Item10;
			item.autoReuse = true;
			item.shootSpeed = 20f;
			item.useAmmo = mod.ItemType("IceBall");			
			item.shoot = mod.ProjectileType("IceBall"); 
		}	
		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);	
			recipe.AddIngredient(ItemID.SnowballCannon, 1);
			recipe.AddIngredient(ItemID.FrostCore, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .20f;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
}}