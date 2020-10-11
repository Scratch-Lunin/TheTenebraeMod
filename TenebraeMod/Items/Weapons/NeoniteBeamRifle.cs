using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using TenebraeMod.Projectiles;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace TenebraeMod.Items.Weapons
{
	public class NeoniteBeamRifle : ModItem
	{

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Converts bullers into neon beams");
		}

		public override void SetDefaults()
		{
			item.damage = 30;
			item.ranged = true;
			item.width = 78;
			item.height = 28;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 3;
			item.value = 150000;
			item.rare = ItemRarityID.Orange;
			item.autoReuse = true;
			item.UseSound = SoundID.Item33;
			item.shoot = mod.ProjectileType("NeonBeam");
			item.shootSpeed = 10f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = ProjectileType<NeonBeam>();
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = mod.GetTexture("Items/Weapons/NeoniteBeamRifle_glowmask");
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
	}
}
