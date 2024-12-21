using System;
using System.Collections.Generic;
using Duck;
using UnityEngine;

namespace Actors
{
	public class GraphicBehaviour : MonoBehaviour
	{
		public enum AnimationType
		{
			Move,
			Swim,
			Buried,
			Quack
		}
		
		[SerializeField]
		Animator animator;
		[SerializeField]
		DuckBehavior duckBehavior;
		[SerializeField]
		Movement movement;
        
		[Header("Parametros")]
		[SerializeField] private string _moveParameter;
		[SerializeField] private string _swimParameter;
		[SerializeField] private string _buriedParameter;
		[SerializeField] string _quackParameter;

		private Dictionary<AnimationType, string> animationParameters;

		private void Awake()
		{
			animationParameters = new Dictionary<AnimationType, string>
			{
				{ AnimationType.Move, _moveParameter },
				{ AnimationType.Swim, _swimParameter },
				{ AnimationType.Buried, _buriedParameter },
				{ AnimationType.Quack, _quackParameter }
			};
		}
		
		public void SetAnimation(AnimationType animation, bool value)
		{
			if (animationParameters.TryGetValue(animation, out var parameter) && !string.IsNullOrEmpty(parameter))
			{
				animator.SetBool(parameter, value);
			}
		}
		
		public void TriggerAnimation(AnimationType animation)
		{
			if (animationParameters.TryGetValue(animation, out var parameter) && !string.IsNullOrEmpty(parameter))
			{
				animator.SetTrigger(parameter);
			}
		}

		Vector2 direction;
        
		public Vector2 Direction
		{
			get => direction;
			set
			{
				if(Mathf.Approximately(value.sqrMagnitude, 0))
					return;
                
				if(!Mathf.Approximately(value.x, 0))
					transform.localScale = new Vector3(value.x > 0 ? -1 : 1, 1, 1);
                
				direction = value;
			}
		}

		public bool IsMoving
		{
			get => animator.GetBool(_moveParameter);
			set => animator.SetBool(_moveParameter, value);
		}
        
		public bool IsSwimming
		{
			get => animator.GetBool(_swimParameter);
			set => animator.SetBool(_swimParameter, value);
		}
        
		public bool IsBuried
		{
			get => animator.GetBool(_buriedParameter);
			set => animator.SetBool(_buriedParameter, value);
		}
        
		public bool IsQuacking
		{
			get
			{
				if (!animator
				    || !animator.runtimeAnimatorController)
					return false;
				return animator.GetCurrentAnimatorStateInfo(0).IsName("Grasnando");
			}
            
			set
			{
				if(string.IsNullOrEmpty(_quackParameter)
				   || !value)
					return;
                
				animator.SetTrigger(_quackParameter);
			}
		}
		
		
		void OnMove(Vector2 velocity, bool isMoving)
		{
			IsMoving = isMoving;
			Direction = velocity.normalized;
		}
        
		void OnQuacking()
		{
			IsQuacking = true;
		}

		void OnWater(bool isOnWater)
		{
			IsSwimming = isOnWater;
		}

		void Start()
		{
			if(duckBehavior)
				duckBehavior.OnQuacking.AddListener(OnQuacking);
			
			if (movement)
			{
				movement.OnMoved.AddListener(OnMove);
				movement.OnWaterEvent.AddListener(OnWater);
			}
		}

#if UNITY_EDITOR
		void Reset()
		{
			animator = GetComponentInParent<Animator>(true);
			movement = GetComponentInParent<Movement>(true);
		}
#endif
	}
}