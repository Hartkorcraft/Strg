using Godot;
using System;
using System.Collections.Generic;
using HartLib;
using static HartLib.Utils;

public class Map
{
    public TileMap FloorTiles { get; private set; }
    public TileMap MidTiles { get; private set; }
    public TileMap DebugTiles { get; private set; }

    public Vector2ui MapSize { get; private set; } = new Vector2ui(10, 10);
    public static Vector2ui TileSize { get; private set; } = new Vector2ui(32, 16);
    private GridCell[,] gridMap;

    // public void DisplayPath(List<PathFinding<TileTypes>.PathFindingCell> path, bool clear = true)
    // {
    //     if (clear) PathFindingTiles.Clear();
    //     if (path is null) return;
    //     foreach (var pathPos in path)
    //     {
    //         PathFindingTiles.SetCellv(pathPos.GridPos.Vec2(), (int)TileTypes.Dot);
    //     }
    // }

    public void InitGridMap(Vector2ui _MapSize, bool clear = true)
    {
        if (clear)
        {
            FloorTiles.Clear();
            MidTiles.Clear();
        }

        gridMap = new GridCell[_MapSize.x, _MapSize.y];

        for (int y = 0; y < _MapSize.y; y++)
        {
            for (int x = 0; x < _MapSize.x; x++)
            {
                gridMap[x, y] = new GridCell(new Vector2i(x, y));
            }
        }
    }

    public GridCell GetGridCell(Vector2i gridPos) { return gridMap[gridPos.x, gridPos.y]; }

    public TileTypes GetFloorTileType(Vector2i gridPos)
    {
        if (CheckIfInRange(gridPos, MapSize.Vec2i()))
        {
            return gridMap[gridPos.x, gridPos.y].FloorTile;
        }
        return TileTypes.Empty;
    }
    public TileTypes GetMidTileType(Vector2i gridPos)
    {
        if (CheckIfInRange(gridPos, MapSize.Vec2i()))
        {
            return gridMap[gridPos.x, gridPos.y].MidTile;
        }
        return TileTypes.Empty;
    }

    public void SetTileFloor(Vector2i gridPos, TileTypes type)
    {
        if (CheckIfInRange(gridPos, MapSize.Vec2i()))
        {
            gridMap[gridPos.x, gridPos.y].FloorTile = type;
        }
    }
    public void SetTileMid(Vector2i gridPos, TileTypes type)
    {
        gridMap[gridPos.x, gridPos.y].MidTile = type;
    }

    public bool OnMap(Vector2i pos)
    {
        return CheckIfInRange(pos, MapSize.Vec2i());
    }

    public Map(Vector2ui _mapSize, TileMap _FloorTiles, TileMap _MidTiles, TileMap _DebugTiles)
    {
        FloorTiles = _FloorTiles;
        MidTiles = _MidTiles;
        DebugTiles = _DebugTiles;
        MapSize = _mapSize;
    }
    public enum TileTypes
    {
        Empty = 0,
        Grass = 1,
        Rock = 2
    }
}