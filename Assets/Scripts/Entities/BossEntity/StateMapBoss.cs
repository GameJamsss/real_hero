using System.Collections.Generic;
using Assets.Scripts.Entities.PlayerEntity;
using Assets.Scripts.StateMachine;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Entities.BossEntity
{
    public static class StateMapBoss
    {
        private static readonly GenericMoveModifier gmm = new GenericMoveModifier();

        public static State<Boss> Idle = new State<Boss>("Idle", (ulong)StatePriorityBoss.Idle)
            .SetEnterCondition(boss => boss.isIdle)
            .SetOnStateEnter(boss => {
                boss.Animation.Play("Idle");
                boss.isHaunt = false;
                boss.CurrentIdleTimer = 0f;
                boss.IdleTimer = Random.Range(boss.MinIdleTime, boss.MaxIdleTime);
                Debug.Log("STATE Idle");
            })
            .SetStateLogic(boss =>
            {
                boss.CurrentIdleTimer += Time.deltaTime;
                if (boss.IdleTimer <= boss.CurrentIdleTimer)
                {
                    if (boss.CurrentStateAtackCount < boss.StateAtackCount){
                        if (Random.Range(0, 2) == 0)
                        {
                            boss.isDefaultAttack=true;
                        }
                        else if (Random.Range(0, 2) == 1)
                        {
                            boss.isNailShots=true;
                        }
                        else
                        {
                            boss.isCarThrow=true;
                        }
                    
                        boss.CurrentStateAtackCount++;
                    
                    }
                    else{
                        boss.isStrongAttacks=true;
                        boss.CurrentStateAtackCount=0;
                    }

                    boss.isIdle = false;
                }
            });

        public static State<Boss> DefaultAttack =
            new State<Boss>("DefaultAttack", (ulong) StatePriorityBoss.DefaultAttack)
                .SetEnterCondition(boss => boss.isDefaultAttack)
                .SetOnStateEnter(boss => {
                    Debug.Log("STATE DefaultAttack");
                    boss.isHaunt = true;
                    boss.CurrentDefaultAttackTimer = 0f;
                    boss.defaultAttacCount = 0;
                })
                .SetStateLogic(boss => {
                    if (!boss.isDefaultAttackPlay){
                        boss.CurrentDefaultAttackTimer += Time.deltaTime;
                        if (boss.DefaultAttackTimer <= boss.CurrentDefaultAttackTimer){
                            switch (boss.defaultAttacCount){
                                case 0:
                                    boss.DefaultAttack();
                                    boss.CurrentDefaultAttackTimer = 0f;
                                    boss.defaultAttacCount++;
                                    break;
                                case 1:
                                    boss.DefaultRightAttack();
                                    boss.CurrentDefaultAttackTimer = 0f;
                                    boss.defaultAttacCount++;
                                    break;
                                case 2:
                                    boss.DefaultLeftRightAttack();
                                    boss.CurrentDefaultAttackTimer = 0f;
                                    boss.defaultAttacCount++;
                                    break;
                                default:
                                    boss.ExitDefaultAttack();
                                    break;
                            }
                        }
 
                    }
                });

        public static State<Boss> NailShots = new State<Boss>("NailShots", (ulong) StatePriorityBoss.NailShots)
            .SetEnterCondition(boss => boss.isNailShots)
            .SetOnStateEnter(boss => {
                Debug.Log("STATE NailShots");
                boss.isHaunt = false;
                boss.MakeMarkerNailShots();
                boss.CurrentNailShotsTimer = 0f;
            })
            .SetStateLogic(boss => {
                boss.CurrentNailShotsTimer += Time.deltaTime;
                if (boss.NailShotsTimer <= boss.CurrentNailShotsTimer&&!boss.isNailShotsPlay)
                {
                    boss.Atencion();
                }
            });

        public static State<Boss> CarThrow = new State<Boss>("CarThrow", (ulong) StatePriorityBoss.CarThrow)
            .SetEnterCondition(boss => boss.isCarThrow)
            .SetOnStateEnter(boss => {
                Debug.Log("STATE CarThrow");
                boss.CarThrow();
                boss.isHaunt = false;
                boss.CurrentCarThrowTimer = 0f;
            })
            .SetStateLogic(boss => {
                if (!boss.isCarThrowPlay){
                    boss.CurrentCarThrowTimer += Time.deltaTime;
                    if (boss.CarThrowTimer <= boss.CurrentCarThrowTimer && !boss.isCarThrowPlay){
                        switch (boss.carThrowCount){
                            case 1:
                                boss.SpawnCar();
                                boss.CurrentCarThrowTimer = 0f;
                                boss.carThrowCount--;
                                boss.ExitCarThrow();
                                break;
                            case 2:
                                boss.SpawnCar();
                                boss.CurrentCarThrowTimer = 0f;
                                boss.carThrowCount--;
                                break;
                            default:
                                boss.ExitCarThrow();
                                break;
                        }

                    }
                }
            });

        public static State<Boss> StrongAttacks = new State<Boss>("StrongAttacks", (ulong)StatePriorityBoss.StrongAttacks)
            .SetEnterCondition(boss => boss.isStrongAttacks)
            .SetOnStateEnter(boss =>
            {
                Debug.Log("STATE StrongAttacks");
                boss.isHaunt = true;
                boss.CurrentStrongAttacksTimer = 0f;
                boss.defaultAttacCount = 0;
            })
            .SetStateLogic(boss => {
                if (!boss.isDefaultAttackPlay){
                    boss.CurrentStrongAttacksTimer += Time.deltaTime;
                    if (boss.StrongAttacksTimer <= boss.CurrentStrongAttacksTimer){
                        switch (boss.defaultAttacCount){
                            case 0:
                                boss.DefaultAttack();
                                boss.CurrentStrongAttacksTimer = 0f;
                                boss.defaultAttacCount++;
                                break;
                            case 1:
                                boss.DefaultRightAttack();
                                boss.CurrentStrongAttacksTimer = 0f;
                                boss.defaultAttacCount++;
                                break;
                            case 2:
                                boss.DefaultLeftRightStrongAttack();
                                boss.CurrentStrongAttacksTimer = 0f;
                                break;
                            default:
                                boss.ExitStrongAttack();
                                break;
                        }
                    }
 
                }
            });
        public static State<Boss> Vulnerable = new State<Boss>("Vulnerable", (ulong)StatePriorityBoss.Vulnerable)
            .SetEnterCondition(boss => boss.isVulnerable)
            .SetOnStateEnter(boss =>
            {
                Debug.Log("STATE Vulnerable");
                boss.CurrentVulnerableTimer = 0f;
                boss.Vulnerable();
            })
            .SetStateLogic(boss => {
                    boss.CurrentVulnerableTimer += Time.deltaTime;
                    if (boss.VulnerableTimer <= boss.CurrentVulnerableTimer){
                        boss.ExitVulnerable();
                }
            });
    }
}