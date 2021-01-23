using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class LifeGame : MonoBehaviour
{
    public ComputeShader random;
    public ComputeShader life;

    [SerializeField] private int aliveMin;
    [SerializeField] private int aliveMax;
    [SerializeField] private int birthMin;
    [SerializeField] private int birthMax;

    RenderTexture field;
    int length = 16;
    int size;

    int kernel;
    public VisualEffect effect;

    float t = 0;
    [SerializeField] private float cycle = 0.5f;

    void Start()
    {
        size = length / 4;

        field = new RenderTexture(length, length, 0, RenderTextureFormat.RFloat);
        field.dimension = TextureDimension.Tex3D;
        field.volumeDepth = length;
        field.enableRandomWrite = true;
        field.Create();

        kernel = life.FindKernel("Life");
        life.SetTexture(kernel, "field", field);
        life.SetInt("aliveMin", aliveMin);
        life.SetInt("aliveMax", aliveMax);
        life.SetInt("birthMin", birthMin);
        life.SetInt("birthMax", birthMax);
        life.SetInt("length", length);


        int randomKernel = random.FindKernel("Random");
        random.SetTexture(randomKernel, "field", field);
        Vector4 seed = new Vector4(Random.Range(1f, 10f), Random.Range(1f, 10f), Random.Range(1f, 10f), Random.Range(1f, 10f));
        random.SetVector("seed", seed);
        random.SetInt("posX",4);
        random.SetInt("posY",4);
        random.SetInt("posZ",4);
        random.Dispatch(randomKernel, 2, 2, 2);

        effect.SetInt("length", length);
        effect.SetTexture("field", field);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > cycle)
        {
            t = 0;
            life.Dispatch(kernel, size, size, size);
        }
    }
}
