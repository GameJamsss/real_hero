using Assets.Scripts.Domain;
using Assets.Scripts.Entities.PlayerEntity.ModifierImpl;
using Assets.Scripts.StateMachine;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Entities.PlayerEntity
{
	public static class StateMap
	{
		private static readonly GenericMoveModifier gmm = new GenericMoveModifier();
		private static readonly SlideModifier sm = new SlideModifier();
		private static readonly GenericJumpModifier gjm = new GenericJumpModifier();
		private static readonly GravityFallModifier gfm = new GravityFallModifier();
		private static readonly DashOffsetResetModifier dorm = new DashOffsetResetModifier();

		public static State<Player> Idle = new State<Player>("Idle", (ulong)StatePriority.Idle)
			.SetOnStateEnter(entity =>
			{
				entity.AirJumpsCounter = 0;
				entity.Animation.Play("Idle");
			})
			.AddModifier(dorm)
			.AddModifier(sm);

		public static State<Player> Move = new State<Player>("Move", (ulong)StatePriority.Move)
			.SetOnStateEnter(entity =>
			{
				entity.Animation.Play("Run");
			})
			.SetEnterCondition(entity => Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
			.AddModifier(dorm)
			.AddModifier(gmm);

		public static State<Player> Jump = new State<Player>("Jump", (ulong)StatePriority.Jump)
			.SetEnterCondition(entity => Input.GetButtonDown("Jump") && Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask))
			.SetOnStateEnter(entity =>
			{
				entity.AirJumpsCounter = 0;
				entity.Animation.Play("jump");
			}
			)
			.AddModifier(gmm)
			.AddModifier(dorm)
			.AddModifier(gjm);

		public static State<Player> AirJump = new State<Player>("AirJump", (ulong)StatePriority.AirJump)
			.SetEnterCondition(entity => Input.GetButtonDown("Jump") && entity.AirJumpsCounter < entity.MaxAirJumps)
			.SetOnStateEnter(entity =>
			{
				entity.AirJumpsCounter = entity.AirJumpsCounter + 1;
				entity.Rigidbody.velocity = new Vector2(entity.Rigidbody.velocity.x, entity.AirJumpHeight);
			}
			)
			.AddModifier(gmm)
			.AddModifier(dorm);

		public static State<Player> Fall = new State<Player>("Fall", (ulong)StatePriority.Fall)
			.SetEnterCondition(entity =>
				!Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask)
			)
			.SetOnStateEnter(entity => entity.Animation.Play("fallJump"))
			.AddModifier(gmm)
			.AddModifier(dorm)
			.AddModifier(gfm)
			.ToBlack(Move);

		public static State<Player> MoveUp = new State<Player>("MoveUp", (ulong)StatePriority.MoveUp)
			.SetEnterCondition(entity =>
				entity.Rigidbody.velocity.y > 0f
				&& !Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask)
			)
			.SetOnStateEnter(entity => entity.Animation.Play("jump"))
			.AddModifier(gmm)
			.AddModifier(dorm)
			.ToBlack(Move);

		public static State<Player> Dash = new State<Player>("Dash", (ulong)StatePriority.Dash)
			.SetEnterCondition(entity => Input.GetButton("Dash") && entity.CurrentDashOffsetSec > entity.DashOffsetSec)
			.SetOnStateEnter(entity =>
				{
					entity.PlayDash();
					Dash.Lock = true;
					entity.CurrentDashOffsetSec = 0f;
					entity.CurrentDashTimeSec = 0f;
					entity.Rigidbody.velocity = Vector2.zero;
					entity.LastDashVector = entity.transform.localScale.x;
				}
			)
			.SetStateLogic(entity =>
				{
					entity.CurrentDashTimeSec += Time.deltaTime;
					float vector =
						Mathf.Sign(entity.CurrentDashTimeSec > entity.UncontrollableDashTimeSec
							? entity.transform.localScale.x
							: entity.LastDashVector);

					entity.Rigidbody.velocity = Vector2.right * entity.DashPower * vector;

					Dash.Lock = entity.CurrentDashTimeSec < entity.DashTimeMilliAll;
				}
			);

		public static State<Player> Damage = new State<Player>("Damage", (ulong)StatePriority.Damage)
			.SetEnterCondition(entity =>
				{
					List<Collider2D> colliders = new List<Collider2D>();
					entity.Collider.OverlapCollider(new ContactFilter2D().NoFilter(), colliders);
					return colliders.Exists(col => col.gameObject.GetComponent<Damagable>() != null) && !entity._isDamaged;
				}
			)
			.SetOnStateEnter(entity =>
				{
					entity.CurrentStunDuration = 0f;
					List<Collider2D> colliders = new List<Collider2D>();
					entity.Collider.OverlapCollider(new ContactFilter2D().NoFilter(), colliders);
					GameObject goDamageZone = colliders
						.Select(col => col.gameObject)
						.ToList()
						.Find(dmg => dmg.GetComponent<Damagable>() != null);
					if (goDamageZone != null)
					{
						entity.Damaged(goDamageZone.GetComponent<Damagable>().GetDamage());
					}
				}
			)
			.AddModifier(dorm);

		public static State<Player> Attack = new State<Player>("Attack", (ulong)StatePriority.Attack)
			.SetEnterCondition(entity => Input.GetButtonDown("Fire1") && Physic.Unity.IsColliderTouchingGround(entity.Collider, entity.GroundMask))
			.SetOnStateEnter(entity =>
			{
				Attack.Lock = true;
				entity.cooldownTimer = 0;
				entity.Animation.Play("attack");
			})
			.SetStateLogic(entity =>
			{
				float animLength = entity.Animation.GetCurrentAnimatorStateInfo(0).length;
				entity.cooldownTimer = entity.cooldownTimer + Time.deltaTime;
				if ((entity.cooldownTimer > entity.attackCooldown && Input.anyKeyDown) || entity.cooldownTimer > animLength)
				{
					Attack.Lock = false;
				}
			})
			.AddModifier(sm);
	}
}
