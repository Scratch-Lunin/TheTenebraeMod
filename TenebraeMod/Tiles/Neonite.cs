using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TenebraeMod.Tiles
{
	public class Neonite : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			TileID.Sets.Ore[Type] = true;
			Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
			Main.tileValue[Type] = 410; // Metal Detector value, see https://terraria.gamepedia.com/Metal_Detector
			Main.tileShine2[Type] = true; // Modifies the draw color slightly.
			Main.tileShine[Type] = 975; // How often tiny dust appear off this tile. Larger is less frequently

			soundType = SoundID.Tink;
			soundStyle = 1;
			dustType = 1;
			minPick = 200;
			mineResist = 5f;

			drop = mod.ItemType("NeoniteShard");
			AddMapEntry(new Color(237, 37, 10));
			// Set other values here
		}
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0.3f;
			g = 0.1f;
			b = 0.05f;
		}
	}
}