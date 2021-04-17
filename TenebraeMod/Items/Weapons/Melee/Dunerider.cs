using TenebraeMod.Projectiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace TenebraeMod.Items.Weapons.Melee
{
	class Dunerider : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fires sand particles at nearby enemies");
			ItemID.Sets.Yoyo[item.type] = true;
			ItemID.Sets.GamepadExtraRange[item.type] = 15;
			ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.width = 32;
			item.height = 28;
			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 16f;
			item.knockBack = 5f;
			item.damage = 40;
			item.rare = ItemRarityID.LightRed;

			item.melee = true;
			item.channel = true;
			item.noMelee = true;
			item.noUseGraphic = true;

			item.UseSound = SoundID.Item1;
			item.value = Item.sellPrice(gold: 4);
			item.shoot = ProjectileType<DuneriderProjectile>();
		}
	}


	internal class DuneriderProjectile : ModProjectile
	{
		private int timer;
		private NPC target;
		public override void SetStaticDefaults()
		{
			// The following sets are only applicable to yoyo that use aiStyle 99.
			ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 11f;
			ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 300f;
			ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 13f;
		}

		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 99;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
		}

		public override void AI()
		{
			timer++;
			if (timer == 5)
			{
				target = null;
				timer = 0;
				float distance = 150f;
				projectile.friendly = false;
				int targetID = -1;
				for (int k = 0; k < 200; k++)
				{
					if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && !Main.npc[k].immortal && Main.npc[k].chaseable)
					{
						Vector2 newMove = Main.npc[k].Center - Main.MouseWorld;
						float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
						if (distanceTo < distance)
						{
							targetID = k;
							distance = distanceTo;
							projectile.friendly = true;
						}
					}
				}
				if (projectile.friendly)
				{
					target = Main.npc[targetID];

					Vector2 shotVelocity = target.Center - projectile.Center;
					shotVelocity.Normalize();
					shotVelocity *= 5;
					Vector2 offset = new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5));
					int shot = Projectile.NewProjectile(projectile.Center + offset, shotVelocity, mod.ProjectileType("SandParticles"), projectile.damage / 2, projectile.knockBack, projectile.owner);
					Main.projectile[shot].minion = false;
					Main.projectile[shot].melee = true;
				}
				projectile.friendly = true;
			}
		}
	}

	internal class SandParticles : ModProjectile
    {
		public override string Texture => "TenebraeMod/Items/Weapons/Melee/DuneriderProjectile";
		public override void SetDefaults()
		{
			projectile.alpha = 255;
			projectile.width = 5;
			projectile.height = 5;
			projectile.friendly = true;
			projectile.penetrate = 2;
			projectile.melee = true;
			projectile.timeLeft = 120;
		}

        public override void AI()
        {
			if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 10, (float)Main.rand.Next(-2, 2), (float)Main.rand.Next(-2, 2), 50);
				dust.noGravity = false;
			}
			projectile.velocity.X *= .99f;
			projectile.velocity.Y *= .98f;
        }
    }

	public class DuneriderDrop : GlobalNPC
	{
		public override void NPCLoot(NPC npc)
		{
			if (npc.type == NPCID.Tumbleweed && Main.hardMode)
			{
				int chance = Main.expertMode ? 4 : 2; // 2% chance in normal, 4% in expert
				if (Main.rand.Next(100) < chance)
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Dunerider"));
			}
		}
	}
}
