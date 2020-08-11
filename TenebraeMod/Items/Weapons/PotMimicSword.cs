
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;


		namespace TenebraeMod.Items.Weapons
		{
		public class PotMimicSword : ModItem
		{
		public override void SetStaticDefaults() 
			{
			DisplayName.SetDefault("Clay-More"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Hold right click to long-jump");
			}

		public override void SetDefaults() 
			{
			item.damage = 25;
			item.melee = true;
			item.width = 58;
			item.height = 56;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 3;
			item.value = 300000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			}
		public override bool CanUseItem(Player player)
			{
            player.dash = 1;
		   return true;
		}}}