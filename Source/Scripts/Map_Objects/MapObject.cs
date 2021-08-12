using Godot;
using System;
using System.Collections.Generic;
using HartLib;
using static HartLib.Utils;
//using static GlobalConstants;

public abstract class MapObject : Node2D, IHealth, ISelectable, INameable
{

    public string ObjectName { get; protected set; } = "";// = "Default_name";
    protected Vector2i gridPos;
    public virtual Vector2i GridPos
    {
        get { return gridPos; }
        set
        {
            Position = value.Vec2() * new Vector2(GlobalConst.TILE_SIZE, GlobalConst.TILE_SIZE);
            gridPos = value;
        }
    }

    public int Health { get; set; } = 1;
    public virtual void Damage(uint dmg)
    {
        if (Health - (int)dmg <= 0)
        {
            Kill();
        }
        else
        {
            Health -= (int)dmg;
        }
        GD.Print("Damaged: ", this.ToString(), " ", dmg, " New health: ", Health);
    }
    public virtual void Heal(uint health)
    {
        Health += (int)health;
        GD.Print("Healed: ", this.ToString(), " ", health, " New health: ", Health);
    }
    public virtual void Kill()
    {
        GD.Print("Destroyed: ", this.ToString());
        QueueFree();
    }

    // protected HashSet<Map.TileTypes> blockingTiles = new HashSet<Map.TileTypes> { Map.TileTypes.StoneBlock };
    // protected Func<Vector2i, HashSet<Map.TileTypes>, bool> checkForBlocking = (pos, blocking) =>
    // { return (blocking.Contains((Map.TileTypes)Main.map.GetFloorTileType(pos)) || blocking.Contains((Map.TileTypes)Main.map.GetMidTileType(pos))) && Main.map.GetDarknessTileType(pos) != Map.TileTypes.FullDarkness; };

    public override void _EnterTree()
    {

    }

    public override void _Ready()
    {

    }

    public virtual void Turn()
    {

    }

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
