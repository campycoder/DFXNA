using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DwarfFortressXNA
{

    public struct ColorPair
    {
        public Color foreground;
        public Color background;
        public ColorPair(Color fore, Color back)
        {
            this.foreground = fore;
            this.background = back;
        }
    }
    public class ColorManager
    {
        public static Color black;
        public static Color blue;
        public static Color green;
        public static Color cyan;
        public static Color red;
        public static Color magenta;
        public static Color brown;
        public static Color light_grey;
        public static Color dark_grey;
        public static Color light_blue;
        public static Color light_green;
        public static Color light_cyan;
        public static Color light_red;
        public static Color light_magenta;
        public static Color yellow;
        public static Color white;

        public static List<Color> colorList;

        public ColorManager()
        {
            colorList = new List<Color>();
            colorList.Add(new Color(0,0,0));
            colorList.Add(new Color(0,0,128));
            colorList.Add(new Color(0,128,0));
            colorList.Add(new Color(0,128,128));
            colorList.Add(new Color(128,0,0));
            colorList.Add(new Color(128,0,128));
            colorList.Add(new Color(128,128,0));
            colorList.Add(new Color(192, 192, 192));
            colorList.Add(new Color(128, 128, 128));
            colorList.Add(new Color(0,0,255));
            colorList.Add(new Color(0,255,0));
            colorList.Add(new Color(0,255,255));
            colorList.Add(new Color(255,0,0));
            colorList.Add(new Color(255,0,255));
            colorList.Add(new Color(255,255,0));
            colorList.Add(new Color(255,255,255));
        }

        public ColorPair GetPairFromTriad(int fore, int back, int foreb)
        {
            return new ColorPair(colorList[fore + (foreb * 8)], colorList[back]);
        }
    }
}
