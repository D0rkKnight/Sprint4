using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class TilemapMirror : MonoBehaviour
{
    public Tilemap tilemap;
    public bool mirrorX = false;
    public bool mirrorY = false;

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
        ClearTilesInRegion(tilemap, new Vector3Int(0, tilemap.cellBounds.yMin, 0), new Vector3Int(tilemap.cellBounds.xMax, tilemap.cellBounds.yMax, 0));
        ClearTilesInRegion(tilemap, new Vector3Int(tilemap.cellBounds.xMin, 0, 0), new Vector3Int(tilemap.cellBounds.xMax, tilemap.cellBounds.yMax, 0));

        if (!mirrorX && !mirrorY)
            return;

        // Get the bounds of the original Tilemap
        BoundsInt bounds = tilemap.cellBounds;

        // Iterate over each cell in the original Tilemap
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            // Get the tile from the original Tilemap
            TileBase originalTile = tilemap.GetTile(position);

            // Mirror the position if enabled
            Vector3Int mirroredPosition = position;
            List<Vector3Int> mirroredPositions = new List<Vector3Int>();

            // // lazy fix
            if (mirrorX)
                mirroredPositions.Add(new Vector3Int(-position.x, position.y, position.z));
            if (mirrorY)
                mirroredPositions.Add(new Vector3Int(position.x, -position.y, position.z));
            if (mirrorX && mirrorY)
                mirroredPositions.Add(new Vector3Int(-position.x, -position.y, position.z));

            // Set the mirrored tile in the mirrored Tilemap
            foreach (Vector3Int mirrored in mirroredPositions)
            {
                tilemap.SetTile(mirrored, originalTile);
            }
        }
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
