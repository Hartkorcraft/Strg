using Godot;
using System;
using System.Collections.Generic;
using HartLib;
using static HartLib.Utils;

public class TestMO : SpriteMapObject, ISelectable
{

    private TwoPointLine twoPointLine;// = new TwoPointLine(null);

    public override void _EnterTree()
    {
        base._EnterTree();
        blockingMovement.Add(Map.TileType.Brick_Wall);

        GridPos = new Vector2i(Position / new Vector2(GlobalConst.TILE_SIZE, GlobalConst.TILE_SIZE));
        Setup_TwoPointLine();
    }

    public override void _Ready()
    {
        base._Ready();

        twoPointLine.Visible = false;

        GD.Print(Position);
    }


    public override void _Input(InputEvent inputEvent)
    {
        if (Main.game_Manager.AllowWorldInput && inputEvent.IsActionPressed("Left_Mouse"))
        {
            if (Main.game_Manager.CurrentSelection == this && Main.game_Manager.Mouseover.Count == 0)
            {
                Main.map.DebugTiles.Clear();
                var path = Main.map.PathFinding.FindPath(GridPos, Main.Mouse_Grid_Pos, blockingMovement, true, true);
                if (path != null && path.Count > 0)
                {
                    List<Vector2i> path_grid_positions = new List<Vector2i>();
                    foreach (var pathTile in path)
                    {
                        Main.map.DebugTiles.SetCellv(pathTile.GridPos.Vec2(), (int)Map.TileType.Red_Dot);
                        path_grid_positions.Add(pathTile.GridPos);
                    }
                    MoveOnPath(path_grid_positions);
                }
            }
        }
    }

    void Setup_TwoPointLine()
    {
        Func<Vector2> get_Line_End_Point = () => { return GetLocalMousePosition(); };
        twoPointLine = new TwoPointLine(get_Line_End_Point);
        AddChild(twoPointLine);
    }
    //////////////////* IMOUSEABLE /////////////////////////

    public override void On_Left_Mouse_Click()
    {
        base.On_Left_Mouse_Click();
        Main.game_Manager.Select(this, true);
    }

    //////////////////* ISELECTABLE /////////////////////////
    public bool CanSelect { get; set; } = true;
    public virtual void HandleSelection()
    {

    }
    public virtual void HandleBeingSelected()
    {

    }
    public virtual void HandleBeingUnselected()
    {

    }
}
