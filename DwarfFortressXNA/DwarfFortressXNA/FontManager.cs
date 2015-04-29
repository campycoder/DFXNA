using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DwarfFortressXNA
{
    public class FontManager
    {
        public ColorManager DfColor;
        public char[] Codepage =
        {' ', '☺', '☻', '♥', '♦', '♣', '♠', '•', '◘', '○', '◙', '♂', '♀', '♪', '♫', '☼',
            '►', '◄', '↕', '‼', '¶', '§', '▬', '↨', '↑', '↓', '→', '←', '∟', '↔', '▲', '▼',
            ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',','-', '.', '/',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?',
            '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
            'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '\\', ']','^', '_',
            '`', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
            'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '{', '|', '}', '~', '⌂',
            'Ç', 'ü', 'é', 'â', 'ä', 'à', 'å', 'ç', 'ê', 'ë', 'è', 'ï', 'î', 'ì', 'Ä', 'Å',
            'É', 'æ', 'Æ', 'ô', 'ö', 'ò', 'û', 'ù', 'ÿ', 'Ö', 'Ü', '¢', '£', '¥', '₧', 'ƒ',
            'á', 'í', 'ó', 'ú', 'ñ', 'Ñ', 'ª', 'º', '¿', '⌐', '¬', '½', '¼', '¡', '«', '»',
            '░', '▒', '▓', '│', '┤', '╡', '╢', '╖', '╕', '╣', '║', '╗', '╝', '╜', '╛', '┐',
            '└', '┴', '┬', '├', '─', '┼', '╞', '╟', '╚', '╔', '╩', '╦', '╠', '═', '╬', '╧',
            '╨', '╤', '╥', '╙', '╘', '╒', '╓', '╫', '╪', '┘', '┌', '█', '▄', '▌', '▐', '▀',
            'α', 'ß', 'Γ', 'π', 'Σ', 'σ', 'µ', 'τ', 'Φ', 'Θ', 'Ω', 'δ', '∞', 'φ', 'ε', '∩',
            '≡', '±', '≥', '≤', '⌠', '⌡', '÷', '≈', '°', '∙', '·', '√', 'ⁿ', '²', '■', ' '};
        public FontManager()
        {
            DfColor = new ColorManager();
        }

        public void DrawString(string toDraw, SpriteBatch spriteBatch, Texture2D font, Vector2 position, ColorPair colorPair)
        {
            for (var i = 0; i < toDraw.Length;i++ )
            {
                DrawCharacter(toDraw[i], spriteBatch, font, new Vector2(position.X + i, position.Y), colorPair);
            }
        }

        public void DrawCharacter(char toDraw, SpriteBatch spriteBatch, Texture2D font, Vector2 position, ColorPair colorPair)
        {
            var backSpot = GetPositionFromCharacter('█');
            var characterSpot = GetPositionFromCharacter(toDraw);
            spriteBatch.Draw(font, new Rectangle((int)position.X*8, (int)position.Y*12, 8, 12), new Rectangle((int)backSpot.X * 8, (int)backSpot.Y * 12, 8, 12), colorPair.Background);
            spriteBatch.Draw(font, new Rectangle((int)position.X*8, (int)position.Y*12, 8, 12), new Rectangle((int)characterSpot.X * 8, (int)characterSpot.Y * 12, 8, 12), colorPair.Foreground);
        }

        public Vector2 GetPositionFromCharacter(char character)
        {
            var rowX = 0;
            var rowY = 0;
            /*for(int i = 0; i < 256;i++)
            {
                if(character == codepage[i])
                {
                   rowX = i % 16;
                   rowY = (int)Math.Floor((double)i / 16);
                }
            }*/
            if(character >= 0x20 && character < 0x80)
            {
                rowX = character % 16;
                rowY = (int)Math.Floor((double)(character / 16));
            }
            else
            {
                //Ugly switch statement for those things unsolveable by convential (i.e. midnight) math
                switch (character)
                {
                    case '■':
                        rowX = 14;
                        rowY = 15;
                        break;
                    case '²':
                        rowX = 13;
                        rowY = 15;
                        break;
                    case 'ⁿ':
                        rowX = 12;
                        rowY = 15;
                        break;
                    case '√':
                        rowX = 11;
                        rowY = 15;
                        break;
                    case '·':
                        rowX = 10;
                        rowY = 15;
                        break;
                    case '∙':
                        rowX = 9;
                        rowY = 15;
                        break;
                    case '°':
                        rowX = 8;
                        rowY = 15;
                        break;
                    case '≈':
                        rowX = 7;
                        rowY = 15;
                        break;
                    case '÷':
                        rowX = 6;
                        rowY = 15;
                        break;
                    case '⌡':
                        rowX = 5;
                        rowY = 15;
                        break;
                    case '⌠':
                        rowX = 4;
                        rowY = 15;
                        break;
                    case '≤':
                        rowX = 3;
                        rowY = 15;
                        break;
                    case '≥':
                        rowX = 2;
                        rowY = 15;
                        break;
                    case '±':
                        rowX = 1;
                        rowY = 15;
                        break;
                    case '≡':
                        rowX = 0;
                        rowY = 15;
                        break;
                    case '∩':
                        rowX = 15;
                        rowY = 14;
                        break;
                    case 'ε':
                        rowX = 14;
                        rowY = 14;
                        break;
                    case 'φ':
                        rowX = 13;
                        rowY = 14;
                        break;
                    case '∞':
                        rowX = 12;
                        rowY = 14;
                        break;
                    case 'δ':
                        rowX = 11;
                        rowY = 14;
                        break;
                    case 'Ω':
                        rowX = 10;
                        rowY = 14;
                        break;
                    case 'Θ':
                        rowX = 9;
                        rowY = 14;
                        break;
                    case 'Φ':
                        rowX = 8;
                        rowY = 14;
                        break;
                    case 'τ':
                        rowX = 7;
                        rowY = 14;
                        break;
                    case 'µ':
                        rowX = 6;
                        rowY = 14;
                        break;
                    case 'σ':
                        rowX = 5;
                        rowY = 14;
                        break;
                    case 'Σ':
                        rowX = 4;
                        rowY = 14;
                        break;
                    case 'π':
                        rowX = 3;
                        rowY = 14;
                        break;
                    case 'Γ':
                        rowX = 2;
                        rowY = 14;
                        break;
                    case 'ß':
                        rowX = 1;
                        rowY = 14;
                        break;
                    case 'α':
                        rowX = 0;
                        rowY = 14;
                        break;
                    case '▀':
                        rowX = 15;
                        rowY = 13;
                        break;
                    case '▐':
                        rowX = 14;
                        rowY = 13;
                        break;
                    case '▌':
                        rowX = 13;
                        rowY = 13;
                        break;
                    case '▄':
                        rowX = 12;
                        rowY = 13;
                        break;
                    case '█':
                        rowX = 11;
                        rowY = 13;
                        break;
                    case '┌':
                        rowX = 10;
                        rowY = 13;
                        break;
                    case '┘':
                        rowX = 9;
                        rowY = 13;
                        break;
                    case '╪':
                        rowX = 8;
                        rowY = 13;
                        break;
                    case '╫':
                        rowX = 7;
                        rowY = 13;
                        break;
                    case '╓':
                        rowX = 6;
                        rowY = 13;
                        break;
                    case '╒':
                        rowX = 5;
                        rowY = 13;
                        break;
                    case '╘':
                        rowX = 4;
                        rowY = 13;
                        break;
                    case '╙':
                        rowX = 3;
                        rowY = 13;
                        break;
                    case '╥':
                        rowX = 2;
                        rowY = 13;
                        break;
                    case '╤':
                        rowX = 1;
                        rowY = 13;
                        break;
                    case '╨':
                        rowX = 0;
                        rowY = 13;
                        break;
                    case '╧':
                        rowX = 15;
                        rowY = 12;
                        break;
                    case '╬':
                        rowX = 14;
                        rowY = 12;
                        break;
                    case '═':
                        rowX = 13;
                        rowY = 12;
                        break;
                    case '╠':
                        rowX = 12;
                        rowY = 12;
                        break;
                    case '╦':
                        rowX = 11;
                        rowY = 12;
                        break;
                    case '╩':
                        rowX = 10;
                        rowY = 12;
                        break;
                    case '╔':
                        rowX = 9;
                        rowY = 12;
                        break;
                    case '╚':
                        rowX = 8;
                        rowY = 12;
                        break;
                    case '╟':
                        rowX = 7;
                        rowY = 12;
                        break;
                    case '╞':
                        rowX = 6;
                        rowY = 12;
                        break;
                    case '┼':
                        rowX = 5;
                        rowY = 12;
                        break;
                    case '─':
                        rowX = 4;
                        rowY = 12;
                        break;
                    case '├':
                        rowX = 3;
                        rowY = 12;
                        break;
                    case '┬':
                        rowX = 2;
                        rowY = 12;
                        break;
                    case '┴':
                        rowX = 1;
                        rowY = 12;
                        break;
                    case '└':
                        rowX = 0;
                        rowY = 12;
                        break;
                    case '┐':
                        rowX = 15;
                        rowY = 11;
                        break;
                    case '╛':
                        rowX = 14;
                        rowY = 11;
                        break;
                    case '╜':
                        rowX = 13;
                        rowY = 11;
                        break;
                    case '╝':
                        rowX = 12;
                        rowY = 11;
                        break;
                    case '╗':
                        rowX = 11;
                        rowY = 11;
                        break;
                    case '║':
                        rowX = 10;
                        rowY = 11;
                        break;
                    case '╣':
                        rowX = 9;
                        rowY = 11;
                        break;
                    case '╕':
                        rowX = 8;
                        rowY = 11;
                        break;
                    case '╖':
                        rowX = 7;
                        rowY = 11;
                        break;
                    case '╢':
                        rowX = 6;
                        rowY = 11;
                        break;
                    case '╡':
                        rowX = 5;
                        rowY = 11;
                        break;
                    case '┤':
                        rowX = 4;
                        rowY = 11;
                        break;
                    case '│':
                        rowX = 3;
                        rowY = 11;
                        break;
                    case '▓':
                        rowX = 2;
                        rowY = 11;
                        break;
                    case '▒':
                        rowX = 1;
                        rowY = 11;
                        break;
                    case '░':
                        rowX = 0;
                        rowY = 11;
                        break;
                    case '»':
                        rowX = 15;
                        rowY = 10;
                        break;
                    case '«':
                        rowX = 14;
                        rowY = 10;
                        break;
                    case '¡':
                        rowX = 13;
                        rowY = 10;
                        break;
                    case '¼':
                        rowX = 12;
                        rowY = 10;
                        break;
                    case '½':
                        rowX = 11;
                        rowY = 10;
                        break;
                    case '¬':
                        rowX = 10;
                        rowY = 10;
                        break;
                    case '⌐':
                        rowX = 9;
                        rowY = 10;
                        break;
                    case '¿':
                        rowX = 8;
                        rowY = 10;
                        break;
                    case 'º':
                        rowX = 7;
                        rowY = 10;
                        break;
                    case 'ª':
                        rowX = 6;
                        rowY = 10;
                        break;
                    case 'Ñ':
                        rowX = 5;
                        rowY = 10;
                        break;
                    case 'ñ':
                        rowX = 4;
                        rowY = 10;
                        break;
                    case 'ú':
                        rowX = 3;
                        rowY = 10;
                        break;
                    case 'ó':
                        rowX = 2;
                        rowY = 10;
                        break;
                    case 'í':
                        rowX = 1;
                        rowY = 10;
                        break;
                    case 'á':
                        rowX = 0;
                        rowY = 10;
                        break;
                    case 'ƒ':
                        rowX = 15;
                        rowY = 9;
                        break;
                    case '₧':
                        rowX = 14;
                        rowY = 9;
                        break;
                    case '¥':
                        rowX = 13;
                        rowY = 9;
                        break;
                    case '£':
                        rowX = 12;
                        rowY = 9;
                        break;
                    case '¢':
                        rowX = 11;
                        rowY = 9;
                        break;
                    case 'Ü':
                        rowX = 10;
                        rowY = 9;
                        break;
                    case 'Ö':
                        rowX = 9;
                        rowY = 9;
                        break;
                    case 'ÿ':
                        rowX = 8;
                        rowY = 9;
                        break;
                    case 'ù':
                        rowX = 7;
                        rowY = 9;
                        break;
                    case 'û':
                        rowX = 6;
                        rowY = 9;
                        break;
                    case 'ò':
                        rowX = 5;
                        rowY = 9;
                        break;
                    case 'ö':
                        rowX = 4;
                        rowY = 9;
                        break;
                    case 'ô':
                        rowX = 3;
                        rowY = 9;
                        break;
                    case 'Æ':
                        rowX = 2;
                        rowY = 9;
                        break;
                    case 'æ':
                        rowX = 1;
                        rowY = 9;
                        break;
                    case 'É':
                        rowX = 0;
                        rowY = 9;
                        break;
                    case 'Å':
                        rowX = 15;
                        rowY = 8;
                        break;
                    case 'Ä':
                        rowX = 14;
                        rowY = 8;
                        break;
                    case 'ì':
                        rowX = 13;
                        rowY = 8;
                        break;
                    case 'î':
                        rowX = 12;
                        rowY = 8;
                        break;
                    case 'ï':
                        rowX = 11;
                        rowY = 8;
                        break;
                    case 'è':
                        rowX = 10;
                        rowY = 8;
                        break;
                    case 'ë':
                        rowX = 9;
                        rowY = 8;
                        break;
                    case 'ê':
                        rowX = 8;
                        rowY = 8;
                        break;
                    case 'ç':
                        rowX = 7;
                        rowY = 8;
                        break;
                    case 'å':
                        rowX = 6;
                        rowY = 8;
                        break;
                    case 'à':
                        rowX = 5;
                        rowY = 8;
                        break;
                    case 'ä':
                        rowX = 4;
                        rowY = 8;
                        break;
                    case 'â':
                        rowX = 3;
                        rowY = 8;
                        break;
                    case 'é':
                        rowX = 2;
                        rowY = 8;
                        break;
                    case 'ü':
                        rowX = 1;
                        rowY = 8;
                        break;
                    case 'Ç':
                        rowX = 0;
                        rowY = 8;
                        break;
                    case '⌂':
                        rowX = 15;
                        rowY = 7;
                        break;
                    case '▼':
                        rowX = 15;
                        rowY = 1;
                        break;
                    case '▲':
                        rowX = 14;
                        rowY = 1;
                        break;
                    case '↔':
                        rowX = 13;
                        rowY = 1;
                        break;
                    case '∟':
                        rowX = 12;
                        rowY = 1;
                        break;
                    case '←':
                        rowX = 11;
                        rowY = 1;
                        break;
                    case '→':
                        rowX = 10;
                        rowY = 1;
                        break;
                    case '↓':
                        rowX = 9;
                        rowY = 1;
                        break;
                    case '↑':
                        rowX = 8;
                        rowY = 1;
                        break;
                    case '↨':
                        rowX = 7;
                        rowY = 1;
                        break;
                    case '▬':
                        rowX = 6;
                        rowY = 1;
                        break;
                    case '§':
                        rowX = 5;
                        rowY = 1;
                        break;
                    case '¶':
                        rowX = 4;
                        rowY = 1;
                        break;
                    case '‼':
                        rowX = 3;
                        rowY = 1;
                        break;
                    case '↕':
                        rowX = 2;
                        rowY = 1;
                        break;
                    case '◄':
                        rowX = 1;
                        rowY = 1;
                        break;
                    case '►':
                        rowX = 0;
                        rowY = 1;
                        break;
                    case '☼':
                        rowX = 15;
                        break;
                    case '♫':
                        rowX = 14;
                        break;
                    case '♪':
                        rowX = 13;
                        break;
                    case '♀':
                        rowX = 12;
                        break;
                    case '♂':
                        rowX = 11;
                        break;
                    case '◙':
                        rowX = 10;
                        break;
                    case '○':
                        rowX = 9;
                        break;
                    case '◘':
                        rowX = 8;
                        break;
                    case '•':
                        rowX = 7;
                        break;
                    case '♠':
                        rowX = 6;
                        break;
                    case '♣':
                        rowX = 5;
                        break;
                    case '♦':
                        rowX = 4;
                        break;
                    case '♥':
                        rowX = 3;
                        break;
                    case '☻':
                        rowX = 2;
                        break;
                    case '☺':
                        rowX = 1;
                        break;
                }
            }

            return new Vector2(rowX, rowY);
        }
    }
}
