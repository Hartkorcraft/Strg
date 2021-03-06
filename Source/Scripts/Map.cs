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
    private GridCell[,] gridMap;
    public PathFinding<Map.TileType> PathFinding { get; private set; }
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
                var floorTileType = (TileType)FloorTiles.GetCellv(new Vector2(x, y));
                var midTileType = (TileType)MidTiles.GetCellv(new Vector2(x, y));
                if (floorTileType is TileType.Empty) { floorTileType = TileType.Grass; }
                gridMap[x, y] = new GridCell(new Vector2i(x, y), floorTileType, midTileType);
            }
        }
    }

    public GridCell GetGridCell(Vector2i gridPos)
    {
        if (CheckIfInRange(gridPos, MapSize)) { return gridMap[gridPos.x, gridPos.y]; }
        return null;
    }

    public TileType GetTileType(Vector2i gridPos, TileMap tileMap)
    {
        return (TileType)tileMap.GetCellv(gridPos.Vec2());
    }

    public TileType GetFloorTileType(Vector2i gridPos)
    {
        if (CheckIfInRange(gridPos, MapSize.Vec2i()))
        {
            return gridMap[gridPos.x, gridPos.y].FloorTile;
        }
        return TileType.Empty;
    }

    public void SetTileFloor(Vector2i gridPos, TileType type)
    {
        if (CheckIfInRange(gridPos, MapSize.Vec2i()))
        {
            gridMap[gridPos.x, gridPos.y].FloorTile = type;
        }
    }
    public TileType GetMidTileType(Vector2i gridPos)
    {
        if (CheckIfInRange(gridPos, MapSize.Vec2i()))
        {
            return gridMap[gridPos.x, gridPos.y].MidTile;
        }
        return TileType.Empty;
    }

    public void SetTileMid(Vector2i gridPos, TileType type)
    {
        if (CheckIfInRange(gridPos, MapSize.Vec2i()))
        {
            gridMap[gridPos.x, gridPos.y].MidTile = type;
        }
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

        Func<Vector2i, HashSet<TileType>, bool> checkForBlocking = (pos, blocking) => { return blocking.Contains((TileType)_MidTiles.GetCellv(pos.Vec2())); };
        PathFinding = new PathFinding<TileType>(new Vector2i(_mapSize), checkForBlocking);
    }
    public enum TileType
    {
        Empty = -1,
        Grass = 0,
        Rock = 1,
        Wood = 2,
        Brick_Wall = 5,
        Red_Dot = 6
    }
}