using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DwarfFortressXNA.Objects
{
    public class Tile
    {
        public int CurrentTemperature = 10043; // Approx. 75 deg F
        public State CurrentState = State.SOLID;
        public Material Material;
        public Tile(Material material)
        {
            
        }

        public string GetNameBasedOnState(bool plural)
        {
            return plural ? Material.StateList[CurrentState].Plural : Material.StateList[CurrentState].Name;
        }

        public void UpdateTile(GameTime gameTime)
        {
            
        }

        public void RenderTile(SpriteBatch spriteBatch, Texture2D font, Vector2 whereToRender)
        {
            DwarfFortress.FontManager.DrawCharacter(Material.Tile, spriteBatch, font, whereToRender, Material.DisplayColor);
        }
    }
}
