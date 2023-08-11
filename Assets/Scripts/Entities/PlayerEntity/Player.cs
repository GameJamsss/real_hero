using System;
using System.Collections;
using Assets.Scripts.Entities.BossEntity;
using Assets.Scripts.StateMachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Entities.PlayerEntity
{
	public class Player : MonoBehaviour
	{
		[Header("Velocity")]
		[SerializeField] public float Velocity = 5.0f;

		[Header("Acceleration")]
		[SerializeField] public float Acceleration = 2.0f;

		[Header("Decceleration")]
		[SerializeField] public float Decceleration = 2.0f;

		[Header("Jump height")]
		[SerializeField] public float JumpHeight = 5.0f;

		[Header("Max air jumps")]
		[SerializeField] public int MaxAirJumps = 1;

		[Header("Fall gravity scale")]
		[SerializeField] public float FallGravityScale = 2f;

		[Header("The time need to be passed before new dash in milliseconds")]
		[SerializeField] public float DashOffsetSec = 0.5f;

		[Header("How long player will be dashing without control in milliseconds")]
		[SerializeField] public float UncontrollableDashTimeSec = 0.2f;

		[Header("How long player will be dashing in milliseconds overall")]
		[SerializeField] public float DashTimeMilliAll = 0.4f;

		[Header("Dash power")]
		[SerializeField] public float DashPower = 8f;

		[Header("Stun duration in seconds")]
		[SerializeField] public float StunDuration = 1.5f;

		[Header("Knock back power")]
		[SerializeField] public float KnockBackPower = 2f;

		[Header("Knock up power")]
		[SerializeField] public float KnockUpPower = 2f;

		[Header("Ground Layer Mask")]
		[SerializeField] public LayerMask GroundMask;

		[HideInInspector] public Collider2D Collider;

		[HideInInspector] public Animator Animation;

		[HideInInspector] public Rigidbody2D Rigidbody;

		[HideInInspector] public GameObject AttackObjectTest;

		[HideInInspector] public int AirJumpsCounter = 0;

		[HideInInspector] public float CurrentDashTimeSec = 0f;

		[HideInInspector] public float CurrentDashOffsetSec = 0f;

		[HideInInspector] public float LastDashVector = 0f;

		[HideInInspector] public float CurrentStunDuration = 0f;

		StateMachine<Player> fsm;

		public GameObject AttackBall;
		public SpriteRenderer spriteRenderer;

		public GameObject dash;
		public event Action<int> damaged;

		public float attackCooldown = 1.0f; // Кулдаун между атаками
		public float attackDuration = 0.01f; // Длительность hitball


		private bool isAttacking = false;
		private float cooldownTimer = 0.0f;

		[SerializeField]
		public Color _blinkColor = Color.red;

		private Material originalMaterial;

		[SerializeField]
		public Material blinkMaterial;

		public float _flashDuration = 0.2f;

		public bool _isDamaged = false;

		public bool death;

		public AudioSource walk;
		public AudioClip[] walkSteps;
		public Boss Boss;
		void Start()
		{
			originalMaterial = spriteRenderer.material;

			Animator animation = gameObject.GetComponent<Animator>();
			if (animation) Animation = animation; else Debug.LogError(gameObject.name + " need Animation");

			Collider2D col = gameObject.GetComponent<Collider2D>();
			if (col) Collider = col; else Debug.LogError(gameObject.name + " need Collider2d");

			Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
			if (rb) Rigidbody = rb; else Debug.LogError(gameObject.name + " need Rigidbody2d");

			//GameObject attackArea = GameObject.Find("AttackArea");
			//if (attackArea) AttackObjectTest = attackArea; else Debug.LogError("Need attack area object on: " + gameObject.name);

			LastDashVector = transform.localScale.x;

			fsm = new StateMachine<Player>(
				new StateManager<Player>(this)
				.AddState(StateMap.Idle)
				.AddState(StateMap.Move)
				.AddState(StateMap.Jump)
				.AddState(StateMap.Fall)
				.AddState(StateMap.MoveUp)
				.AddState(StateMap.Damage)
				.AddState(StateMap.Dash)
			);

		}

		void Update()
		{
			if (!death && !Boss.death)
			{
				fsm.Run();
			}
			//Debug.Log(fsm.CurrentState.Name);

			if (Input.GetButton("Fire1"))
			{
				Attack();
			}
			if (isAttacking)
			{
				cooldownTimer -= Time.deltaTime;
				if (cooldownTimer <= 0)
				{
					isAttacking = false;
				}
			}
			// TODO: может жестко, но вроде понятно
			//Animation.enabled = !_isDamaged;
		}

		public void Damaged(int damage)
		{
			damaged?.Invoke(damage);
			StartCoroutine(HurtAndStun());
			_isDamaged = true;
		}

		public void Attack()
		{
			if (!isAttacking)
			{
				isAttacking = true;
				cooldownTimer = attackCooldown;
				Animation.Play("attack");
			}
		}
		private IEnumerator Hit()
		{
			AttackBall.SetActive(true);
			yield return new WaitForSeconds(attackDuration);
			AttackBall.SetActive(false);
			Animation.Play("Idle");
		}


		private IEnumerator HurtAndStun()
		{
			float elapsedTime = 0f;
			while (elapsedTime < StunDuration)
			{
				FlashEffect();

				elapsedTime += _flashDuration;
				yield return new WaitForSeconds(_flashDuration);
			}

			spriteRenderer.material = originalMaterial;
			_isDamaged = false;
		}

		private void FlashEffect()
		{
			if (spriteRenderer.material == originalMaterial)
			{
				spriteRenderer.material = blinkMaterial;
			}

			else
			{
				spriteRenderer.material = originalMaterial;
			}
		}


		public void Death()
		{
			death = true;
			Animation.updateMode = AnimatorUpdateMode.UnscaledTime;
			Animation.Play("death");
		}

		public void Walk()
		{
			walk.clip = walkSteps[Random.Range(0, walkSteps.Length)];
			walk.Play();
		}

		public void PlayDash()
		{
			StartCoroutine(DashActive());
		}

		public IEnumerator DashActive()
		{
			dash.SetActive(true);
			yield return new WaitForSeconds(0.3f);
			dash.SetActive(false);
		}
	}
}
