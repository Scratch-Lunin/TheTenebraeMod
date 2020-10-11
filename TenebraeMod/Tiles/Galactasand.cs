using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TenebraeMod.Tiles
{
	public class Galactasand : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			dustType = 22;
			drop = mod.ItemType("Galactasand");
			AddMapEntry(new Color(95, 37, 182));
			// Set other values here
		}
	}
}