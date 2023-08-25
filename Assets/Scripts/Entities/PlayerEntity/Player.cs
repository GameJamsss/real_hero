using System;
using System.Collections;
using Assets.Scripts.Entities.BossEntity;
using Assets.Scripts.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
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

		[Header("Air Jump height")]
		[SerializeField] public float AirJumpHeight = 2.5f;

        [Header("Max air jumps")]
		[SerializeField] public int MaxAirJumps = 1;

		[Header("Fall gravity scale")]
		[SerializeField] public float FallGravityScale = 2f;

		[Header("The time need to be passed before new dash in milliseconds")]
		[SerializeField] public float DashOffsetSec = 10.0f;

		[Header("How long player will be dashing without control in milliseconds")]
		[SerializeField] public float UncontrollableDashTimeSec = 0.2f;

		[Header("How long player will be dashing in milliseconds overall")]
		[SerializeField] public float DashTimeMilliAll = 0.4f;

		[Header("Dash power")]
		[SerializeField] public float DashPower = 8f;

		[Header("blinkingDuration duration in seconds")]
		[SerializeField] public float blinkingDuration = 0.8f;

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


        public bool isAttacking = false;
		public float cooldownTimer = 0.0f;

		[SerializeField]
		public Color _blinkColor = Color.red;

		private Material originalMaterial;

		[SerializeField]
		public Material blinkMaterial;

		public float _flashDuration = 0.3f;

		public bool _isDamaged = false;

		public bool death;

		public AudioSource walk;
		public AudioClip[] walkSteps;
		public Boss Boss;

		private float _walkAudioMultiplier = 5.0f;

		public Image dashImage;
		void Start()
		{

			originalMaterial = spriteRenderer.material;

			Animator animation = gameObject.GetComponent<Animator>();
			if (animation) Animation = animation; else Debug.LogError(gameObject.name + " need Animation");

			Collider2D col = gameObject.GetComponent<Collider2D>();
			if (col) Collider = col; else Debug.LogError(gameObject.name + " need Collider2d");

			Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
			if (rb) Rigidbody = rb; else Debug.LogError(gameObject.name + " need Rigidbody2d");

			LastDashVector = transform.localScale.x;

			fsm = new StateMachine<Player>(
				new StateManager<Player>(this)
				.AddState(StateMap.Idle)
				.AddState(StateMap.Move)
				.AddState(StateMap.Jump)
				.AddState(StateMap.AirJump)
				.AddState(StateMap.Fall)
				.AddState(StateMap.MoveUp)
				.AddState(StateMap.Damage)
				.AddState(StateMap.Dash)
				.AddState(StateMap.Attack)
            );
        }
		bool a = true;
        void Update()
        {
			if (!death && !Boss.death)
			{
				fsm.Run();
				rotateSprite();
                dashImage.fillAmount = CurrentDashOffsetSec / DashOffsetSec;
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
			while (elapsedTime < blinkingDuration)
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
			StartCoroutine(PlayDeath());
		}
		public IEnumerator PlayDeath()
		{
			yield return new WaitForSeconds(0.1f);
			Animation.Play("death");
		}

		public void Walk()
		{
			walk.clip = walkSteps[Random.Range(0, walkSteps.Length)];
			walk.Play();
			//TODO: Volume level should be higher
			walk.volume *= _walkAudioMultiplier;
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

		private void rotateSprite()
		{
            float input = Input.GetAxis("Horizontal");
			if (input < 0) transform.localScale = new Vector3(-1, 1, 1);
			else if (input > 0) transform.localScale = new Vector3(1, 1, 1);
		}
	}
}
