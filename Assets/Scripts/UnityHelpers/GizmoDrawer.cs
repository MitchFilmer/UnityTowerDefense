using UnityEngine;
using System.Collections;

public class GizmoDrawer : MonoBehaviour
{
    public Color myColor;
    public enum GizmoShape
    {
        WireSphere = 0,
        Sphere = 1,
        Cube,
        WireCube,
    }

    public enum EGizmoColor
    {
        Blue,
        Cyan,
        Green,
        Magenta,
        Red,
        White,
        Yellow,
    }

    public bool showGizmo;
    public GizmoShape shape;
    public EGizmoColor gizmoColour;
    public float radius;
    public Vector3 scale;

    private void OnDrawGizmos()
    {
        if (!showGizmo)
        {
            return;
        }

        switch (gizmoColour)
        {
            case EGizmoColor.Blue:
                Gizmos.color = Color.blue;
                break;
            case EGizmoColor.Cyan:
                Gizmos.color = Color.cyan;
                break;
            case EGizmoColor.Green:
                Gizmos.color = Color.green;
                break;
            case EGizmoColor.Magenta:
                Gizmos.color = Color.magenta;
                break;
            case EGizmoColor.Red:
                Gizmos.color = Color.red;
                break;
            case EGizmoColor.White:
                Gizmos.color = Color.white;
                break;
            case EGizmoColor.Yellow:
            default:
                Gizmos.color = Color.yellow;
                break;
        }

        switch (shape)
        {
            case GizmoShape.WireSphere:
                Gizmos.DrawWireSphere(transform.localPosition, radius);
                break;
            case GizmoShape.Sphere:
                Gizmos.DrawSphere(transform.localPosition, radius);
                break;
            case GizmoShape.Cube:
                Gizmos.DrawCube(transform.localPosition, scale);
                break;
            case GizmoShape.WireCube:
                Gizmos.DrawWireCube(transform.localPosition, scale);
                break;
            default:
                break;
        }
    }
}