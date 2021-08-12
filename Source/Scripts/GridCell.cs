using Godot;
using System;
using System.Collections.Generic;
using HartLib;
using static HartLib.Utils;

public class GridCell
{
    private Vector2i gridPos;
    public Vector2i GridPos { get { return gridPos; } private set { gridPos = value; } }

    private Map.TileType floorTile;
    public Map.TileType FloorTile
    {
        get { return floorTile; }
        set
        {
            floorTile = value;
            Main.map?.FloorTiles.SetCellv(GridPos.Vec2(), (int)floorTile);
        }
    }

    public GridCell(Vector2i _gridPos, Map.TileType _floorTile = Map.TileType.Grass)
    {
        GridPos = _gridPos;
        FloorTile = _floorTile;
    }
}
