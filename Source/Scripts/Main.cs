using Godot;
using System;
using System.Collections.Generic;
using HartLib;
using static HartLib.Utils;

public class Main : Node
{

    public static Map map { get; private set; }
    public static Debug_Manager debug_Manager { get; private set; }
    public static Game_Manager game_Manager { get; private set; }

    [Export] Vector2 initMapSize = new Vector2(5, 5);
    public Vector2i InitMapSize
    {
        get { return new Vector2i(initMapSize); }
        set { initMapSize.x = value.x; initMapSize.y = value.y; }
    }

    public static Vector2i Mouse_Grid_Pos { get { return MouseToCart(map.FloorTiles.GetGlobalMousePosition(), map.FloorTiles); } }
    public static Vector2i Last_Clicked_Grid_Pos { get; private set; } = new Vector2i(-1, -1);


    public override void _EnterTree()
    {
        var Tiles = GetNode("Tiles");

        debug_Manager = (Debug_Manager)GetNode("DebugUI").GetNode("Debug_Manager");
        game_Manager = (Game_Manager)GetNode("Game_Manager");

        map = new Map(
        new Vector2ui(InitMapSize),
        (TileMap)Tiles.GetNode("Floor_Tiles"),
        (TileMap)Tiles.GetNode("Mid_Tiles"),
        (TileMap)Tiles.GetNode("Debug_Tiles")
        );



        var testMapObject = (TestMO)GetNode("Tiles/TestMapObject");
        testMapObject.GridPos = new Vector2i(4, 2);
    }

    public override void _Ready()
    {
        map.InitGridMap(map.MapSize, false);

        //Main
        debug_Manager.AddLog(new DebugInfo("Mouse_and_Last_Click_Pos"));
        debug_Manager.AddLog(new DebugInfo("Tiles_under_mouse"));

        //SpriteMapObject
        debug_Manager.AddLog(new DebugInfo("Map_Object_under_mouse"));
    }

    public override void _Process(float delta)
    {
        debug_Manager.UpdateLog("Mouse_and_Last_Click_Pos", Mouse_Grid_Pos.ToString() + " | " + Last_Clicked_Grid_Pos.ToString());
        debug_Manager.UpdateLog("Tiles_under_mouse",
        "Floor: " + map.GetTileType(Mouse_Grid_Pos, map.FloorTiles).ToString() + " | " +
        "Mid: " + map.GetTileType(Mouse_Grid_Pos, map.MidTiles).ToString());
    }
    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseButton)
        {
            if (inputEvent.IsPressed())
            {
                if (inputEvent.IsActionPressed("Left_Mouse"))
                {
                    On_Left_Mouse_Click();
                }
                else if (inputEvent.IsActionPressed("Right_Mouse"))
                {
                    On_Right_Mouse_Click();
                }
            }
        }
    }

    public virtual void On_Left_Mouse_Click()
    {
        Last_Clicked_Grid_Pos = Mouse_Grid_Pos;
    }
    public virtual void On_Right_Mouse_Click()
    {

    }
}
