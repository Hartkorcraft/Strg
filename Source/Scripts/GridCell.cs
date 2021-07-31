using Godot;
using System;
using System.Collections.Generic;
using HartLib;
using static HartLib.Utils;

public class GridCell
{
    private Vector2i gridPos;
    public Vector2i GridPos { get { return gridPos; } private set { gridPos = value; } }

    private Map.TileTypes floorTile;
    public Map.TileTypes FloorTile
    {
        get { return floorTile; }
        set
        {
            floorTile = value;
            Main.map?.FloorTiles.SetCellv(GridPos.Vec2(), (int)floorTile);
        }
    }

    private Map.TileTypes midTile;
    public Map.TileTypes MidTile
    {
        get { return midTile; }
        set
        {
            midTile = value;
            Main.map?.MidTiles.SetCellv(GridPos.Vec2(), (int)midTile);
        }
    }

    public GridCell(Vector2i _gridPos, Map.TileTypes _floorTile = Map.TileTypes.Grass, Map.TileTypes _midTile = Map.TileTypes.Empty)
    {
        GridPos = _gridPos;
        FloorTile = _floorTile;
        MidTile = _midTile;
    }
}
