using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction{
    N, S, E, W
}

public class SnakeController : MonoBehaviour
{
    #region public variables
    public SnakeJoint snakeHead;
    public SnakeJoint snakeBody;
    #endregion

    #region private variables
    private Vector2 direction = Vector2.up;
    private int lastPos = 1;
    private List<SnakeJoint> snake;
    private float moveTimer;
    private float moveMaxTime = 0.2f;
    #endregion

    private void Start() {
        snake = new List<SnakeJoint>();
        snake.Add(snakeHead);
    }

    void Update()
    {
        moveTimer += Time.deltaTime;
        if(moveTimer >= moveMaxTime){
            ChangePosition();

            moveTimer = 0;
        }else{
            if(Input.GetKeyDown(KeyCode.UpArrow) && !direction.Equals(Vector2.down)){
                SetDirection(Vector2.up);
            }else if(Input.GetKeyDown(KeyCode.DownArrow) && !direction.Equals(Vector2.up)){
                SetDirection(Vector2.down);
            }else if(Input.GetKeyDown(KeyCode.LeftArrow) && !direction.Equals(Vector2.right)){
                SetDirection(Vector2.left);
            }else if(Input.GetKeyDown(KeyCode.RightArrow) && !direction.Equals(Vector2.left)){
                SetDirection(Vector2.right);
            }

            if(Input.GetKeyDown(KeyCode.A)){
                AddJoint();
            }
        }
    }

    public void GoUp(){
        SetDirection(Vector2.up);
    }

    public void GoDown(){
        SetDirection(Vector2.down);
    }

    public void GoRight(){
        SetDirection(Vector2.right);
    }

    public void GoLeft(){
        SetDirection(Vector2.left);
    }

    public void SetDirection(Vector2 _dir){
        direction = _dir.normalized;
    }

    public void ChangePosition(){
        var previousPosition = snakeHead.transform.position;
        snakeHead.transform.position = snakeHead.transform.position + new Vector3(direction.x, 0, direction.y);
        snakeHead.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));

        if(snakeHead.GetNextJoint() != null){
            snakeHead.GetNextJoint().MoveJoint(previousPosition, direction);
        }
    }

    public void AddJoint(){

        var instance = Instantiate(snakeBody, snake[lastPos-1].transform.position, Quaternion.identity);
        instance.transform.SetParent(transform);
        snake.Add(instance.GetComponent<SnakeJoint>());

        snake[lastPos-1].SetNextJoint(instance.GetComponent<SnakeJoint>());
        snake[lastPos].SetPrevJoint(snakeHead);

        snake[lastPos].index = lastPos;

        lastPos++;
    }
}
