using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace TenebraeMod.Buffs
{
    public class HolyRetribution : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Holy Retribution");
            Description.SetDefault("Increases the the Holy Excalibur's abilities");
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
        }
    }
}
