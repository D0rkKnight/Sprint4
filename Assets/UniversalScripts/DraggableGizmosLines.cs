using UnityEngine;
using UnityEditor;

public class DraggableGizmosLines : MonoBehaviour
{
    public float lineLength = 5f;

    private Vector3 verticalStart;
    private Vector3 verticalEnd;
    private Vector3 horizontalStart;
    private Vector3 horizontalEnd;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(verticalStart, verticalEnd);
        Gizmos.DrawLine(horizontalStart, horizontalEnd);
    }

    private void OnDrawGizmosSelected()
    {
        verticalStart = transform.position + Vector3.up * (lineLength / 2);
        verticalEnd = transform.position - Vector3.up * (lineLength / 2);
        horizontalStart = transform.position + Vector3.right * (lineLength / 2);
        horizontalEnd = transform.position - Vector3.right * (lineLength / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(verticalStart, verticalEnd);
        Gizmos.DrawLine(horizontalStart, horizontalEnd);

        Vector3 newPosition = Handles.PositionHandle(transform.position, Quaternion.identity);
        if (newPosition != transform.position)
        {
            Undo.RecordObject(transform, "Move Gizmos Lines");
            transform.position = newPosition;
        }
    }
}
