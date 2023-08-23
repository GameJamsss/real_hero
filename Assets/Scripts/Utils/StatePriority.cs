namespace Assets.Scripts.Utils
{
    public enum StatePriority : ulong
    {
        Idle = 1,
        Move = 2,
        Fall = 3,
        MoveUp = 4,
        AirJump = 5,
        Jump = 6,
        Attack1 = 7,
        Attack2 = 8,
        Attack3 = 9,
        Damage = 10,
        Dash = 11,
    }
    public enum StatePriorityBoss : ulong
    {
        Idle = 1,
        DefaultAttack = 2,
        NailShots = 3,
        CarThrow = 4,
        StrongAttacks = 5,
        Vulnerable = 6,
    }
}
