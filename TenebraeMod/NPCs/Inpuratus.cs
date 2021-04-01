using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TenebraeMod.Items.Misc;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;

namespace TenebraeMod.NPCs
{
    [AutoloadBossHead]
    public class Inpuratus : ModNPC
    {
        public int Ohyeahphase2;
        public int lmaotheplayerisdead;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inpuratus");
            Main.npcFrameCount[npc.type] = 1;
            NPCID.Sets.TrailCacheLength[npc.type] = 5;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetDefaults()
        {
            music = MusicID.Boss3;
            musicPriority = MusicPriority.BossLow;
            npc.width = 118;
            npc.height = 188;
            npc.damage = 30;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.defense = 30;
            npc.lifeMax = 25000;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 10, 0, 0);
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.buffImmune[BuffID.Confused] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            bossBag = ModContent.ItemType<InpuratusBag>();
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.7f);
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                Item.NewItem(npc.position, ItemID.CursedFlame, 20 + Main.rand.Next(10));
                Item.NewItem(npc.position, ItemID.RottenChunk, 50 + Main.rand.Next(10));
                var dropChooser = new WeightedRandom<int>();
                dropChooser.Add(ItemID.ClingerStaff, 5);
                dropChooser.Add(ModContent.ItemType<Items.Weapons.VileGlaive>(), 5);
                dropChooser.Add(ModContent.ItemType<Items.Weapons.CursedCarbine>(), 5);
                int choice = dropChooser;
                Item.NewItem(npc.getRect(), choice);
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/InpuratusGore" + i), npc.scale);
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int i = 0; i < npc.oldPos.Length; i++)
            {
                Texture2D texture = Main.npcTexture[npc.type];
                Vector2 trailPosition = npc.oldPos[i] - Main.screenPosition + (npc.Center - npc.position) + new Vector2(0f, Main.NPCAddHeight(npc.whoAmI) + npc.gfxOffY);
                Color color = npc.GetAlpha(drawColor) * ((float)(npc.oldPos.Length - i) / (float)npc.oldPos.Length);
                spriteBatch.Draw(texture, trailPosition, npc.frame, color, npc.rotation, npc.frame.Size() * 0.5f, npc.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void AI()
        {
            if (npc.ai[0] == 0)
            {
                npc.TargetClosest();
            }
            Player player = Main.player[npc.target];
            npc.ai[0]++;
            if (npc.HasValidTarget)
            {
                Vector2 vector = player.Center - npc.Center;
                npc.rotation = vector.ToRotation() - MathHelper.PiOver2;
            }
            if (!player.ZoneCorrupt)
            {
                npc.velocity *= 20;
                npc.damage = 90;
            }
            //Lighting.AddLight(npc.Center, Color.Purple);
            Vector2 vel = player.Center - npc.Center;
            vel.Normalize();
            npc.velocity = vel * 4;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(false);
                P = Main.player[npc.target];
                if (!P.active || P.dead)
                {
                    lmaotheplayerisdead++;

                    if (lmaotheplayerisdead >= 1)
                    {
                        npc.velocity = Vector2.Zero;
                    }
                    if (lmaotheplayerisdead == 120)
                    {
                        Main.PlaySound(SoundID.Roar, npc.position, 0);
                        npc.rotation -= (6 * npc.direction);
                    }
                    if (lmaotheplayerisdead >= 140)
                    {
                        npc.velocity = new Vector2(0f, 20f);
                        npc.rotation -= (6 * npc.direction);
                        Dust dust;
                        Vector2 position = npc.Center;
                        dust = Terraria.Dust.NewDustDirect(position, 152, 215, 219, 0f, -9.736842f, 0, new Color(0, 255, 17), 0.9868422f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(92, Main.LocalPlayer);
                        dust.fadeIn = 0.1973684f;

                    }
                    if (lmaotheplayerisdead == 200)
                    {
                        npc.active = false;
                        Main.NewText("Inpuratus laughs at your failure.");
                    }
                }
            }
            npc.direction = 1;
            float moveSpeed = 3;
            if (npc.Distance(P.MountedCenter) > 120)
            {
                moveSpeed = 3 + (npc.Distance(P.MountedCenter) - 120) / 40;
            }

            if (npc.ai[0] == 30)
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    float speed = 5f;
                    int type = mod.ProjectileType("CorruptedCrystalProjectile");
                    int damage = 25;
                    Main.PlaySound(SoundID.Item, npc.Center, 43);
                    Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);
                }
            }
            if (npc.ai[0] == 40)
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    float speed = 5f;
                    int type = mod.ProjectileType("CorruptedCrystalProjectile");
                    int damage = 25;
                    Main.PlaySound(SoundID.Item, npc.Center, 43);
                    Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);
                }
            }
            if (npc.ai[0] == 50)
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    float speed = 5f;
                    int type = mod.ProjectileType("CorruptedCrystalProjectile");
                    int damage = 25;
                    Main.PlaySound(SoundID.Item, npc.Center, 43);
                    Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);
                }
            }
            if (npc.ai[0] == 60)
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    float speed = 5f;
                    int type = mod.ProjectileType("CorruptedCrystalProjectile");
                    int damage = 25;
                    Main.PlaySound(SoundID.Item, npc.Center, 43);
                    Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);
                }
            }
            if (npc.ai[0] == 70)
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    float speed = 5f;
                    int type = mod.ProjectileType("CorruptedCrystalProjectile");
                    int damage = 25;
                    Main.PlaySound(SoundID.Item, npc.Center, 43);
                    Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);
                }
            }
            if (npc.ai[0] == 90)
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    float speed = 5f;
                    int type = mod.ProjectileType("CorruptedCrystalProjectile");
                    int damage = 25;
                    Main.PlaySound(SoundID.Item, npc.Center, 43);
                    Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);
                }
            }
            if (npc.ai[0] == 120)
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    float speed = 5f;
                    int type = mod.ProjectileType("CorruptedCrystalProjectile");
                    int damage = 25;
                    Main.PlaySound(SoundID.Item, npc.Center, 43);
                    Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);
                }
            }
            if (npc.ai[0] == 150)
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    float speed = 5f;
                    int type = mod.ProjectileType("CorruptedCrystalProjectile");
                    int damage = 25;
                    Main.PlaySound(SoundID.Item, npc.Center, 43);
                    Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);
                }
            }
            if (npc.ai[0] == 200)
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    float speed = 5f;
                    int type = mod.ProjectileType("CorruptedCrystalProjectile");
                    int damage = 25;
                    Main.PlaySound(SoundID.Item, npc.Center, 43);
                    Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);
                }
            }
            if (npc.ai[0] == 290)
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    Main.PlaySound(SoundID.Item, (int)npc.position.X, (int)npc.position.Y, 2);
                    float rotation = npc.rotation;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, (int)NPCID.DevourerHead);
                        Main.PlaySound(SoundID.NPCKilled, npc.Center, 13);
                    }
                }
            }
            if (npc.ai[0] == 310)
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    float speed = 12f;
                    int damage = 35;
                    int type = mod.ProjectileType("CorruptedCrystalProjectile");
                    Main.PlaySound(SoundID.Item, (int)npc.position.X, (int)npc.position.Y, 2);
                    float rotation = npc.rotation;
                    {
                        for (int i = 0; i < 10; ++i)
                            Projectile.NewProjectile(position, (direction * speed).RotatedBy(MathHelper.Pi), type, damage, 0f, Main.myPlayer);
                        Main.PlaySound(SoundID.Item, npc.Center, 43);

                    }
                }
            }
            if (npc.ai[0] >= 320)
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    float speed = 0f;
                    int damage = 35;
                    int type = ProjectileID.EyeFire;
                    float rotation = npc.rotation;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        npc.velocity += new Vector2(npc.direction * 35f, npc.direction * 35f);
                        //npc.velocity = vel * 30;
                        Main.PlaySound(SoundID.Roar, npc.Center, 3);
                        Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);
                    }
                }
            }
            if (npc.ai[0] >= 350)
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    float rotation = npc.rotation;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        npc.velocity = vel * 4;
                    }
                }
            }
            if (npc.ai[0] >= 360)
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    float speed = 0f;
                    int type = ProjectileID.EyeFire;
                    int damage = 35;
                    float rotation = npc.rotation;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        npc.velocity += new Vector2(npc.direction * 35f, npc.direction * 35f);
                        //npc.velocity = vel * 30;
                        Main.PlaySound(SoundID.Roar, npc.Center, 3);
                        Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);
                    }
                }
            }
            if (npc.ai[0] >= 360)
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = targetPosition - position;
                    direction.Normalize();
                    float rotation = npc.rotation;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        npc.velocity = vel * 4;
                        npc.ai[0] = 0;
                    }
                }
            }
        }
    }
}