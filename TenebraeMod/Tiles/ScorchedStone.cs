using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TenebraeMod.Tiles
{
	public class ScorchedStone : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			dustType = 1;
			drop = mod.ItemType("ScorchedStone");
			AddMapEntry(new Color(82, 77, 78));
			// Set other values here
		}
	}
}