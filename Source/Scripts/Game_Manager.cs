using Godot;
using System;
using System.Collections.Generic;
using HartLib;
using static HartLib.Utils;

public class Game_Manager : Node
{

    //////////////////////////////////////////////////////////////////////////

    //public List<ITurn> turnObjects { get; set; } = new List<ITurn>();
    public static bool CanSelect { get; set; } = true;

    // private static ISelectable currentSelection;
    // public static ISelectable CurrentSelection
    // {
    //     get { return currentSelection; }
    //     set
    //     {
    //         if (CanSelect is false) return;
    //         currentSelection?.HandleBeingUnselected();
    //         if (currentSelection == value)
    //         {
    //             currentSelection = null;
    //         }
    //         else
    //         {
    //             currentSelection = value;
    //         }

    //         string name = Helpers.NameAndType(currentSelection as INameable);

    //         GD.Print("Selected: ", name);
    //         Main.debug_Manager.UpdateLog("CurrentSelection", name);
    //     }
    // }

    //////////////////////////////////////////////////////////////////////////

    public override void _Ready()
    {
        Main.debug_Manager.AddLog(new DebugInfo("test", "just testing", true));

        Main.debug_Manager.AddLog(new DebugInfo("test2", "seks", true));

        Main.debug_Manager.UpdateLog("test", "dostales maila");
    }

    public override void _Process(float delta)
    {
        //currentSelection?.HandleBeingSelected();
    }

    // public void AddITurnObject(ITurn iturn)
    // {
    //     if (turnObjects.Contains(iturn) is false)
    //     {
    //         turnObjects.Add(iturn);
    //     }
    //     GD.Print("Added: ", iturn.GetType().Name, " ", iturn.ToString(), " Iturn to Gamemanager");
    // }

    // public bool DeleteITurnObject(ITurn iturn)
    // {
    //     return turnObjects.Remove(iturn);
    // }

    //Main.debug_Manager.AddLog(new DebugInfo("CurrentSelection", Helpers.NameAndType(currentSelection as INameable), true));


}
