using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace TenebraeMod.Items.Weapons
{
    public class DeathNote : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death Note");
            Tooltip.SetDefault("[ Unobtainable Item ]\n'Instead of writing the name, tap the enemy for it to die.'");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.noMelee = true;
            item.useTime = 1;
            item.useAnimation = 1;
            item.reuseDelay = 4;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.value = 0;
            item.rare = ItemRarityID.Red;
            item.shoot = mod.ProjectileType("DeathNote");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld;
            damage = 25;
            return true;
        }
    }
}
