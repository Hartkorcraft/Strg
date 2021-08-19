using Godot;
using System;
using System.Collections.Generic;
using HartLib;
using static HartLib.Utils;

public abstract class GameState
{
    protected static Game_Manager game_manager = Main.game_Manager;

    public virtual bool AllowWorldInput { get; set; } = true;

    #region READY UPDATE EXIT 

    public virtual void ReadyState()
    {
        GD.Print("Entered " + GetType().ToString() + " state");
    }

    public virtual void UpdateState()
    {

    }
    public virtual void ExitState()
    {
        //GD.Print("Exited " + GetType().ToString() + " state");
    }
    #endregion

    public virtual void OnMouseOver(IMouseable imouseable)
    {

    }

    #region SELECTING
    public virtual bool CanSelect { get; set; } = true;

    public virtual void Select(ISelectable selection, bool unSelectIfSame = false)
    {
        if (CanSelect is false) { return; }

        if (selection == game_manager.CurrentSelection && unSelectIfSame) { selection = null; }

        game_manager.CurrentSelection?.HandleBeingUnselected();
        game_manager.CurrentSelection = selection;
        game_manager.CurrentSelection?.HandleBeingSelected();
    }
    #endregion


    public GameState()
    {
    }
}
