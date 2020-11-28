using Microsoft.Xna.Framework;
using TenebraeMod.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Items.Weapons
{
    public class BlackHoleCannon : ModItem
    {
        private int channelTimer;
        private bool consume;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blackhole Cannon");
            Tooltip.SetDefault("66% chance not to consume ammo" + "\n'Annihilate your enemies with the power of a singularity!'");
        }

        public override void SetDefaults()
        {
            item.damage = 50;
            item.ranged = true;
            item.width = 74;
            item.height = 32;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 7;
            item.value = 100000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item22;
            item.autoReuse = true;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 16f;
            item.channel = true;
            item.useAmmo = AmmoID.Rocket;
        }

        public override bool ConsumeAmmo(Player player)
        {
            bool oldConsume = consume;
            consume = false;
            return oldConsume && Main.rand.NextFloat() >= .25f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentVortex, 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void HoldItem(Player player)
        {
            if (player.channel)
            {
                channelTimer++;
            }
            else
            {
                channelTimer = 0;
                consume = true;
            }
            if (channelTimer >= 2 * item.useTime)
            {
                int ammoDamage = 0;
                for (int i = 0; i < player.inventory.Length; i++)
                {
                    int j = (i + 54) % 58;
                    if (player.inventory[j].ammo == item.useAmmo)
                    {
                        ammoDamage = player.inventory[j].damage;
                        break;
                    }
                }

                player.channel = false;
                channelTimer = 0;
                if (Main.rand.NextBool(4))
                {
                    //shoot gravimine
                    Vector2 velocity = Main.MouseWorld - player.Center;
                    if (velocity.Length() > 0)
                    {
                        velocity.Normalize();
                    }
                    velocity *= item.shootSpeed;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<Gravamine>(), item.damage + ammoDamage, item.knockBack, player.whoAmI);
                }
                //set bullet type
                Main.PlaySound(SoundID.Item, player.Center, 61);
                for (int i = 0; i < Main.rand.Next(3, 9); i++)
                {
                    Vector2 velocity = Main.MouseWorld - player.Center;
                    if (velocity.Length() > 0)
                    {
                        velocity.Normalize();
                    }
                    velocity *= item.shootSpeed;
                    velocity = velocity.RotatedByRandom(0.5f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<VortexRocket>(), item.damage + ammoDamage, item.knockBack, player.whoAmI);
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return false;
        }
    }
}
