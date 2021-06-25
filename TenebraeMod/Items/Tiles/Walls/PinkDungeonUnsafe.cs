using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Items.Tiles.Walls
{
    public class PinkDungeonUnsafe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unsafe Pink Brick Wall");
            Tooltip.SetDefault("Places unsafe Dungeon walls" + "\nEnemies will spawn when enough walls and Dungeon brick are placed" + "\nCan only be mined at its edge");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.maxStack = 999;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 15;
            item.useAnimation = 15;
            item.value = 0;
            item.useTurn = true;
            item.autoReuse = true;
            item.rare = ItemRarityID.White;
            item.createWall = 9;
            // Set other item.X values here
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod); // 1 Brick ---> 4 Unsafe Walls
            recipe.AddIngredient(ItemID.PinkBrick);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 4);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod); // 4 Unsafe Walls ---> 1 Brick
            recipe.AddRecipeGroup("TenebraeMod:UnsafePinkBrickWall", 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.PinkBrick);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod); // 1 Wall ---> 1 Unsafe Wall
            recipe.AddIngredient(ItemID.PinkBrickWall);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod); // 1 Unsafe Wall ---> 1 Wall
            recipe.AddIngredient(null, "PinkDungeonUnsafe");
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.PinkBrickWall);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod); // 4 Walls ---> 1 Brick
            recipe.AddRecipeGroup("TenebraeMod:PinkBrickWall", 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.PinkBrick);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod); // 1 Brick ---> 4 Walls
            recipe.AddIngredient(ItemID.PinkBrick);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.PinkBrickWall, 4);
            recipe.AddRecipe();
        }
    }
}