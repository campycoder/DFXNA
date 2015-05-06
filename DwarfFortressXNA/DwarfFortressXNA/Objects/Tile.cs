using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DwarfFortressXNA.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DwarfFortressXNA.Objects
{
    public class Tile
    {
        public int CurrentTemperature = 10043; // Approx. 75 deg F
        public State CurrentState = State.SOLID;
        public Material Material;
        public bool Outside = false;
        public bool Light = false;
        public bool AboveGround = false;
        public bool Revealed = false;

        public Tile(Material material)
        {
            Material = material;
            if (material == null) return;
            if (Material.IntProperties["MAT_FIXED_TEMP"] != 60001)
                CurrentTemperature = Material.IntProperties["MAT_FIXED_TEMP"];
        }

        public string GetNameBasedOnState(bool plural, bool dependsOnReveal)
        {
            if (Material == null) return "Open Space";
            return dependsOnReveal && !Revealed ? "???" : Material.GetNameFromState(CurrentState, plural);
        }

        public void UpdateTile(GameTime gameTime)
        {
            if (!Revealed || Material == null) return;
            if (CurrentState == State.SOLID && 
                Material.IntProperties["MELTING_POINT"] != 60001 &&
                CurrentTemperature >= Material.IntProperties["MELTING_POINT"])
            {
                CurrentState = State.LIQUID;
            }
        }

        public void RenderTile(SpriteBatch spriteBatch, Texture2D font, Vector2 whereToRender)
        {
            if (!Revealed) return;
            DwarfFortress.FontManager.DrawCharacter(Material == null ? '▓' : Material.Tile, spriteBatch, font, whereToRender, Material == null ? new ColorPair(ColorManager.Cyan, ColorManager.Black) : Material.DisplayColor);
        }
    }
}
