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
		public override void Load()
		{
			if (!Main.dedServ)
			{
				Main.itemTexture[ItemID.ShinyRedBalloon] = GetTexture("Resprite/ShinyRedBalloon");
				Main.itemTexture[ItemID.HoneyBalloon] = GetTexture("Resprite/HoneyBalloon");
				Main.itemTexture[ItemID.SandstorminaBalloon] = GetTexture("Resprite/SandstorminaBalloon");
				Main.itemTexture[ItemID.BalloonHorseshoeHoney] = GetTexture("Resprite/AmberHorseshoeBalloon");
				Main.itemTexture[ItemID.BlizzardinaBalloon] = GetTexture("Resprite/BlizzardinaBalloon");
				Main.itemTexture[ItemID.WhiteHorseshoeBalloon] = GetTexture("Resprite/WhiteHorseshoeBalloon");
				Main.itemTexture[ItemID.YellowHorseshoeBalloon] = GetTexture("Resprite/YellowHorseshoeBalloon");
				Main.itemTexture[ItemID.CloudinaBalloon] = GetTexture("Resprite/CloudinaBalloon");
				Main.itemTexture[ItemID.BlueHorseshoeBalloon] = GetTexture("Resprite/BlueHorseshoeBalloon");
				Main.itemTexture[ItemID.FartInABalloon] = GetTexture("Resprite/FartinaBalloon");
				Main.itemTexture[ItemID.BalloonHorseshoeFart] = GetTexture("Resprite/GreenHorseshoeBalloon");
			}
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