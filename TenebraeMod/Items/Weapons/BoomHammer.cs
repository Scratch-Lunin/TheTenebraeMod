
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;


namespace TenebraeMod.Items.Weapons
{
	public class BoomHammer : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Boom Hammer"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Sends a explosive shockwave");
		}

		public override void SetDefaults() 
		{
			item.damage = 120;
			item.melee = true;
			item.width = 74;
			item.height = 74;
			item.useTime = 50;
			item.useAnimation = 50;
			item.useStyle = 1;
			item.knockBack = 8;
			item.value = 10000;
			item.rare = 6;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.shootSpeed = 40f;	
		item.shoot = mod.ProjectileType("BoomHammerWave");
		}

	public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)

        {float numberProjectiles = 10; // 3 shots
            float rotation = MathHelper.ToRadians(85);//Shoots them in a 45 degree radius. (This is technically 90 degrees because it's 45 degrees up from your cursor and 45 degrees down)
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 85f; //45 should equal whatever number you had on the previous line
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
			recipe.AddIngredient(ItemID.Dynamite, 50);
			recipe.AddIngredient(ItemID.HallowedBar, 10);
			recipe.AddIngredient(ItemID.Wire, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
}}