using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace TenebraeMod.Items.Weapons
{
    public class LuminiteSpikyBall : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Luminite Spiky Ball");
            Tooltip.SetDefault("Homes and electrifies on hit");
        }

        public override void SetDefaults() {
            item.thrown = true;
            item.maxStack = 999;
            item.consumable = true;
            item.damage = 30;
            item.width = 14;
            item.height = 14;
            item.useTime = 15;
            item.useAnimation = 15;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 1;
            item.value = 6;
            item.rare = ItemRarityID.Red;
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("LuminiteSpikyBallProjectile");
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpikyBall,111);
            recipe.AddIngredient(ItemID.LunarBar);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this,111);
            recipe.AddRecipe();
        }
    }

    internal class LuminiteSpikyBallProjectile : ModProjectile {
        private NPC target;
		public override string Texture => "TenebraeMod/Items/Weapons/LuminiteSpikyBall";
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Luminite Spiky Ball");
		}

		public override void SetDefaults() {
			projectile.thrown = true;
			projectile.aiStyle = -1;
			projectile.width = 18;
			projectile.height = 18;
			projectile.penetrate = 1;
			projectile.friendly = true;
			projectile.tileCollide = false;
            projectile.light = 0.5f;
		}

        public override void AI() {
            if (target == null || !target.active || !target.chaseable || target.dontTakeDamage) {
                float distance = 900f;
                projectile.friendly = false;
                int targetID = -1;
                for (int k = 0; k < 200; k++) {
                    if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && !Main.npc[k].immortal && Main.npc[k].chaseable) {
                        Vector2 newMove = Main.npc[k].Center - Main.MouseWorld;
                        float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                        if (distanceTo < distance) {
                            targetID = k;
                            distance = distanceTo;
                            projectile.friendly = true;
                        }
                    }
                }
                if (!projectile.friendly) {
                    target = null;
                } else {
                    target = Main.npc[targetID];
                }
                projectile.friendly = true;
            }

            if (target!=null) {
                projectile.velocity += new Vector2(0.1f,0).RotatedBy((target.Center-projectile.Center).ToRotation()) + (target.velocity-projectile.velocity)/60;
                projectile.velocity *= 1.002f;
            } else {
                projectile.velocity *= 0.99f;
            }
            if (projectile.velocity.Length()>16) {
                projectile.velocity.Normalize();
                projectile.velocity*=16;
            }
            projectile.rotation += projectile.velocity.X * 0.1f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            Main.PlaySound(SoundID.Item,projectile.Center,93);
            Projectile.NewProjectile(projectile.Center,Vector2.Zero,ProjectileID.Electrosphere,damage,knockback,projectile.owner);
        }
    }
}