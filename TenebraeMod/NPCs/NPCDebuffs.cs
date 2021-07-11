using System;
using Microsoft.Xna.Framework;
using TenebraeMod.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace TenebraeMod.NPCs
{
    public class NPCDebuffs : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool holyflames = false;
        public bool warriordebuff = false;
        public bool lastHitFromTrueHolyflame = false;
        int holydamage = 0;

        public override void ResetEffects(NPC npc)
        {
            holyflames = false;
            warriordebuff = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (holyflames)
            {
                if (holydamage >= npc.lifeMax / 2 && npc.lifeMax != 1920 && !npc.boss)
                {
                    holydamage = npc.lifeMax / 2;
                }
                else if (holydamage >= 960)
                {
                    holydamage = 960;
                }
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= (int)Math.Ceiling((float)holydamage / 16);
                holydamage++;
            }
            else
            {
                holydamage = 0;
            }
            if (warriordebuff)
            {
                if (npc.life < 10)
                {
                    if (npc.lifeRegen > 0)
                    {
                        npc.lifeRegen = 0;
                    }
                    npc.lifeRegen -= npc.life * 10;
                }
            }   
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            lastHitFromTrueHolyflame = projectile.type == ModContent.ProjectileType<Projectiles.Mage.TrueHolyFlame>();
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            lastHitFromTrueHolyflame = false;
        }

        public override void NPCLoot(NPC npc)
        {
            if (lastHitFromTrueHolyflame && npc.lifeMax > 5 && !npc.SpawnedFromStatue && !npc.friendly)
            {
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.ArondightHealOrb>());
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (holyflames)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, ModContent.DustType<HolyflameDust>(), npc.velocity.Y * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 5f);
                    Main.dust[dust].velocity.Y += 0.2f;
                }
                Lighting.AddLight(npc.position, 0.7f, 0.65f, 0.3f);
            }
            if (warriordebuff)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 271, 0f, 0f, 100, new Color(255, 0, 0), 1f);
                    Main.dust[dust].noGravity = true;

                    dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 270, 0f, 0f, 100, new Color(255, 0, 0), .8f);
                    Main.dust[dust].noGravity = true;
                }
            }
        }
    }
}
