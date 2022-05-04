using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCamara : MonoBehaviour
{
    //public int zoom = 1;
    //public int minCamsize = 1;
    //public int maxCamsize = 10;
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float zoom = 1;
    private float minCamsize = 1;
    private float maxCamsize = 10;

    [SerializeField]
    private SpriteRenderer mapRenderer;

    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    //private float zoom, minCamsize, maxCamsize;

    private Vector3 dragOrigin;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;

        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x /2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x /2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y /2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y /2f;
    }

    // Update is called once per frame
    void Update()
    {
        PanCamera();
        
    }
    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            cam.transform.position = ClampCamera(cam.transform.position + difference);
            //cam.transform.position += difference;
        }
    }

    public void ZoomIn()
    {
        float newSize = cam.orthographicSize - zoom;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamsize, maxCamsize);

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    public void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoom;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamsize, maxCamsize);

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        //float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        //float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        float newX = Mathf.Clamp(targetPosition.x, -4, 4);
        float newY = Mathf.Clamp(targetPosition.y, -2, 2);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
