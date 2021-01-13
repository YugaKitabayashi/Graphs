using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EndPointBehavior: MonoBehaviour
{
    [SerializeField] private Vector3 force;
    [SerializeField] private VisualEffect effect;

    private Vector3 oldPosition;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        oldPosition = transform.position;
    }
    private void Update()
    {
        effect.SetVector3("position", transform.position);
        effect.SetVector3("oldPosition", oldPosition);
        oldPosition = transform.position;

    }
}
