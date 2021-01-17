using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BoidSimulation : MonoBehaviour
{

    public ComputeShader random;
    public ComputeShader boid;

    RenderTexture position;
    RenderTexture velocity;

    int length =128;
    int boidKernel;
    public VisualEffect effect;

    [SerializeField] private float cohesionForce;
    [SerializeField] private float separationForce;
    [SerializeField] private float alignmentForce;
    [SerializeField] private float cohesionDistance;
    [SerializeField] private float separationDistance;
    [SerializeField] private float alignmentDistance;
    [SerializeField] private float cohesionAngleCos;
    [SerializeField] private float separationAngleCos;
    [SerializeField] private float alignmentAngleCos;
    [SerializeField] float boundaryForce;
    [SerializeField] float boundaryDistance;
    [SerializeField] float minVelocity;
    [SerializeField] float maxVelocity;


    // Start is called before the first frame update
    void Start()
    {
        position = new RenderTexture(length, length, 0, RenderTextureFormat.ARGBFloat);
        position.enableRandomWrite = true;
        position.Create();

        velocity = new RenderTexture(length, length, 0, RenderTextureFormat.ARGBFloat);
        velocity.enableRandomWrite = true;
        velocity.Create();

        boidKernel = boid.FindKernel("Boid");
        boid.SetTexture(boidKernel, "position", position);
        boid.SetTexture(boidKernel, "velocity", velocity);
        boid.SetInt("length", length);
        boid.SetFloat("boundaryDistance", boundaryDistance);
        boid.SetFloat("boundaryForce", boundaryForce);
        boid.SetFloat("minVelocity", minVelocity);
        boid.SetFloat("maxVelocity", maxVelocity);

        int randomKernel = random.FindKernel("Random");
        random.SetTexture(randomKernel, "positionResult", position);
        random.SetTexture(randomKernel, "velocityResult", velocity);
        random.SetFloat("positionRange", boundaryDistance);
        random.SetFloat("velocityRange", maxVelocity / Mathf.Sqrt(3f));
        random.Dispatch(randomKernel, 16, 16, 1);

        effect.SetTexture("position", position);
        effect.SetTexture("velocity", velocity);
    }

    // Update is called once per frame
    void Update()
    {
        BoidUpdate();
    }

    void BoidUpdate()
    {
        boid.SetFloat("cohesionForce", cohesionForce);
        boid.SetFloat("separationForce", separationForce);
        boid.SetFloat("alignmentForce", alignmentForce);
        boid.SetFloat("cohesionDistance", cohesionDistance);
        boid.SetFloat("separationDistance", separationDistance);
        boid.SetFloat("alignmentDistance", alignmentDistance);
        boid.SetFloat("cohesionAngleCos", cohesionAngleCos);
        boid.SetFloat("separationAngleCos", separationAngleCos);
        boid.SetFloat("alignmentAngleCos", alignmentAngleCos);
        boid.SetFloat("deltaTime", Time.deltaTime);
        boid.Dispatch(boidKernel, 16, 16, 1);
    }
}
