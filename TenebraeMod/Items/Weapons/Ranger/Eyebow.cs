using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using TenebraeMod.Projectiles;
using System;

namespace TenebraeMod.Items.Weapons.Ranger
{
	public class Eyebow : ModItem
	{

		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Shoots homing eye arrows");
		}

		public override void SetDefaults() {
			item.damage = 16;
			item.ranged = true;
			item.width = 24;
			item.height = 36;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 1;
			item.value = 2000;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 7f;
			item.useAmmo = AmmoID.Arrow;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            type = ProjectileType<EyeArrow>();
            return true;
		}
	}

    internal class EyeArrow : ModProjectile 
	{
		private NPC target;
        public override void SetDefaults() 
		{
			projectile.ranged = true;
            projectile.arrow = true;
			projectile.aiStyle = -1;
			projectile.width = 14;
			projectile.height = 14;
            drawOriginOffsetY = 20;
			projectile.penetrate = 1;
			projectile.friendly = true;
			projectile.tileCollide = true;
		}
        public override void AI() {
            projectile.rotation = projectile.velocity.ToRotation() - (float)Math.PI/2;

			if (target == null || !target.active || !target.chaseable || target.dontTakeDamage) {
                float distance = 900f;
                projectile.friendly = false;
                int targetID = -1;
                for (int k = 0; k < 200; k++) {
                    if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5 && !Main.npc[k].immortal && Main.npc[k].chaseable) {
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
				float dTheta = (target.Center-projectile.Center).ToRotation()-projectile.velocity.ToRotation();
				if (dTheta > Math.PI) {
					dTheta -= 2*(float)Math.PI;
				} else if (dTheta < -Math.PI) {
					dTheta += 2*(float)Math.PI;
				}
				if (Math.Abs(dTheta) > 0.02f) {
					dTheta = (dTheta > 0) ? 0.05f : -0.05f;
				}
				projectile.velocity = projectile.velocity.RotatedBy(dTheta);
			}
		}
        public override bool OnTileCollide(Vector2 oldVelocity) {
			Main.PlaySound(SoundID.Dig, projectile.position);
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            return true;
        }
    }

	class EyebowDrops : GlobalNPC
	{
		public override void NPCLoot(NPC npc)
		{
			if(npc.type == NPCID.EyeofCthulhu)
			{
				if (Main.rand.NextBool(3) && !Main.expertMode)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Eyebow"), 1);
				}
			}
		}
	}

	class EyebowDropsBossBag : GlobalItem
	{
		public override void OpenVanillaBag(string context, Player player, int arg) {
            if (context == "bossBag") {
                if (arg == ItemID.EyeOfCthulhuBossBag) {
                    if (Main.rand.NextBool(2)) {
                        player.QuickSpawnItem(ItemType<Eyebow>());
                    }
                }
            }
        }
	}
}
