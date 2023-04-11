using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public enum DuelStateMode
    {
        DrawState,
        MoveState,
        MainState,
        EndState,
    }
    public enum PlayerStateMode
    {
        DoThingState,
        ReadyState,
        NoDoThingState,
    }
}
