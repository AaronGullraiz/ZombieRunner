using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public const float PistolFireRate = 1;
    public const float SMGFireRate = 0.1f;


    public const int PistolBulets = 10;
    public const int SMGBulets = 25;
}

public enum AnimationState
{
    IDLE = 0,
    RUN = 1,
    DEATH = 2,
    ATTACK = 3,
    RunWithGun = 4,
}

public enum WeaponType
{
    PISTOL,
    SMG
}