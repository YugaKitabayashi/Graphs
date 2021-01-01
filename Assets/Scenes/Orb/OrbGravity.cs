using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbGravity : MonoBehaviour
{
    [SerializeField] private GameObject[] orbs;
    [SerializeField] private float g;
    [SerializeField] private float r;

    private void Start()
    {
        for(int i = 0; i < orbs.Length; i++)
        {
            orbs[i].transform.position = Random.insideUnitSphere * 2f;
        }
    }

    void Update()
    {
        for (int i = 0; i < orbs.Length; i++)
        {
            Vector3 force = Vector3.zero;
            for (int j = 0; j < orbs.Length; j++)
            {
                if (i != j)
                {
                    Vector3 f = orbs[j].transform.position - orbs[i].transform.position;
                    float d = f.magnitude;
                    force += g / d * (1f - 2f / (1f + Mathf.Exp(d - r))) * f / d;
                }
            }
            orbs[i].GetComponent<Rigidbody>().AddForce(force);
        }
    }
}
