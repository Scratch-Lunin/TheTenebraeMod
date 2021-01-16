using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Projectiles
{

	public class ScarlothCharge : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scarloth Charge");
			// Sets the amount of frames this minion has on its spritesheet
			Main.projFrames[projectile.type] = 1;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}

		public sealed override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.tileCollide = false;
			projectile.friendly = true;
			projectile.minion = true;
			//reasonn its a minion because i want it to home to enemies and im hecking lazy
			projectile.penetrate = 1;
			projectile.timeLeft = 200;
		}

		public override bool? CanCutTiles()
		{
			return true;
		}
		public override bool MinionContactDamage()
		{
			return true;
		}

		public override void AI()
		{
			//add lighting
			Lighting.AddLight(projectile.position, new Vector3(1f, 0.5f, 0f)); //the Vector3 will be the color in rgb values, the vector2 will be your projectile's position
			Lighting.maxX = 400; //height 
			Lighting.maxY = 400; //width

			Player player = Main.player[projectile.owner];

			#region General behavior
			Vector2 idlePosition = player.Center;
			idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

			Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
			float distanceToIdlePosition = vectorToIdlePosition.Length();
			if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f)
			{
				// Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
				// and then set netUpdate to true
				projectile.position = idlePosition;
				projectile.velocity *= 0.1f;
				projectile.netUpdate = true;
			}

			// If your minion is flying, you want to do this independently of any conditions
			float overlapVelocity = 0.10f;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile other = Main.projectile[i];
				if (i != projectile.whoAmI && other.active && other.owner == projectile.owner && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width)
				{
					if (projectile.position.X < other.position.X) projectile.velocity.X -= overlapVelocity;
					else projectile.velocity.X += overlapVelocity;

					if (projectile.position.Y < other.position.Y) projectile.velocity.Y -= overlapVelocity;
					else projectile.velocity.Y += overlapVelocity;
				}
			}
			#endregion

			#region Find target
			// Starting search distance
			float distanceFromTarget = 200f;
			Vector2 targetCenter = projectile.position;
			bool foundTarget = false;

			// This code is required if your minion weapon has the targeting feature
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				float between = Vector2.Distance(npc.Center, projectile.Center);
				// Reasonable distance away so it doesn't target across multiple screens
				if (between < 2000f)
				{
					distanceFromTarget = between;
					targetCenter = npc.Center;
					foundTarget = true;
				}
			}
			if (!foundTarget)
			{
				// This code is required either way, used for finding a target
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy())
					{
						float between = Vector2.Distance(npc.Center, projectile.Center);
						bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
						// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
						// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
						bool closeThroughWall = between < 100f;
						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
						{
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}

			// friendly needs to be set to true so the minion can deal contact damage
			// friendly needs to be set to false so it doesn't damage things like target dummies while idling
			// Both things depend on if it has a target or not, so it's just one assignment here
			// You don't need this assignment if your minion is shooting things instead of dealing contact damage
			projectile.friendly = foundTarget;
			#endregion

			#region Movement

			// Default movement parameters (here for attacking)
			float speed = 20f;
			float inertia = 20f;

			if (foundTarget)
			{
				// Minion has a target: attack (here, fly towards the enemy)
				if (distanceFromTarget > 40f)
				{
					// The immediate range around the target (so it doesn't latch onto it when close)
					Vector2 direction = targetCenter - projectile.Center;
					direction.Normalize();
					direction *= speed;
					projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
				}
			}
			else
			{
				if (distanceToIdlePosition > 600f)
				{
					speed = 30f;
					inertia = 60f;
				}
				else
				{
					speed = 50f;
					inertia = 80f;
				}
				if (distanceToIdlePosition > 20f)
				{
					// The immediate range around the player (when it passively floats about)

					vectorToIdlePosition.Normalize();
					vectorToIdlePosition *= speed;
					projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
				}
				else if (projectile.velocity == Vector2.Zero)
				{
					// If there is a case where it's not moving at all, give it a little "poke"
					projectile.velocity.X = -0.01f;
					projectile.velocity.Y = -0.01f;
				}
			}
			#endregion

		}
	}
}