using Godot;
using System;
using System.Collections.Generic;
using HartLib;
using static HartLib.Utils;


public class Selector : Sprite
{
    [Export]
    public float MoveSpeed { get; set; } = 0.3f;
    public override void _Process(float delta)
    {
        if (this.Visible)

        {
            Position = Lerp(Position, (Main.MouseGridPos.Vec2() * GlobalConst.TILE_SIZE) - new Vector2(1, 1), MoveSpeed);
        }
    }

}
