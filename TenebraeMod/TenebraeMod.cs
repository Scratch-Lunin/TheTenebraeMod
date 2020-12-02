using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TenebraeMod
{
	public partial class TenebraeMod : Mod
	{
		public TenebraeMod()
		{
		}
		
		public override void AddRecipeGroups()
		{
			RecipeGroup goldbar = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Gold Bar", new int[]
			{
				ItemID.GoldBar,
				ItemID.PlatinumBar
			});
			RecipeGroup.RegisterGroup("TenebraeMod:GoldBar", goldbar);
			RecipeGroup demonbrick = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Demonite Brick", new int[]
			{
				ItemID.DemoniteBrick,
				ItemID.CrimtaneBrick
			});
			RecipeGroup.RegisterGroup("TenebraeMod:DemoniteBrick", demonbrick);
			RecipeGroup cobaltbar = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Cobalt Bar", new int[]
			{
				ItemID.CobaltBar,
				ItemID.PalladiumBar
			});
			RecipeGroup.RegisterGroup("TenebraeMod:CobaltBar", cobaltbar);
		}
	}
}