using Godot;
using System;
using System.Collections.Generic;
using HartLib;
using static HartLib.Utils;

public class Main : Node
{

    ///////////////////////////////////////////////////////////////////////////////////////

    public static Map map { get; private set; }
    public static Debug_Manager debug_Manager { get; private set; }
    public static Game_Manager game_Manager { get; private set; }


    [Export] Vector2 initMapSize = new Vector2(5, 5);
    public Vector2i InitMapSize
    {
        get { return new Vector2i(initMapSize); }
        set { initMapSize.x = value.x; initMapSize.y = value.y; }
    }

    public static Vector2i MouseGridPos { get { return MouseToCart(map.FloorTiles.GetGlobalMousePosition(), map.FloorTiles); } }

    ///////////////////////////////////////////////////////////////////////////////////////

    public override void _EnterTree()
    {
        var Tiles = GetNode("Tiles");
        debug_Manager = (Debug_Manager)GetNode("DebugUI").GetNode("Debug_Manager");
        game_Manager = (Game_Manager)GetNode("Game_Manager");


        map = new Map(
        new Vector2ui(InitMapSize),
        (TileMap)Tiles.GetNode("Floor_Tiles"),
        (TileMap)Tiles.GetNode("Debug_Tiles")
        );

    }

    public override void _Ready()
    {
        map.InitGridMap(map.MapSize, false);

        debug_Manager.AddLog(new DebugInfo("Mouse_pos"));
        debug_Manager.AddLog(new DebugInfo("Tile_under_mouse"));

        var testMapObject = (TestMO)GetNode("Tiles/TestMapObject");

        testMapObject.GridPos = new Vector2i(4, 2);
    }

    public override void _Process(float delta)
    {
        debug_Manager.UpdateLog("Mouse_pos", MouseGridPos.ToString());
        debug_Manager.UpdateLog("Tile_under_mouse", map.GetTileType(MouseGridPos, map.FloorTiles).ToString());// map.GetGridCell(MouseGridPos)?.FloorTile.ToString());

    }

    public override void _Input(InputEvent inputEvent)
    {
        // if (inputEvent.IsActionPressed("Left_Mouse"))
        // {
        //     var mousePosCart = MouseToCart(map.FloorTiles.GetGlobalMousePosition(), map.FloorTiles);

        //     GD.Print(mousePosCart);
        // }
    }
}
