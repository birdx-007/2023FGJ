using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosterController : MonoBehaviour
{
    PlayerCharacter character;
    Transform targetTrans;
    float direct ;

    void Start ()
    {
        character = GetComponent<PlayerCharacter>();

        targetTrans = GameObject.FindGameObjectWithTag("Player").transform; //玩家名
        

        

    }


	void Update ()
    {
        direct = targetTrans.position.x -character.transform.position.x;
        Debug.Log(direct);
        character.Move(direct,false);

    }
}
