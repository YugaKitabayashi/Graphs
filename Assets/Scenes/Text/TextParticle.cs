using System.Collections;
using UnityEngine.TextCore;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;
using TMPro;

public class TextParticle: MonoBehaviour
{
    public TMP_FontAsset font;
    public VisualEffect effect;
    public string text;

    private Texture2D rectMap;

    // Start is called before the first frame update
    void Start()
    {
        Convert();
    }

    void Convert()
    {
        int length = text.Length;
        Color[] colors = new Color[length];

        rectMap = new Texture2D(length, 1, TextureFormat.RGBAFloat, false);
        rectMap.filterMode = FilterMode.Point;
        rectMap.wrapMode = TextureWrapMode.Repeat;

        effect.SetTexture("rectMap",rectMap);
        effect.SetInt("length", length);

        float w = font.atlasWidth, h = font.atlasHeight;

        for (int i = 0; i < length; i++)
        {
            char c = text[i];
            uint id = font.characterLookupTable[c].glyphIndex;
            GlyphRect glyph = font.glyphLookupTable[id].glyphRect;

            colors[i].r = glyph.x / w;
            colors[i].g = glyph.y / h;
            colors[i].b = glyph.width / w;
            colors[i].a = glyph.height / h;
        }

        rectMap.SetPixels(colors);
        rectMap.Apply();
    }
}
