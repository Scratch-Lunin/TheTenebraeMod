using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TenebraeMod.Tiles
{
	public class ScorchedDirt : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			dustType = 22;
			drop = mod.ItemType("ScorchedDirt");
			AddMapEntry(new Color(96, 89, 87));
			// Set other values here
		}
	}
}