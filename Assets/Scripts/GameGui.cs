using UnityEngine;

public static class GameGui
{
    static Font s_font;

    public static void Apply()
    {
        if (s_font == null)
        {
            s_font = Font.CreateDynamicFontFromOSFont(new[]
            {
                "PingFang SC",
                "Heiti SC",
                "STHeiti",
                "Hiragino Sans GB",
                "Songti SC",
                "Arial Unicode MS"
            }, 16);
        }

        if (s_font != null)
        {
            GUI.skin.font = s_font;
        }
    }
}
