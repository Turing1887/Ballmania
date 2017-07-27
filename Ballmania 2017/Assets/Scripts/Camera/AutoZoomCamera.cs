using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoZoomCamera : MonoBehaviour {

    [SerializeField]
    float boundingBoxPadding = 2f;

    [SerializeField]
    float minimumSize = 8f;

    [SerializeField]
    float zoomSpeed = 20f;

    Camera camera;

    Vector2 boundingBoxCenter;
    float maxWidth;
    float maxHeight;

    private GameObject[] player;

    void Awake()
    {
        camera = GetComponent<Camera>();
        camera.orthographic = false;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
    }

    void LateUpdate()
    {
        Rect boundingBox = CalculateTargetsBoundingBox();
        Vector3 newPos = CalculateCameraPosition(boundingBox);
        newPos.y = CalculateZSize(boundingBox);
        newPos.z -= newPos.y / 2;
        transform.position = newPos;
    }

    // Max und Min Werte von X und Y der Player bestimmen
    Rect CalculateTargetsBoundingBox()
    {
        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;
        float minZ = Mathf.Infinity;
        float maxZ = Mathf.NegativeInfinity;

        foreach (GameObject target in player)
        {
            Vector3 position = target.transform.position;

            minX = Mathf.Min(minX, position.x);
            minZ = Mathf.Min(minZ, position.z);
            maxX = Mathf.Max(maxX, position.x);
            maxZ = Mathf.Max(maxZ, position.z);
        }
        maxWidth = Mathf.Abs((minX - maxX)) + 2*boundingBoxPadding;
        maxHeight = Mathf.Abs((minZ - maxZ)) + 2*boundingBoxPadding;
        // returned ein Rechteck aus den Min und Max Werten
        return Rect.MinMaxRect(minX - boundingBoxPadding, maxZ + boundingBoxPadding, maxX + boundingBoxPadding, minZ - boundingBoxPadding);
    }
    // Max und Min Ende

    Vector3 CalculateCameraPosition(Rect boundingBox)
    {
        boundingBoxCenter = boundingBox.center;

        return new Vector3(boundingBoxCenter.x, 0, boundingBoxCenter.y);
    }

    float CalculateZSize(Rect boundingBox)
    {
        float zoom;
        if(maxWidth >= maxHeight)
        {
            zoom = (float) ((maxWidth * 0.5) / Mathf.Tan(Mathf.PI/6));
            zoom = Mathf.Round(zoom);
        }
        else
        {
            zoom = (float) ((maxHeight * 0.5) / Mathf.Tan(Mathf.PI / 6));
            zoom = Mathf.Round(zoom);
        }

        if(zoom < minimumSize)
        {
            zoom = minimumSize;
        }

        return Mathf.Clamp(Mathf.Lerp(camera.transform.position.y, zoom, Time.deltaTime * zoomSpeed), minimumSize, Mathf.Infinity);
    }
}
