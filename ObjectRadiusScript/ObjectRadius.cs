using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class ObjectRadius : MonoBehaviour
{
    [Range(0.0f, 10f)] [SerializeField] private float objectRadius;
    [SerializeField] private Color gizmoColor = Color.red;
    [SerializeField] private GizmoType gizmoType = GizmoType.WireSphere;
    [SerializeField] private GameObject customGizmoMesh;
    [SerializeField] private bool onlySelected = false;

    private bool radiusWarning = false;

    private void OnDrawGizmos()
    {
        if (objectRadius <= 0.1f)
            if (!radiusWarning)
            {
                Debug.LogWarning("The Objects Radius Has Been Set To Less Than 0.1f, Is This Correct?");
                radiusWarning = true;
            }

        if (SelectionDrawGizmo())
        {
            Gizmos.color = gizmoColor;
            switch (gizmoType)
            {
                case GizmoType.WireSphere:
                    Gizmos.DrawWireSphere(transform.position, objectRadius);
                    break;
                case GizmoType.SolidSphere:
                    Gizmos.DrawSphere(transform.position, objectRadius);
                    break;
                case GizmoType.CustomMesh:
                    if (customGizmoMesh != null)
                        Gizmos.DrawMesh(customGizmoMesh.GetComponent<MeshFilter>().sharedMesh, transform.position,
                            Quaternion.identity, Vector3.one * objectRadius);

                    break;
            }
        }
    }

    private bool SelectionDrawGizmo()
    {
        if (!onlySelected)
            return true;
        else if (Selection.activeGameObject == gameObject) return true;

        return false;
    }

    private enum GizmoType
    {
        WireSphere,
        SolidSphere,
        CustomMesh
    }
}
#endif
