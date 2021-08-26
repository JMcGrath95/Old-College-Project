using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Werewolf_StateMachine : StateMachine
{
    public Werewolf_State_Attack attack_state;
    public Werewolf_State_Movement movement_state;
    public Animator animator;
    public NavMeshAgent meshAgent;
    public Transform Player;
    public Enemy_Stats Enemy;
    public PickupItem item;
    public string Walking ="Walking";
    public string Attack = "Melee Hit Action 1";
    
    // Start is called before the first frame update
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Enemy = gameObject.GetComponent<Enemy_Stats>();
        movement_state.UpdateComponent(meshAgent,Player,Enemy,animator,this);
        attack_state.UpdateComponents(meshAgent, Player, Enemy, animator, this);
        //GetComponent<EnemyHealth>().DeathEvent += D_Event;
        ChangeState(movement_state);       
    }

    // Update is called once per frame
    public override void Update()
    {
        Debug.Log(currentState.ToString());
        if (IsInRange())
        {
            
            ChangeState(attack_state);
        }
        else ChangeState(movement_state);
        base.Update();
    }
    bool IsInRange()
    {
        if (Vector3.Distance(meshAgent.transform.position, Player.position) <= Enemy.Enemy.AttackRange)
        {
            return true;
        }
        else return false;
    }
    //private void D_Event() 
    //{
    //    //item.item.ItemType = ItemUseType.InstantUse;
    //    item.testItem = true;
    //    Instantiate(item.gameObject,transform.position,Quaternion.identity);
    //}
}
