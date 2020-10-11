using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace TenebraeMod.Items.Weapons
{
    public class Pumpscythe : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("The Pumpking's strongest weapon, ripped from his body.");
        }

        public override void SetDefaults()
        {
            item.width = 70;
            item.height = 72;
            item.value = 300000;
            item.rare = ItemRarityID.Yellow;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 30;
            item.useTime = 20;
            item.autoReuse = true;
            item.damage = 120;
            item.melee = true;
            item.knockBack = 3f;
            item.shootSpeed = 8f;
            item.shoot = mod.ProjectileType("PumpkinScythe");
            item.UseSound = SoundID.Item1;
            // Set other item.X values here
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Items/Weapons/Pumpscythe_glowmask");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void AddRecipes()
        {
            // Recipes here. See Basic Recipe Guide
        }
    }
}