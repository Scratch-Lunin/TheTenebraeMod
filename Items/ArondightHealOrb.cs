using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TenebraeMod.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Items {
    public class ArondightHealOrb : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Healing Orb");
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
        }

        public override bool OnPickup(Player player)
        {
            player.HealEffect(20);
            player.statLife += 20;
            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            for (int i = 9; i >= 0; i--)
            {
                spriteBatch.Draw(Main.itemTexture[item.type], item.Center - Main.screenPosition, new Rectangle(0, 0, item.width, item.height), Color.Red * (1 - i / 10f), rotation, new Vector2(item.width / 2, item.height / 2), scale * (1 + i / 10f), SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}