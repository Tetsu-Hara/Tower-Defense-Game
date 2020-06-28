using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startBot : MonoBehaviour
{
    private Animator StartBot;
    public float moveSpeed = 0f;
    public float rotateSpeed = 1.0f;


    public Camera useCamera;
    private Transform player;
    private Vector3 offset; //位置偏移量
    private float smoothing = 3;  //平滑度

    public Transform easy;
    public Transform hard;
    public Transform exit;
    public GameObject enterCanva;
    public GameObject exitCanva;

    private void Start()
    {
        //相机
        player = this.gameObject.transform;
        offset = useCamera.transform.position - player.position;

        StartBot = GetComponent<Animator>();
        StartBot.SetBool("isRun", false);
        StartBot.SetBool("isAttack", false);
        moveSpeed = 0f;


        enterCanva.SetActive(false);
        exitCanva.SetActive(false);
    }

    private void Update()
    {

        enterCanva.SetActive(false);
        exitCanva.SetActive(false);

        //相机
        Vector3 targetPos = player.position + offset;
        useCamera.transform.position = Vector3.Lerp(useCamera.transform.position, targetPos, Time.deltaTime * smoothing);
        useCamera.transform.LookAt(new Vector3(player.position.x, player.position.y + 2, player.position.z));


        if (Input.GetKeyDown(KeyCode.W))
        {
            StartBot.SetBool("isRun", true);
            moveSpeed = 2.5f;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            StartBot.SetBool("isRun", false);
            moveSpeed = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            transform.Rotate(0, transform.rotation.y+180, 0);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartBot.SetBool("isAttack", true);
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            StartBot.SetBool("isAttack", false);
        }

        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);


        if (Vector3.Distance(transform.position, easy.position)<1.5)
        {
            enterCanva.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space)|| keySpace)
            {
                Debug.Log("3");
                SceneManager.LoadScene(1);
            }
            
        }
        if (Vector3.Distance(transform.position, hard.position) < 1.5)
        {
            enterCanva.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space)|| keySpace)
            {
                SceneManager.LoadScene(2);
            }

        }

        else if (Vector3.Distance(transform.position, exit.position) < 1.5)
        {
            exitCanva.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space)|| keySpace)
            {
                Application.Quit();
            }
            
        }

    }


    bool keySpace = false;
    public void OnSpaceKey()
    {
        Debug.Log("1");
        keySpace = true;
        Debug.Log("2");
    }

}
