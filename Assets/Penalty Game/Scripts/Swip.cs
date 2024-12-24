using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swip : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float lineDrawSpeed = 10f;
    private List<Vector3> points = new List<Vector3>();
    private bool isDrawing = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            ContinueDrawing(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
        }
    }

    private void StartDrawing(Vector3 startPosition)
    {
        points.Clear();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(startPosition);
        points.Add(worldPosition);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, worldPosition);
        isDrawing = true;
    }

    private void ContinueDrawing(Vector3 newPosition)
    {
        if (!isDrawing) return;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(newPosition);
        float distance = Vector3.Distance(points[points.Count - 1], worldPosition);

        if (distance >= 0.1f)
        {
            points.Add(worldPosition);
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }
    }

    private void StopDrawing()
    {
        isDrawing = false;
    }
}
