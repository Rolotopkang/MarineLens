using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashFloatingEffect : MonoBehaviour
{
    public float amplitude = 0.2f;
    public float frequency = 0.5f;
    public float rotationSpeed = 10f;

    private Vector3 startPosition;
    private Vector3 noiseOffset;
    private int Derection = 1;

    void Start()
    {
        startPosition = transform.position;
        noiseOffset = new Vector3(
            Random.Range(0f, 100f),
            Random.Range(0f, 100f),
            Random.Range(0f, 100f)
        );
        Derection = Random.Range(0, 2)*2 - 1;

    }

    void Update()
    {
        float offsetX = Mathf.PerlinNoise(Time.time * frequency + noiseOffset.x, noiseOffset.y) - 0.5f;
        float offsetY = Mathf.PerlinNoise(noiseOffset.x, Time.time * frequency + noiseOffset.y) - 0.5f;
        float offsetZ = Mathf.PerlinNoise(Time.time * frequency + noiseOffset.z, noiseOffset.y) - 0.5f;
        
        Vector3 floatOffset = new Vector3(offsetX, offsetY, offsetZ) * amplitude;
        transform.position = startPosition + floatOffset;

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * Derection);
    }
}
