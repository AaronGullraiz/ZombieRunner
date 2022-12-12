using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public const float PistolFireRate = 1;
    public const float SMGFireRate = 0.1f;
}

public enum AnimationState
{
    IDLE = 0,
    RUN = 1,
    DEATH = 2,
    ATTACK = 3,
}