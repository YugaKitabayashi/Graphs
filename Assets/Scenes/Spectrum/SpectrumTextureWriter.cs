using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(AudioSource), typeof(VisualEffect))]
public class SpectrumTextureWriter : MonoBehaviour
{
    private AudioSource audioSource;
    private VisualEffect visualEffect;

    [SerializeField] private int length = 1024;
    private float[] waveSamples;
    private float[] spectrumSamples;

    Color[] outputColors;
    private Texture2D outputMap;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        visualEffect = GetComponent<VisualEffect>();

        waveSamples = new float[length];
        spectrumSamples = new float[length];
        outputColors = new Color[length];

        outputMap = new Texture2D(length, 1, TextureFormat.RGFloat, false);
        outputMap.filterMode = FilterMode.Bilinear;
        outputMap.wrapMode = TextureWrapMode.Clamp;

        visualEffect.SetTexture("OutputMap", outputMap);

        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.GetOutputData(waveSamples, 0);
        audioSource.GetSpectrumData(spectrumSamples, 0, FFTWindow.Hamming);

        for(int i=0;i<length; i++)
        {
            outputColors[i].r = waveSamples[i];
            outputColors[i].g = spectrumSamples[i];
        }
        outputMap.SetPixels(outputColors);
        outputMap.Apply();
    }
}
