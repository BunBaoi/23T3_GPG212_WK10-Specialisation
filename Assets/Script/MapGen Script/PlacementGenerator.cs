using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlacementGenerator : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    [Header("Raycast Setting")]
    [SerializeField] int density;

    [Space]

    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;
    [SerializeField] Vector2 xRange;
    [SerializeField] Vector2 zRange;

    [Header("Prefab Variation Setting")]
    [SerializeField, Range(0, 1)] float rotateTowardsNormal;
    [SerializeField] Vector2 rotationRange;
    [SerializeField] Vector3 minScale;
    [SerializeField] Vector3 maxScale;

    public void Generate()
    { 
        Clear();

        for (int i = 0; i < density; i++)
        {
            float sampleX = Random.Range(xRange.x, xRange.y);
            float sampleY = Random.Range(zRange.x, zRange.y);
            Vector3 rayStart = new Vector3(sampleX, maxHeight, sampleY);

            if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
                continue;

            if (hit.point.y < minHeight)
                continue;

            GameObject instantieatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(this.prefab, transform);
            instantieatedPrefab.transform.position = hit.point;
            instantieatedPrefab.transform.Rotate(Vector2.up, Random.Range(rotationRange.x, rotationRange.y), Space.Self);
            instantieatedPrefab.transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.FromToRotation(instantieatedPrefab.transform.up, hit.normal), rotateTowardsNormal);
            instantieatedPrefab.transform.localScale = new Vector3(
                Random.Range(minScale.x, maxScale.x),
                Random.Range(minScale.y, maxScale.y),
                Random.Range(minScale.z, maxScale.z));
            Debug.Log(minScale);
        }
    }

    public void Clear()
    {
        while (transform.childCount !=0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
