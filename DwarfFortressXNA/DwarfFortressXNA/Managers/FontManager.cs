using System;
using System.Collections.Generic;
using System.Linq;
using DwarfFortressXNA.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DwarfFortressXNA.Managers
{
    public class FontManager
    {
        /// <summary>
        /// Instance of ColorManager - used for deciphering color combinations or accessing base colors raw.
        /// </summary>
        public ColorManager ColorManager;
        /// <summary>
        /// An array of chars aligned according to the IBM 437 standards.
        /// </summary>
        public char[] Codepage =
        {   
            ' ', '☺', '☻', '♥', '♦', '♣', '♠', '•', '◘', '○', '◙', '♂', '♀', '♪', '♫', '☼',
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
            '≡', '±', '≥', '≤', '⌠', '⌡', '÷', '≈', '°', '∙', '·', '√', 'ⁿ', '²', '■', ' '
        };
        /// <summary>
        /// Width of a character in the current font image.
        /// </summary>
        public int CharSizeX = 8;
        /// <summary>
        ///  Height of a character in the current font image.
        /// </summary>
        public int CharSizeY = 12;
        public FontManager()
        {
            ColorManager = new ColorManager();
        }

        /// <summary>
        /// Internal function - used to set the character sizes on font changes/initialization.
        /// </summary>
        /// <param name="x">Width of character.</param>
        /// <param name="y">Height of character.</param>
        public void SetCharSize(int x, int y)
        {
            CharSizeX = x;
            CharSizeY = y;
        }

        /// <summary>
        /// Draw text in a text-box, along with instructions for closing it. Locks input and pauses game until box is cleared.
        /// </summary>
        /// <param name="toDraw">String to render.</param>
        /// <param name="spriteBatch">SpriteBatch to render on.</param>
        /// <param name="font">Image of font to utilize.</param>
        /// <param name="position">Position of textbox.</param>
        /// <param name="size">Size of textbox.</param>
        /// <param name="colorPair">Pair of colours for text within textbox.</param>
        public void DrawBoxedText(string toDraw, SpriteBatch spriteBatch, Texture2D font, Vector2 position, Vector2 size,
            ColorPair colorPair)
        {
            var buffer =new List<string>();
            for (var i = 0; i < toDraw.Length; i++)
            {
                if (i%(size.X - 2) != 0 || i == 0) continue;
                buffer.Add(toDraw.Substring(0, i));
                toDraw = toDraw.Remove(0, i);

                toDraw = buffer[buffer.Count - 1].Substring(buffer.Last().LastIndexOf(' ')) + toDraw;
                buffer[buffer.Count - 1] = buffer[buffer.Count - 1].Remove(buffer.Last().LastIndexOf(' '));
                if (buffer[buffer.Count - 1][0] == ' ')
                    buffer[buffer.Count - 1] = buffer[buffer.Count - 1].Remove(0, 1);
                i = 0;
            }
            buffer.Add(toDraw);
            if (buffer[buffer.Count - 1][0] == ' ')
                buffer[buffer.Count - 1] = buffer[buffer.Count - 1].Remove(0, 1);
            size.Y = buffer.Count + 3;
            for (var y = position.Y; y < position.Y + size.Y + 1; y++)
            {
                for (var x = position.X; x < position.X + size.X + 1; x++)
                {
                    if (x == position.X && y == position.Y) DrawCharacter('╔', spriteBatch, font, new Vector2(x,y), new ColorPair(ColorManager.DarkGrey, ColorManager.Black));
                    else if (x == position.X + size.X && y == position.Y) DrawCharacter('╗', spriteBatch, font, new Vector2(x, y), new ColorPair(ColorManager.DarkGrey, ColorManager.Black));
                    else if (x == position.X && y == position.Y + size.Y) DrawCharacter('╚', spriteBatch, font, new Vector2(x, y), new ColorPair(ColorManager.DarkGrey, ColorManager.Black));
                    else if (x == position.X + size.X && y == position.Y + size.Y) DrawCharacter('╝', spriteBatch, font, new Vector2(x, y), new ColorPair(ColorManager.DarkGrey, ColorManager.Black));
                    else if(x == position.X || x == position.X + size.X) DrawCharacter('║', spriteBatch, font, new Vector2(x,y), new ColorPair(ColorManager.DarkGrey, ColorManager.Black));
                    else if (y == position.Y || y == position.Y + size.Y) DrawCharacter('═', spriteBatch, font, new Vector2(x, y), new ColorPair(ColorManager.DarkGrey, ColorManager.Black));
                    else DrawCharacter('█', spriteBatch, font, new Vector2(x, y), new ColorPair(ColorManager.Black, ColorManager.Black));
                }
            }
            for (var i = 0; i < buffer.Count; i++)
            {
                DwarfFortress.FontManager.DrawString(buffer[i], spriteBatch, font, new Vector2(position.X+2,position.Y+2+i), colorPair);
            }
            DwarfFortress.FontManager.DrawString("Press", spriteBatch, font, new Vector2(position.X+2,position.Y+size.Y), new ColorPair(ColorManager.White, ColorManager.Black));
            DwarfFortress.FontManager.DrawString(" Enter ", spriteBatch, font, new Vector2(position.X + 7, position.Y + size.Y), new ColorPair(ColorManager.LightGreen, ColorManager.Black));
            DwarfFortress.FontManager.DrawString("to close window", spriteBatch, font, new Vector2(position.X + 14, position.Y + size.Y), new ColorPair(ColorManager.White, ColorManager.Black));
        }

        /// <summary>
        /// Renders a string of characters to the screen at a position in a certain colour.
        /// </summary>
        /// <param name="toDraw">String to render.</param>
        /// <param name="spriteBatch">SpriteBatch to render on.</param>
        /// <param name="font">Image of font to utilize.</param>
        /// <param name="position">Position of string.</param>
        /// <param name="colorPair">Pair of colours for text rendered.</param>
        public void DrawString(string toDraw, SpriteBatch spriteBatch, Texture2D font, Vector2 position, ColorPair colorPair)
        {
            if (toDraw == null) return;
            for (var i = 0; i < toDraw.Length;i++ )
            {
                DrawCharacter(toDraw[i], spriteBatch, font, new Vector2(position.X + i, position.Y), colorPair);
            }
        }
        /// <summary>
        /// Internal function. Used to render an individual character - (hopefully)rarely used outside of the basemost functions for rendering.
        /// </summary>
        /// <param name="toDraw">Character to render.</param>
        /// <param name="spriteBatch">SpriteBatch to render on.</param>
        /// <param name="font">Image of font to utilize.</param>
        /// <param name="position">Position of character.</param>
        /// <param name="colorPair">Pair of colours for the character rendered.</param>
        public void DrawCharacter(char toDraw, SpriteBatch spriteBatch, Texture2D font, Vector2 position, ColorPair colorPair)
        {
            var backSpot = GetPositionFromCharacter('█');
            var characterSpot = GetPositionFromCharacter(toDraw);
            spriteBatch.Draw(font, new Rectangle((int)position.X*CharSizeX, (int)position.Y*CharSizeY, CharSizeX, CharSizeY), new Rectangle((int)backSpot.X * CharSizeX, (int)backSpot.Y * CharSizeY, CharSizeX, CharSizeY), colorPair.Background);
            spriteBatch.Draw(font, new Rectangle((int)position.X * CharSizeX, (int)position.Y * CharSizeY, CharSizeX, CharSizeY), new Rectangle((int)characterSpot.X * CharSizeX, (int)characterSpot.Y * CharSizeY, CharSizeX, CharSizeY), colorPair.Foreground);
        }

        /// <summary>
        /// Extracts a character from a token argument, either in integer form or single-quote delimited form.
        /// </summary>
        /// <param name="token">Token to break down.</param>
        /// <returns></returns>
        public char GetCharFromToken(string token)
        {
            var split = token.Split(new[] {':'});
            var strippedChar = RawFile.StripTokenEnding(split[1]);
            if (!strippedChar.StartsWith("'")) return Codepage[RawFile.GetIntFromToken(strippedChar)];
            strippedChar = strippedChar.Replace("'", "");
            return strippedChar[0];
        }

        /// <summary>
        /// Renders a font test square - goes through each character in the codepage in a requested colour.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to render on.</param>
        /// <param name="font">Image of font to utilize.</param>
        /// <param name="position">Position to render font test square..</param>
        /// <param name="colorPair">Pair of colours for text of the square.</param>
        public void FontTest(SpriteBatch spriteBatch, Texture2D font, Vector2 position, ColorPair colorPair)
        {
            for (int i = 0; i < 256; i++)
            {
                DwarfFortress.FontManager.DrawCharacter(Codepage[i], spriteBatch, font, new Vector2(position.X + i%16, position.Y + (int)Math.Floor(i/16d)), colorPair);
            }
        }

        /// <summary>
        /// Long, horrifyingly long function to determine where in the codepage a certain character is.
        /// USE SPARINGLY: This needs some serious optimization!
        /// </summary>
        /// <param name="character">Character to find.</param>
        /// <returns>Position of the character requested in the IBM 437 codepage.</returns>
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
            //ASCII? Just use that!
            if(character >= 0x20 && character < 0x80)
            {
                rowX = character % 16;
                rowY = (int)Math.Floor((double)(character / 16));
            }
            //Not ASCII? We're boned.
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
