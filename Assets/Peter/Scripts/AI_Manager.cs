using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class AI_Manager : MonoBehaviour
{
    [SerializeField]
    protected GameObject Player;
    public List<AI_Pawn> list_of_pawns;

    public List<AI_Pawn> list_of_melee_pawns;
    

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        list_of_pawns = GetComponentsInChildren<AI_Pawn>().ToList();
        if (list_of_pawns != null)
        {
            foreach (var enemy in list_of_pawns)
            {
                if (enemy._enemy_type==AI_Pawn.Enemy_Type.Melee)
                {
                    list_of_melee_pawns.Add(enemy);
                }
            }
        }


        if (Player!=null&&list_of_melee_pawns!=null)
        {
            MoveAllGeneric(list_of_melee_pawns);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MoveAllGeneric(List<AI_Pawn> enemy_list)
    {
        foreach (var pawn in enemy_list)
        {
            pawn.NavMeshAgent.SetDestination(Player.transform.position);
        }
    }
}
