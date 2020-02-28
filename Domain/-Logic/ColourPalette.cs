using System.Collections;

namespace Domain
{
    public class ColourPalette
    {
        static readonly string[] colours = new[] { "#CC3542", "#FA7902", "#FAB320", "#2CC6D2", "#0A98CF", "#065280" };

        static Stack colourStack = new Stack(colours);

        public static string GetColourCode()
        {
            if (colourStack.Count <= 0)
            {
                for (var i = 0; i < colours.Length; i++)
                    colourStack.Push(colours[i]);
            }

            return colourStack.Pop().ToString();
        }
    }
}