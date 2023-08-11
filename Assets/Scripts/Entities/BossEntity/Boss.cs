using System;
using System.Collections;
using Assets.Scripts.Entities.PlayerEntity;
using Assets.Scripts.StateMachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Entities.BossEntity
{
    public class Boss : MonoBehaviour{
        public GameObject pointToSpawnCar;
        public GameObject car;
        public GameObject pointToSpawnNail;
        public GameObject nail;
        public GameObject pointToSpawnNailMarker;
        public GameObject NailMarker;
        public GameObject pointToSpawnEarth;
        public GameObject earth;
        public BossHitBox[] hitbox;
        public event Action<int> damaged; 

        public NailMarker currentNailMarker;
        public bool isIdle;
        public bool isDefaultAttack;
        public bool isNailShots;
        public bool isCarThrow;        
        public bool isVulnerable;
        public bool isStrongAttacks;

        public bool isHaunt;

        public int CurrentStateAtackCount;
        public int StateAtackCount;


        [Header("Idle")]
        public float MinIdleTime = 5f; // Минимальное время в состоянии IdleBoss
        public float MaxIdleTime = 10f; // Максимальное время в состоянии IdleBoss
        public float IdleTimer;
        public float CurrentIdleTimer;

        [Header("CarThrow")]
        public float CarThrowTimer;
        public float CurrentCarThrowTimer;
        public int carThrowCount;
        public int carThrowAnimCount;
        public bool isCarThrowPlay;

        [Header("NailShot")]
        public float NailShotsTimer;
        public float CurrentNailShotsTimer;
        public bool isNailShotsPlay;

        [Header("DefaultAttack")]
        public float DefaultAttackTimer;
        public float CurrentDefaultAttackTimer;
        public int defaultAttacCount;
        public bool isDefaultAttackPlay;
        
        [Header("StrongAttacks")]
        public float StrongAttacksTimer;
        public float CurrentStrongAttacksTimer;
        public int earthCount;

        [Header("Vulnerable")]
        public float VulnerableTimer;
        public float CurrentVulnerableTimer;
        public bool isVulnerablePlay;


        [Header("Other")]
        public Player Player;
        public Transform player;
        public float followSpeed = 5f; // Скорость следования босса за игроком
        public GameObject[] HitBox;

        public StateMachine<Boss> fsm;
        public Animator Animation;

        public int Health;
        public bool SecondStage;
        public float SecondStageValue=2f;
        public SpriteRenderer blink;

        public BoxCollider2D left;
        public BoxCollider2D right;

        public GameObject AtencionIcon;
        public bool death;

        void Start()
        {
            // Initialize boss-specific properties and components here

            fsm = new StateMachine<Boss>(
                new StateManager<Boss>(this)
                    .AddState(StateMapBoss.Idle)
                    .AddState(StateMapBoss.DefaultAttack)
                    .AddState(StateMapBoss.NailShots)
                    .AddState(StateMapBoss.CarThrow)
                    .AddState(StateMapBoss.StrongAttacks)
                    .AddState(StateMapBoss.Vulnerable)
            );

            isIdle = true;
            hitbox[0].damaged += Damage;
            hitbox[1].damaged += Damage;

        }

        void Update()
        {
            if (!death && !Player.death){
                fsm.Run();
            }
            //Debug.Log(fsm.CurrentState.Name);
            if (isHaunt){
                MoveToPlayer();
            }
        }

        void MoveToPlayer(){
            float newX = Mathf.Lerp( transform.position.x, player.position.x, Time.deltaTime * followSpeed);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            if (transform.position.x - player.position.x > 0.2f){
                Animation.Play("Move_left");
            }
            else if(player.position.x  - transform.position.x > 0.2f){
                Animation.Play("Move_right");
            }
            else{
                Animation.Play("Idle");
            }
        }

        public void ChangeStateByHealth(){
            SecondStage = true;
            MinIdleTime = MinIdleTime / SecondStageValue;
            MaxIdleTime = MaxIdleTime / SecondStageValue;
            CarThrowTimer=CarThrowTimer/SecondStageValue;
            NailShotsTimer = NailShotsTimer / SecondStageValue;
            DefaultAttackTimer=DefaultAttackTimer / SecondStageValue;
            StrongAttacksTimer = StrongAttacksTimer / SecondStageValue;
            VulnerableTimer = VulnerableTimer / SecondStageValue;
            Animation.speed = SecondStageValue;
        }
        
        public void DefaultAttack(){
            isDefaultAttackPlay = true;
            isHaunt = false;
            Animation.Play("Hit_left");
        }
        public void DefaultRightAttack(){
            isDefaultAttackPlay = true;
            isHaunt = false;
            Animation.Play("Hit_right");
        }
        public void DefaultLeftRightAttack(){
            isDefaultAttackPlay = true;
            isHaunt = false;
            Animation.Play("Hit_both");
        }
        public void NextAttack(){
            isDefaultAttackPlay = false;
            isHaunt = true;
        }
        public void ExitDefaultAttack(){
            isDefaultAttackPlay = false;
            isDefaultAttack = false;
            isIdle = true;
        }
        
        public void DefaultLeftRightStrongAttack(){
            isDefaultAttackPlay = true;
            isHaunt = false;
            Animation.Play("Hit_both_stuck");
        }
        
        public void ExitStrongAttack(){
            isDefaultAttackPlay = false;
            isStrongAttacks = false;
            isVulnerable = true;
        }
        
        public void ExitVulnerable(){
            HitBox[0].SetActive(false);
            HitBox[1].SetActive(false);

            isVulnerablePlay = false;
            isVulnerable = false;
            isIdle = true;
            left.enabled = true;
            right.enabled = true;

        }

        public void Vulnerable(){
            isVulnerablePlay = true;
            HitBox[0].SetActive(true);
            HitBox[1].SetActive(true);

            Animation.Play("Stuck");
            left.enabled = false;
            right.enabled = false;
        }
       
        public void CarThrow(){
            isCarThrowPlay = true;
            Animation.Play("Throw_car");
            if (Health < 50&&carThrowAnimCount==0){
                carThrowAnimCount = 1;
            }
            else{
                carThrowAnimCount = 0;
            }
        }
        
        public void NextCarThrow(){
            if (carThrowAnimCount>0){
                CarThrow();
            }
            else{
                isCarThrowPlay = false;
                if (Health < 50){
                    carThrowCount = 2;
                }
                else{
                    carThrowCount = 1;
                }
                
                Animation.Play("Idle");
            }
        }

        public void ExitCarThrow(){
            carThrowAnimCount = 0;
            carThrowCount = 0;
            isCarThrowPlay = false;
            isCarThrow = false;
            isIdle = true;
        }
        public void SpawnCar(){
            var obj=Instantiate(car, pointToSpawnCar.transform.position,Quaternion.identity);
            obj.transform.position=new Vector3(player.position.x, pointToSpawnCar.transform.position.y, car.transform.position.z);
        }
        
        public void SpawnEarth(){
            if(Health<50){
                earthCount = (int) (earthCount*1.5f);
            }
                Vector3 positionR = new Vector3(pointToSpawnEarth.transform.position.x+1.5f, pointToSpawnEarth.transform.position.y, transform.position.z);
                var objR = Instantiate(this.earth,positionR,Quaternion.identity);
                objR.GetComponent<Earth>().Init(earthCount,true);

                Vector3 positionL = new Vector3(pointToSpawnEarth.transform.position.x-1.5f, pointToSpawnEarth.transform.position.y, transform.position.z);
                var objL = Instantiate(this.earth,positionL,Quaternion.identity);
                objL.GetComponent<Earth>().Init(earthCount,false);
        }
        
        
        
        public void NailShots(){
            isNailShotsPlay = true;
            Animation.Play("Cabin_shooting");
            if (currentNailMarker){
                foreach (GameObject marker in currentNailMarker.markers){
                    var target =new Vector3(marker.transform.position.x, marker.transform.position.y,
                        marker.transform.position.z);
                    var nail=Instantiate(this.nail, pointToSpawnNail.transform.position,Quaternion.identity);
                    Nail comp=nail.GetComponent<Nail>();
                    comp.target = target;
                    comp.isMove = true;
                }
                if (Health < 50){
                    foreach (GameObject marker in currentNailMarker.leftMarkers){
                        var target =new Vector3(marker.transform.position.x, marker.transform.position.y,
                            marker.transform.position.z);
                        var nail=Instantiate(this.nail, pointToSpawnNail.transform.position,Quaternion.identity);
                        Nail comp=nail.GetComponent<Nail>();
                        comp.target = target;
                        comp.isMove = true;
                    }
                    foreach (GameObject marker in currentNailMarker.rightMarkers){
                        var target =new Vector3(marker.transform.position.x, marker.transform.position.y,
                            marker.transform.position.z);
                        var nail=Instantiate(this.nail, pointToSpawnNail.transform.position,Quaternion.identity);
                        Nail comp=nail.GetComponent<Nail>();
                        comp.target = target;
                        comp.isMove = true;
                    }
                }
                Destroy(currentNailMarker.gameObject);
            }
        }
        
        public void ExitNailShots(){
            isNailShotsPlay = false;
            isNailShots = false;
            isIdle = true;
        }

        
        public void MakeMarkerNailShots(){
            var obj=Instantiate(NailMarker, pointToSpawnNailMarker.transform.position,Quaternion.identity);
            obj.transform.position=new Vector3(player.position.x, pointToSpawnNailMarker.transform.position.y, NailMarker.transform.position.z);
            currentNailMarker = obj.GetComponent<NailMarker>();
            if (Health < 50){
                currentNailMarker.AddBoth();
            }
        }

        
        public void Damage(int damage){
            if (Health <50&&!SecondStage){
                ChangeStateByHealth();
            }
                
            Health = Health - damage;
            damaged?.Invoke(Health);
            Debug.Log("HEalth boss "+Health);
        }
        
        public void Death(){
            death = true;
            Animation.updateMode = AnimatorUpdateMode.UnscaledTime;
            Animation.Play("death");
            
        }
        
        public void Atencion(){
            StartCoroutine(AtecionGO());
        }


        public IEnumerator AtecionGO(){
            AtencionIcon.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            AtencionIcon.SetActive(false);
            NailShots();
        }
    }
}