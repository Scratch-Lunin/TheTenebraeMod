using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod {
	public class TenebraeModItem : GlobalItem {
		public override bool InstancePerEntity => true;

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("Wood",5);
            recipe.AddIngredient(ItemID.GoldBar,3);
            recipe.AddRecipeGroup("IronBar",2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.GoldChest);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("Wood",5);
            recipe.AddIngredient(ItemID.PlatinumBar,3);
            recipe.AddRecipeGroup("IronBar",2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.GoldChest);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DemoniteBrick,8);
            recipe.AddRecipeGroup("IronBar",2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.ShadowChest);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimtaneBrick,8);
            recipe.AddRecipeGroup("IronBar",2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.ShadowChest);
            recipe.AddRecipe();

            recipe.AddRecipeGroup("Wood",8);
            recipe.AddIngredient(ItemID.JungleSpores,2);
            recipe.AddRecipeGroup("IronBar",2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.IvyChest);
            recipe.AddRecipe();
            
            recipe.AddIngredient(ItemID.Coral,8);
            recipe.AddRecipeGroup("IronBar",2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.WaterChest);
            recipe.AddRecipe();
            
            recipe.AddIngredient(ItemID.Chest);
            recipe.AddIngredient(ItemID.Cobweb,8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.WebCoveredChest);
            recipe.AddRecipe();
            
            recipe.AddIngredient(ItemID.Chest,5);
            recipe.AddRecipeGroup(ItemID.SnowBlock,25);
            recipe.AddRecipeGroup(ItemID.IceBlock,25);
            recipe.AddRecipeGroup(ItemID.FrozenKey);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.FrozenChest,5);
            recipe.AddRecipe();
            
            recipe.AddIngredient(ItemID.Chest,5);
            recipe.AddRecipeGroup(ItemID.MudBlock,25);
            recipe.AddRecipeGroup(ItemID.JungleGrassSeeds,25);
            recipe.AddRecipeGroup(ItemID.JungleKey);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.JungleChest,5);
            recipe.AddRecipe();
            
            recipe.AddIngredient(ItemID.Chest,5);
            recipe.AddRecipeGroup(ItemID.PearlstoneBlock,50);
            recipe.AddRecipeGroup(ItemID.HallowedKey);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.HallowedChest,5);
            recipe.AddRecipe();
            
            recipe.AddIngredient(ItemID.Chest,5);
            recipe.AddRecipeGroup(ItemID.EbonstoneBlock,50);
            recipe.AddRecipeGroup(ItemID.CorruptionKey);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.CorruptionChest,5);
            recipe.AddRecipe();
            
            recipe.AddIngredient(ItemID.Chest,5);
            recipe.AddRecipeGroup(ItemID.CrimstoneBlock,50);
            recipe.AddRecipeGroup(ItemID.CrimsonKey);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.CrimsonChest,5);
            recipe.AddRecipe();
        }
    }
}