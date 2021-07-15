
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;


namespace TenebraeMod.Items.Weapons.Melee
{
	public class GemWarhammer : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Gem Warhammer "); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Sends a gem shockwave");
		}

		public override void SetDefaults() 
		{
			item.damage = 18;
			item.melee = true;
			item.width = 74;
			item.height = 74;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 4;
			item.value = 100000;
			item.rare = ItemRarityID.Pink;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shootSpeed = 125f;	
		item.shoot = mod.ProjectileType("GemHammerWave");
		}

	public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)

        {float numberProjectiles = 5 ; // 3 shots
            float rotation = MathHelper.ToRadians(20);//Shoots them in a 45 degree radius. (This is technically 90 degrees because it's 45 degrees up from your cursor and 45 degrees down)
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 20f; //45 should equal whatever number you had on the previous line
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Vector for spread. Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI); //Creates a new projectile with our new vector for spread.
            }
            return false; //makes sure it doesn't shoot the projectile again after this
        }

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.GetItem("StoneSledgehammer"));
			recipe.AddRecipeGroup("Wood", 10);
			recipe.AddIngredient(ItemID.Ruby, 2);
			recipe.AddIngredient(ItemID.Amber, 2);
			recipe.AddIngredient(ItemID.Topaz, 2);
			recipe.AddIngredient(ItemID.Emerald, 2);
			recipe.AddIngredient(ItemID.Sapphire, 2);
			recipe.AddIngredient(ItemID.Amethyst, 2);
			recipe.AddIngredient(ItemID.Diamond, 2);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
}}