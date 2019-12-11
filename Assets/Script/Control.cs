using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    bool nextJump, isJumping, isStanding, isWalking, isGround=false, isRunning,isFalling=true;
    float Haxis;
    float moveSpeed = 20f;//移動速度
    float OmoveSpeed = 20f;
    //float jumpMultiple = 1;
    float jumpVelocity=10f; //落下速度
    float OjumpVelocity = 10f;
    float jumpV_x ;//走路速度
    float jumpHight ;//跳躍高度
    float jumpSpeed=300; //跳躍速度
    
    void FixedUpdate()
    {
        
        

        Haxis=Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)&&isGround)
        {         //不能在落地前跳跃
           
            print("Input.GetKeyDown(KeyCode.Space)");

            {
                nextJump = false;//落地前无法再次起跳
                isJumping = true;//进入跳跃状态
               
                if (isStanding)
                {
                    jumpV_x = 0;//处于站立状态时水平初速度为0
                    isStanding = false;//改变当前状态由站立到跳跃，下同
                }
                if (isWalking)
                {
                    jumpV_x = Haxis * moveSpeed;
                    isWalking = false;
                }

                //if (isRunning)                //加速跳跃
                //{
                //     jumpV_x = Haxis * moveSpeed;
                //    jumpVelocity = jumpVelocity * jumpMultiple;//加速跳跃时竖向分速度也提高
                //    isRunning = false;
                //}
            }
        }
    }
    
     void LateUpdate()
        //当Behaviour启用时，其LateUpdate在每一帧被调用。
        //LateUpdate是在所有Update函数调用后被调用。这可用于调整脚本执行顺序。
        //例如:当物体在Update里移动时，跟随物体的相机可以在LateUpdate里实现。
        //都是在同一偵裡，但update先執行再來是lateupdate

        {
       Vector3 currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        jumpHight = currentPosition.y;
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed * Haxis); //角色移动实现
        if (isJumping==true)//跳躍
        {
           
            jumpHight +=  Time.deltaTime * jumpSpeed;//高度 += 初速*速度  (向上速度           
            currentPosition.y += jumpHight;           
            Vector3 privousPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            currentPosition.x = privousPosition.x + Time.deltaTime * jumpV_x; //空中水平移动实现  // jumpV_x 是走路的速度 
            //transform.position = currentPosition;  //將位置移到計算後的位置
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,currentPosition,0.1f);

            isFalling = true;
            isJumping = false;
           
        }
        if (isFalling == true)//空中落下              
        {
            jumpHight -= jumpVelocity * Time.deltaTime ;//高度 -= 初速*速度  (向下速度
            jumpVelocity = jumpVelocity - 9.8f * Time.deltaTime ; //落下速度 (都會乘上jumpSpeed            
            currentPosition.y = jumpHight;
            Vector3 privousPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            currentPosition.x = privousPosition.x + Time.deltaTime * jumpV_x; //空中水平移动实现  // jumpV_x 是走路的速度 
            transform.position = currentPosition;  //將位置移到計算後的位置
            jumpVelocity = OjumpVelocity; //還原落下速度
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
        {
        


            //落地判定
            if (collider.gameObject.tag == "Ground")
            {
                //nextJump = true;
                isGround = true;
                isJumping = false;
                isFalling = false;
            //還原速度
                moveSpeed = OmoveSpeed;
                jumpVelocity = OjumpVelocity;               
                jumpV_x = 0;              
               
            }
        }
    void OnTriggerExit2D(Collider2D collider)
    {
        //離地判定
        if (collider.gameObject.tag == "Ground")
        {
            isFalling = true;
            print("Exit");
        }
    }




}






