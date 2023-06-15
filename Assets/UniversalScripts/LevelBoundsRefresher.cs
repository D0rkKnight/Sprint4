using UnityEngine;
using MoreMountains.CorgiEngine;

public class LevelBoundsRefresher : MonoBehaviour
{

    private LevelManager levelManager;

    public void Refresh()
    {
        // Check if there is a 2d Box Collider. If not, add one.
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider>();
        }

        // Get the level bounds from the LevelManager
        Bounds levelBounds = levelManager.LevelBounds;

        // Set the box collider to the level bounds
        boxCollider.center = levelBounds.center;
        boxCollider.size = levelBounds.size;
    }

    public void Reset()
    {
        levelManager = FindObjectOfType<LevelManager>();

        Refresh();
    }

}