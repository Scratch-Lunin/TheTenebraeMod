using System.Linq;
using Microsoft.Xna.Framework;
using TenebraeMod.Items.Misc;
using TenebraeMod.Items;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace TenebraeMod.NPCs
{
    [AutoloadBossHead]
    public class Inpuratus : ModNPC
    {
        public int Ohyeahphase2;
        public int PlayerKilled;
        public int Phase2entrance;
        public int stop;
        public int misc;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inpuratus");
            Main.npcFrameCount[npc.type] = 20;
            NPCID.Sets.TrailCacheLength[npc.type] = 5;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetDefaults()
        {
            music = MusicID.Boss3;
            musicPriority = MusicPriority.BossLow;
            npc.width = 118;
            npc.height = 208;
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
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.frameCounter >= 8)
            {
                npc.frame.Y += frameHeight;
                npc.frameCounter = 0;
                if (npc.frame.Y >= Main.npcFrameCount[npc.type] * frameHeight)
                {
                    npc.frame.Y = 0;
                }
            }
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
                dropChooser.Add(ModContent.ItemType<Items.Weapons.Mage.CursefernoBurst>(), 5);
                dropChooser.Add(ModContent.ItemType<Items.Weapons.Melee.VileGlaive>(), 5);
                dropChooser.Add(ModContent.ItemType<Items.Weapons.Ranger.CursedCarbine>(), 5);
                int choice = dropChooser;
                Item.NewItem(npc.getRect(), choice);
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                TenebraeModWorld.InpuratusDies = true;
                TenebraeModWorld.downedInpuratus = true;
                for (int i = 0; i < 8; i++)
                {
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/InpuratusGore" + i), npc.scale);
                }
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            var GlowMask = mod.GetTexture("NPCs/Inpuratus_glowmask");
            var Effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(GlowMask, npc.Center - Main.screenPosition + new Vector2(-1 , npc.gfxOffY), npc.frame, Color.White, npc.rotation, npc.frame.Size() / 2, npc.scale, Effects, 0);
        }

        public override void AI()
        {
            #region Misc
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
            /*if (!player.ZoneCorrupt)
            {
                npc.velocity *= 20;
                npc.damage = 90;
            }*/
            //Lighting.AddLight(npc.Center, Color.Purple);
            if (npc.ai[0] < 320)
            {
                Vector2 vel = player.Center - npc.Center;
                vel.Normalize();
                npc.velocity = vel * 4;
            }
            #endregion
            #region PlayerDeath
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(false);
                P = Main.player[npc.target];
                if (!P.active || P.dead)
                {
                    PlayerKilled++;

                    if (PlayerKilled >= 1)
                    {
                        npc.velocity = Vector2.Zero;
                    }
                    if (PlayerKilled == 100)
                    {
                        Main.PlaySound(SoundID.Roar, npc.Center, 0);
                        npc.rotation -= (6 * npc.direction);
                    }
                    if (PlayerKilled >= 140)
                    {
                        npc.velocity = new Vector2(0f, 20f);
                        npc.rotation -= (6 * npc.direction);
                        Dust dust;
                        Vector2 position = npc.Center;
                        dust = Terraria.Dust.NewDustDirect(position, 152, 215, 219, 0f, -9.736842f, 0, new Color(0, 255, 17), 0.9868422f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(92, Main.LocalPlayer);
                        dust.fadeIn = 0.1973684f;

                    }
                    if (PlayerKilled == 200)
                    {
                        npc.active = false;
                        Main.NewText("Inpuratus laughs at your failure...", 10, 127, 23, false);
                    }
                }
            }
            #endregion
            #region Misc
            npc.direction = 1;
            float moveSpeed = 3;
            if (npc.Distance(P.MountedCenter) > 120)
            {
                moveSpeed = 3 + (npc.Distance(P.MountedCenter) - 120) / 40;
            }
            #endregion
            #region Phase1
            float[] shootTimes = new float[] { 30f, 40f, 50f, 60f, 70f, 90f, 120f, 150f, 200f }; // Defining this makes it a little easier to check when to shoot.
            if (shootTimes.Contains(npc.ai[0])) // If the array contains projectile.ai[0], then projectile.ai[0] has to equal one of the values in the array.
            {
                npc.TargetClosest();
                if (npc.HasValidTarget && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 position = npc.Center;
                    Vector2 targetPosition = Main.player[npc.target].Center;
                    Vector2 direction = npc.DirectionTo(player.Center);
                    float speed = 5f;
                    int type = mod.ProjectileType("CorruptedCrystalProjectile");
                    int damage = 25;
                    Main.PlaySound(SoundID.Item, npc.Center, 43);
                    Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);
                }
            }
            if (npc.ai[0] == 290) //summon minions
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
                        int minionCount = Main.rand.Next(2, 5);
                        for (int i = 0; i < minionCount; i++)
                        {
                            NPC.NewNPC((int)npc.position.X + Main.rand.Next(0, 201), (int)npc.position.Y + Main.rand.Next(0, 201), ModContent.NPCType<VileLeechHead>(), 0, npc.whoAmI, 0);
                        }
                        Main.PlaySound(SoundID.NPCHit, npc.Center, 1);
                    }
                }
            }

            if (npc.ai[0] >= 320) // Changes to >= so all this code runs.
            {
                npc.TargetClosest();

                if (npc.HasValidTarget && npc.ai[0] == 320) // Only dash and roar once.
                {
                    Main.PlaySound(SoundID.Roar, npc.Center, 0);
                    npc.velocity = npc.DirectionTo(Main.player[npc.target].Center) * 35f;
                    TenebraeModWorld.DashShake = true;
                }

                if ((npc.ai[0] - 320) % 5 == 0 && Main.netMode != NetmodeID.MultiplayerClient) // Spawn fire every 5 frames.
                {
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.EyeFire, 35, 0f, Main.myPlayer);
                }

                if (npc.ai[0] >= 340) // Slow down 20 ticks after the dash
                {
                    npc.velocity *= 0.85f;
                }

                if (npc.ai[0] == 400) // Reset the ai
                {
                    npc.ai[0] = 0;
                }
                #endregion
                #region Phase2
                if (npc.life <= 9000)
                {
                    stop++;
                    misc++;
                    Ohyeahphase2++;
                    if (misc == 1)
                    {
                        Phase2entrance = 2;
                    }
                    if (stop >= 1)
                    {
                        //npc.velocity = Vector2.Zero;
                    }

                    if (Phase2entrance == 2)
                    {
                        int amount = 16;

                        TenebraeModWorld.InpuratusDies = true; //maybe
                        for (int i = 0; i < amount; ++i)
                        {
                            float currentRotation = (MathHelper.TwoPi / amount) * i;
                            Vector2 projectileVelocity = currentRotation.ToRotationVector2() * 4;

                            float speed = 40f;
                            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                            int damage = 35;
                            int type = mod.ProjectileType("CorruptedCrystalProjectile");
                            Projectile.NewProjectile(npc.Center, projectileVelocity, type, damage, 0, Main.myPlayer);
                            Phase2entrance = 3;
                        }
                        if (Phase2entrance == 3)
                        {
                            int amount2 = 12;

                            TenebraeModWorld.InpuratusDies = true; //maybe
                            for (int i = 0; i < amount2; ++i)
                            {
                                float currentRotation = (MathHelper.TwoPi / amount2) * i;
                                Vector2 projectileVelocity = currentRotation.ToRotationVector2() * 6;

                                float speed = 40f;
                                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                                int damage = 35;
                                int type = mod.ProjectileType("CorruptedCrystalProjectile");
                                Projectile.NewProjectile(npc.Center, projectileVelocity, type, damage, 0, Main.myPlayer);
                                Phase2entrance = 0;
                            }
                        }
                    }
                }


                #endregion
            }
        }
    }
}