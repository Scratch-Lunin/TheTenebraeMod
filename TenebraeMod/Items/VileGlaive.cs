using Microsoft.Xna.Framework;
using TenebraeMod.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Items.Weapons
{
    public class VileGlaive : ModItem
    {
        public int counter = 0;
        public int counterMax = 4;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires spear heads that explode into cursed flames");
        }

        public override void SetDefaults()
        {
            item.damage = 30;
            item.melee = true;
            item.width = 56;
            item.height = 56;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 6;
            item.UseSound = SoundID.Item1;
            Item.sellPrice(0, 4, 0, 0);
            item.rare = ItemRarityID.Pink;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<VileGlaiveProj>();
            item.shootSpeed = 3.7f;
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem() 
        }
        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (++counter >= counterMax)
            {
                counter = 0;
                float numberProjectiles = 3 + Main.rand.Next(3); // 3, 4, or 5 shots
                float rotation = MathHelper.ToRadians(45);
                position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<VileGlaiveHead>()
                    , damage, knockBack, player.whoAmI);
                }
            }
            return true;
        }
    }
}