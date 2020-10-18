using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeJoint : MonoBehaviour
{
    public bool isHead;
    public int index;

    private SnakeJoint prevJoint;
    private SnakeJoint nextJoint;
    private Vector2 lastDir;

    public void MoveJoint(Vector3 newPos, Vector2 dir){
        Vector3 prevPos = Vector3.zero;
        var distance = Vector3.Distance(transform.position, prevJoint.transform.position);
        if(distance > 1f){
            prevPos = transform.position;
            transform.position = newPos;
        }

        if(nextJoint != null){
            nextJoint.MoveJoint(prevPos, dir);
        }

        transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.y));

        if(index == 2){
            Debug.Log(transform.position);
        }

    }

    public void SetPrevJoint(SnakeJoint snakeJoint){
        prevJoint = snakeJoint;
    }

    public void SetNextJoint(SnakeJoint snakeJoint){
        nextJoint = snakeJoint;
    }

    public SnakeJoint GetNextJoint(){
        return nextJoint;
    }
}
