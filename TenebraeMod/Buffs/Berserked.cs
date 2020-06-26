using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Buffs
{
	public class Berserked : ModBuff {
		public override void SetDefaults() {
			DisplayName.SetDefault("Berserked");
			Description.SetDefault("Your anger clouds your judgement");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex) {
            player.allDamageMult *= 0.8f;
        }
	}
}