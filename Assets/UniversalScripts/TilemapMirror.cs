using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class TilemapMirror : MonoBehaviour
{
    public Tilemap tilemap;
    public bool mirrorX = false;
    public bool mirrorY = false;

    public Vector2 mirrorAxis = new Vector2(0, 0);

    TileBase[] tiles;

    private void Reset()
    {
        tilemap = GetComponent<Tilemap>();

        // Copy list of all tiles
        tiles = tilemap.GetTilesBlock(tilemap.cellBounds);

        // Mirror the initial tiles
        // MirrorTiles();
    }

    public void Update()
    {
        // Screw synchronizing

        //     // If any delta appears in the tilemap, mirror the tiles
        //     Tilemap.tilemapTileChanged += (Tilemap tilemap, Tilemap.SyncTile[] syncTiles) =>
        //     {
        //         MirrorTiles();
        //         Debug.Log("Tiles mirrored");

        //     };

        //     // if (tilesChanged)
        //     // {
        //     //     MirrorTiles();
        //     //     Debug.Log("Tiles mirrored");
        //     // }
    }

    public void MirrorTiles()
    {
        // Clear all tiles opposite the axis X or Y
        // L->R and B->T
        if (mirrorX)
            ClearTilesInRegion(tilemap, new Vector3Int((int)mirrorAxis.x + 1, tilemap.cellBounds.yMin, 0), new Vector3Int(tilemap.cellBounds.xMax, tilemap.cellBounds.yMax, 0));

        if (mirrorY)
            ClearTilesInRegion(tilemap, new Vector3Int(tilemap.cellBounds.xMin, (int)mirrorAxis.y + 1, 0), new Vector3Int(tilemap.cellBounds.xMax, tilemap.cellBounds.yMax, 0));

        if (!mirrorX && !mirrorY)
            return;

        // Get the bounds of the original Tilemap
        BoundsInt bounds = tilemap.cellBounds;

        Vector3Int cellMA = Vector3Int.RoundToInt(mirrorAxis);
        Debug.Log(cellMA);

        if (!mirrorX) cellMA.x = 0;
        if (!mirrorY) cellMA.y = 0;

        // Iterate over each cell in the original Tilemap
        int cnt = 0;
        int x, y, z;

        float maxX = mirrorX ? mirrorAxis.x + 1 : bounds.max.x;
        float maxY = mirrorY ? mirrorAxis.y + 1 : bounds.max.y;

        for (x = bounds.min.x; x < maxX; x++)
        {
            for (y = bounds.min.y; y < maxY; y++)
            {
                for (z = bounds.min.z; z < bounds.max.z; z++)
                {
                    cnt++;
                    Vector3Int position = new Vector3Int(x, y, z);

                    // Get the tile from the original Tilemap
                    TileBase originalTile = tilemap.GetTile(position);
                    if (originalTile == null)
                        continue;

                    // Mirror the position if enabled
                    List<Vector3Int> mirroredPositions = new List<Vector3Int>();

                    // // lazy fix
                    if (mirrorX)
                        mirroredPositions.Add(new Vector3Int(cellMA.x * 2 - position.x, position.y, position.z));
                    if (mirrorY)
                        mirroredPositions.Add(new Vector3Int(position.x, cellMA.y * 2 - position.y, position.z));
                    if (mirrorX && mirrorY)
                        mirroredPositions.Add(new Vector3Int(cellMA.x * 2 - position.x, cellMA.y * 2 - position.y, position.z));

                    // Set the mirrored tile in the mirrored Tilemap
                    foreach (Vector3Int mirrored in mirroredPositions)
                    {
                        tilemap.SetTile(mirrored, originalTile);
                    }
                }
            }
        }

        Debug.Log(cnt);
    }

    public void ClearTilesInRegion(Tilemap tilemap, Vector3Int regionBL, Vector3Int regionUR)
    {
        // Calculate the size of the region
        Vector3Int regionSize = regionUR - regionBL + Vector3Int.one;

        // Create an array of null tiles with the size of the region
        TileBase[] emptyTiles = new TileBase[regionSize.x * regionSize.y];

        // Set the tiles in the region to null
        tilemap.SetTilesBlock(new BoundsInt(regionBL, regionSize), emptyTiles);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(TilemapMirror))]
public class TilemapMirrorEditor : Editor
{
    bool tilesChanged = false;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TilemapMirror myScript = (TilemapMirror)target;
        if (GUILayout.Button("Mirror Tiles"))
        {
            myScript.MirrorTiles();
        }
    }

    private void OnSceneGUI()
    {
        // Dunno how else to do this
        Tilemap.tilemapTileChanged -= OnTileChanged;
        Tilemap.tilemapTileChanged += OnTileChanged;


        TilemapMirror myScript = (TilemapMirror)target;

        // Mirror tiles if the object is selected
        if (tilesChanged)
        {
            myScript.MirrorTiles();
            tilesChanged = false;

            // Dirty the scene
            EditorUtility.SetDirty(myScript.gameObject);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(myScript.gameObject.scene);
        }

        // Draw axis for where the mirrorAxis is
        Handles.color = Color.red;

        // Draw the mirror axis
        int dist = 50;
        Handles.DrawLine((Vector3)myScript.mirrorAxis - Vector3.up * dist, (Vector3)myScript.mirrorAxis + Vector3.up * dist);
        Handles.DrawLine((Vector3)myScript.mirrorAxis - Vector3.right * dist, (Vector3)myScript.mirrorAxis + Vector3.right * dist);

        // Position handle for mirror axis
        EditorGUI.BeginChangeCheck();
        Vector3 newMA = Handles.PositionHandle(myScript.mirrorAxis, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            float snapIncrement = 1;
            newMA = Utils.SnapVec3(newMA, snapIncrement);

            Undo.RecordObject(myScript, "Change Look At Target Position");
            Undo.RecordObject(myScript.tilemap, "Change Look At Target Position");

            if ((Vector2)newMA != myScript.mirrorAxis)
                tilesChanged = true;

            myScript.mirrorAxis = newMA;
        }

    }

    // on tile change
    private void OnTileChanged(Tilemap tilemap, Tilemap.SyncTile[] syncTiles)
    {
        TilemapMirror myScript = (TilemapMirror)target;
        if (tilemap == myScript.tilemap)
            tilesChanged = true;
    }

}
#endif
