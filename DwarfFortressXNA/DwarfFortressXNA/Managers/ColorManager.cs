using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace DwarfFortressXNA.Managers
{

    public struct ColorPair
    {
        public Color Foreground;
        public Color Background;
        public ColorPair(Color fore, Color back)
        {
            Foreground = fore;
            Background = back;
        }
    }
    public class ColorManager
    {
        public static Color Black;
        public static Color Blue;
        public static Color Green;
        public static Color Cyan;
        public static Color Red;
        public static Color Magenta;
        public static Color Brown;
        public static Color LightGrey;
        public static Color DarkGrey;
        public static Color LightBlue;
        public static Color LightGreen;
        public static Color LightCyan;
        public static Color LightRed;
        public static Color LightMagenta;
        public static Color Yellow;
        public static Color White;

        public static List<Color> ColorList;

        public ColorManager()
        {
            Black =new Color(0, 0, 0);
            Blue = new Color(0, 0, 128);
            Green = new Color(0, 128, 0);
            Cyan = new Color(0, 128, 128);
            Red = new Color(128, 0, 0);
            Magenta = new Color(128, 0, 128);
            Brown = new Color(128, 128, 0);
            LightGrey = new Color(192, 192, 192);
            DarkGrey = new Color(128, 128, 128);
            LightBlue = new Color(0, 0, 255);
            LightGreen = new Color(0, 255, 0);
            LightCyan = new Color(0, 255, 255);
            LightRed = new Color(255, 0, 0);
            LightMagenta = new Color(255, 0, 255);
            Yellow = new Color(255, 255, 0);
            White = new Color(255, 255, 255);
            ColorList = new List<Color>
            {
               Black,
               Blue,
               Green,
               Cyan,
               Red,
               Magenta,
               Brown,
               LightGrey,
               DarkGrey,
               LightBlue,
               LightGreen,
               LightCyan,
               LightRed,
               LightMagenta,
               Yellow,
               White
            };
        }

        public ColorPair GetPairFromTriad(int fore, int back, int foreb)
        {
            return new ColorPair(ColorList[fore + (foreb * 8)], ColorList[back]);
        }
    }
}
