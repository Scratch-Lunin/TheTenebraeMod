using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Weapons
{
    public class SporeScepter : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spore Scepter"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Fires a spread of spores that explode into poison clouds");
        }

        public override void SetDefaults()
        {
            item.damage = 11;
            item.magic = true;
            item.mana = 5;
            item.width = 40;
            item.height = 40;
            item.useTime = 60;
            item.useAnimation = 60;
            item.noMelee = true;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SporeScepterProj");
            item.shootSpeed = 6f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.JungleSpores, 5);
            recipe.AddIngredient(ItemID.Stinger, 3);
            recipe.AddIngredient(ItemID.Vine, 1);
            recipe.AddIngredient(ItemID.RichMahogany, 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            {
                int numberProjectiles = 3;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(40));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
                return false;
            }
        }
    }
}
