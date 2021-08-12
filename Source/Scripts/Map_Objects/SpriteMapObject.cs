using Godot;
using System;
using System.Collections.Generic;
using HartLib;
using static HartLib.Utils;


public class SpriteMapObject : MapObject
{
    public Sprite sprite { get; set; }


    public override void _EnterTree()
    {
        sprite = (Sprite)GetChild(0);
    }


}
