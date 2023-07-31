namespace Assets.Scripts.Utils
{
    public enum StatePriority : ulong
    {
        Idle = 1,
        Move = 2,
        Fall = 3,
        MoveUp = 4,
        Jump = 5,
        Attack1 = 6,
        Attack2 = 7,
        Attack3 = 8,
        Damage = 9,
        Dash = 10,
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
