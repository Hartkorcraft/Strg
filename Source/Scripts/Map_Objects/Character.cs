using Godot;
using System;
using System.Collections.Generic;
using HartLib;
using static HartLib.Utils;

public abstract class Character : MapObject, ITurn
{
    //protected PathFinding<Map.TileTypes> pathFinding;
    //protected Fov<Map.TileTypes> fov;
    //public uint fovRange { get; set; } = 5;

    public override void Kill()
    {
        //Main.gameManager.DeleteITurnObject(this as ITurn);
        base.Kill();
    }


    public override void _Ready()
    {
        base._Ready();
        //Main.gameManager.AddITurnObject(this);
        //fov = new Fov<Map.TileTypes>(Main.map.MapSize, blockingTiles, checkForBlocking);
        //pathFinding = new PathFinding<Map.TileTypes>(Main.map.MapSize.Vec2i(), blockingTiles, checkForBlocking);

    }

    public override void Turn()
    {

    }


}
