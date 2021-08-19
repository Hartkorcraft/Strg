using System.Collections.Generic;
using Godot;
using HartLib;
using static HartLib.Utils;
public interface IMoveable
{
    public HashSet<Map.TileType> blockingMovement { get; }
    //public void MoveTo(Vector2 endPos, float smooth);
    public void MoveToGridPos(Vector2i endPos);
}
