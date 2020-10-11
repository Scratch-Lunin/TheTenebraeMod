using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TenebraeMod.Tiles
{
	public class StarlightBlock : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			dustType = 1;
			drop = mod.ItemType("StarlightBlock");
			AddMapEntry(new Color(240, 213, 34));
			// Set other values here
		}
    }
}