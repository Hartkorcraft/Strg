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
    // public static GameManager gameManager { get; private set; }


    [Export] int initMapSize_x = 5;
    [Export] int initMapSize_y = 5;
    public Vector2i InitMapSize
    {
        get { return new Vector2i(initMapSize_x, initMapSize_y); }
        set { initMapSize_x = value.x; initMapSize_y = value.y; }
    }

    ///////////////////////////////////////////////////////////////////////////////////////

    public override void _EnterTree()
    {
        var Tiles = GetNode("Tiles");
        debug_Manager = (Debug_Manager)GetNode("DebugUI").GetNode("Debug_Manager");
        //gameManager = new GameManager();


        map = new Map(
        new Vector2ui(InitMapSize),
        (TileMap)Tiles.GetNode("Floor_Tiles"),
        (TileMap)Tiles.GetNode("Mid_Tiles"),
        (TileMap)Tiles.GetNode("Debug_Tiles")
        );
        map.InitGridMap(map.MapSize);
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("Left_Mouse"))
        {
            var mousePosCart = MouseToCart(map.FloorTiles.GetGlobalMousePosition(), map.FloorTiles);

            GD.Print(mousePosCart);
        }
    }
}
