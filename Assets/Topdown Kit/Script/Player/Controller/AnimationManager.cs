/// <summary>
/// Animation manager.
/// This script use for control an animation character
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationManager : MonoBehaviour {
	
	
	public delegate void AnimationHandle();
	public AnimationHandle animationState;
		
	
	[System.Serializable]
	public class AnimationType01
	{
		public AnimationClip animation;
		public float speedAnimation = 1.0f;
	}
	[System.Serializable]
	public class AnimationType02
	{
		public AnimationClip animation;
		public float speedAnimation = 1.0f;
		public bool speedTuning;
	}
	
	[System.Serializable]
	public class AnimationNormalAttack
	{
		public string _name = "Normal Attack";
		public AnimationClip animation;
		public float speedAnimation = 1.0f;
		public float attackTimer = 0.5f;
		public float multipleDamage = 1f;
		public float flichValue;
		public bool speedTuning;
		
		public GameObject attackFX;
		public AudioClip soundFX;
		
	}
	[System.Serializable]
	public class AnimationCritAttack
	{
		public string _name = "Critical Attack";
		public AnimationClip animation;
		public float speedAnimation = 1.0f;
		public float attackTimer = 0.5f;
		public float multipleDamage = 1f;
		public float flichValue;
		public bool speedTuning;
		
		public GameObject attackFX;
		public AudioClip soundFX;
		
	}
	
	[System.Serializable]
	public class AnimationTakeAttack
	{
		public string _name  = "Take Attack";
		public AnimationClip animation;
		public float speedAnimation = 1.0f;
		
	}
	
	[System.Serializable]
	public class AnimationSkill
	{
		public string skillType;
		public int skillIndex;
		public AnimationClip animationSkill;
		public float speedAnimation;
		public float activeTimer;
		public bool speedTuning;
		
	}
	
	public AnimationType01 idle,cast,death; //idle cast death animation
	public AnimationType02 move; //move animation
	public List<AnimationNormalAttack> normalAttack; //normal attack
	public List<AnimationCritAttack> criticalAttack; //critical attack
	public List<AnimationTakeAttack> takeAttack; //take attack
	
	[HideInInspector]
	public AnimationSkill skillSetup;
	
	//Private Variable
	private HeroController heroController;
	private PlayerStatus playerStatus;
	private PlayerSkill playerSkill;
	[HideInInspector]
	public bool checkAttack;
	
	
	//Editor Variable
	[HideInInspector]
	public int sizeNAtk=0;
	[HideInInspector]
	public int sizeCritAtk=0;
	[HideInInspector]
	public int sizeTakeDmg=0;
	[HideInInspector]
	public List<bool> showNormalAtkSize = new List<bool>();
	[HideInInspector]
	public List<bool> showCritSize = new List<bool>();
	[HideInInspector]
	public List<bool> showTakeDmgSize = new List<bool>();
	
	
	// Use this for initialization
	void Start () {
		
		heroController = this.GetComponent<HeroController>();
		playerStatus = this.GetComponent<PlayerStatus>();
		playerSkill = this.GetComponent<PlayerSkill>();
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(animationState != null){
			animationState();	
		}
	
	}
	
	//Idle Method
	public void Idle(){
		GetComponent<Animation>().CrossFade(idle.animation.name);
		GetComponent<Animation>()[idle.animation.name].speed = idle.speedAnimation;
	}
	
	//Move Method
	public void Move(){
		GetComponent<Animation>().Play(move.animation.name);
		
		if(move.speedTuning)  //Enable Speed Tuning
		{
			GetComponent<Animation>()[move.animation.name].speed = (playerStatus.status.movespd/3f)/move.speedAnimation;	
		}else
		{
			GetComponent<Animation>()[move.animation.name].speed = move.speedAnimation;
		}
		
		
	}
	
	//Attack Method
	public void Attack()
	{	
		GetComponent<Animation>().Play(normalAttack[heroController.typeAttack].animation.name);
		
		if(normalAttack[heroController.typeAttack].speedTuning)  //Enable Speed Tuning
		{
			GetComponent<Animation>()[normalAttack[heroController.typeAttack].animation.name].speed = (playerStatus.status.atkSpd/100f)/normalAttack[heroController.typeAttack].speedAnimation;	
		}else
		{
			GetComponent<Animation>()[normalAttack[heroController.typeAttack].animation.name].speed = normalAttack[heroController.typeAttack].speedAnimation;
		}
			
		//Calculate Attack
		if(GetComponent<Animation>()[normalAttack[heroController.typeAttack].animation.name].normalizedTime > normalAttack[heroController.typeAttack].attackTimer && !checkAttack)
		{
			
			//Attack Damage
			EnemyController enemy;
			enemy = heroController.target.GetComponent<EnemyController>();
			enemy.EnemyLockTarget(heroController.gameObject);
			enemy.GetDamage((playerStatus.status.atk) * normalAttack[heroController.typeAttack].multipleDamage ,(playerStatus.status.hit),normalAttack[heroController.typeAttack].flichValue
				,normalAttack[heroController.typeAttack].attackFX,normalAttack[heroController.typeAttack].soundFX);
			
			
			checkAttack = true;
		}
			
		if(GetComponent<Animation>()[normalAttack[heroController.typeAttack].animation.name].normalizedTime > 0.9f)
		{
			heroController.ctrlAnimState = HeroController.ControlAnimationState.WaitAttack;
			checkAttack = false;
		}
	}
	
	//Critical Method
	public void CriticalAttack()
	{	
		GetComponent<Animation>().Play(criticalAttack[heroController.typeAttack].animation.name);
		
		if(criticalAttack[heroController.typeAttack].speedTuning)  //Enable Speed Tuning
		{
			GetComponent<Animation>()[criticalAttack[heroController.typeAttack].animation.name].speed = (playerStatus.status.atkSpd/100f)/criticalAttack[heroController.typeAttack].speedAnimation;	
		}else
		{
			GetComponent<Animation>()[criticalAttack[heroController.typeAttack].animation.name].speed = criticalAttack[heroController.typeAttack].speedAnimation;
		}
			
		//Calculate Attack
		if(GetComponent<Animation>()[criticalAttack[heroController.typeAttack].animation.name].normalizedTime > criticalAttack[heroController.typeAttack].attackTimer && !checkAttack)
		{
			
			//Attack Damage
			EnemyController enemy;
			enemy = heroController.target.GetComponent<EnemyController>();
			enemy.EnemyLockTarget(heroController.gameObject);
			enemy.GetDamage((playerStatus.status.atk) * criticalAttack[heroController.typeAttack].multipleDamage ,10000,criticalAttack[heroController.typeAttack].flichValue
				,criticalAttack[heroController.typeAttack].attackFX,criticalAttack[heroController.typeAttack].soundFX);
			
			
			checkAttack = true;
		}
			
		if(GetComponent<Animation>()[criticalAttack[heroController.typeAttack].animation.name].normalizedTime > 0.9f)
		{
			heroController.ctrlAnimState = HeroController.ControlAnimationState.WaitAttack;
			checkAttack = false;
		}
	}
	
	//Take attack method
	public void TakeAttack(){
		GetComponent<Animation>().CrossFade(takeAttack[heroController.typeTakeAttack].animation.name);
		GetComponent<Animation>()[takeAttack[heroController.typeTakeAttack].animation.name].speed = takeAttack[heroController.typeTakeAttack].speedAnimation;
		
		if(GetComponent<Animation>()[takeAttack[heroController.typeTakeAttack].animation.name].normalizedTime > 0.9f)
		{
			if(heroController.target != null)
			{
				heroController.ctrlAnimState = HeroController.ControlAnimationState.WaitAttack;
			}else
			{
				heroController.ctrlAnimState = HeroController.ControlAnimationState.Idle;
			}
		}
	}
	
	//Cast method
	public void Cast()
	{
		GetComponent<Animation>().CrossFade(cast.animation.name);
		GetComponent<Animation>()[cast.animation.name].speed = cast.speedAnimation;
	}
	
	//Death method
	public void Death()
	{
		GetComponent<Animation>().CrossFade(death.animation.name);
		GetComponent<Animation>()[death.animation.name].speed = death.speedAnimation;
		
		if(GetComponent<Animation>()[death.animation.name].normalizedTime > 0.9f)
		{
			heroController.DeadReset();
		}
	}
	
	//Skill Method
	public void ActiveSkill()
	{
		GetComponent<Animation>().Play(skillSetup.animationSkill.name);
		
		if(skillSetup.speedTuning)  //Enable Speed Tuning
		{
			GetComponent<Animation>()[skillSetup.animationSkill.name].speed = (playerStatus.status.atkSpd/100f)/skillSetup.speedAnimation;	
		}else
		{
			GetComponent<Animation>()[skillSetup.animationSkill.name].speed = skillSetup.speedAnimation;
		}
			
		//Calculate Attack
		if(GetComponent<Animation>()[skillSetup.animationSkill.name].normalizedTime > skillSetup.activeTimer && !checkAttack)
		{	
			//playerSkill.ActiveSkill(skillSetup.skillType,skillSetup.skillIndex);
			checkAttack = true;
		}
			
		if(GetComponent<Animation>()[skillSetup.animationSkill.name].normalizedTime > 0.9f)
		{
			heroController.ctrlAnimState = HeroController.ControlAnimationState.WaitAttack;
			checkAttack = false;
		}
	}
	
}
