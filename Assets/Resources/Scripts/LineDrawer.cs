using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineDrawer : MonoBehaviour
{
    [SerializeField] float lineThickness = 0.25f;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = lineThickness;
        lineRenderer.endWidth = lineThickness;
    }

    public void Draw(Transform pointA, Transform pointB)
    {
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, pointA.position);
        lineRenderer.SetPosition(1, pointB.position);
        StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        float fadeStep = 0.0075f;

        // loop until the lines are invisible
        while (lineRenderer.startColor.a > 0)
        {
            yield return new WaitForEndOfFrame();
            Color newColor = new Color(
                    lineRenderer.startColor.r,
                    lineRenderer.startColor.g,
                    lineRenderer.startColor.b,
                    lineRenderer.startColor.a - fadeStep);

            lineRenderer.startColor = newColor;
            lineRenderer.endColor = newColor;
        }
    }
}
