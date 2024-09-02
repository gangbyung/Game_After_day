using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    static public Player Instance; //싱클톤

    
    public GameManager manager;
    [Header("플레이어의 현재 맵 위치를 알려주는 기능")]
    public string currentMapName; //transferMap 스크립트에 있는 transferMapName 변수의 값을 저장.

    [Header("플레이어 이동 관련 기능")]
    public float runSpeed;
    public float walkSpeed;
    public float currentSpeed;
    public float jumpPower;
    

    //기능들 넣어주기 리지드바디(물리), 스프라이트렌더러(플레이어 회전),애니메이터(플레이어 움직일때 모습)
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    GameObject scanObject;

    Vector3 dirVec;

    public string sceneToDestroy = "3.Endpart0";

    void Awake()
    {
        
    }
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            rigid = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
            
        }
        else
        {
            Destroy(this.gameObject);
        }

        
        
        Hud.Instance.UpdateUI();
    }
    void Update()
    {
        
        if (!Input.GetButtonUp("Horizontal") && (!Input.GetKey(KeyCode.Space))) //스태미나 회복
        {
            
            Increcovery(Hud.Instance.recoveryStamina * Time.deltaTime);
        }
        //jump
        if (Hud.Instance.currentStamina > Hud.Instance.jumpStamina)
        {
            if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

                anim.SetBool("isJumping", true);
                anim.SetBool("iswalking", false);
                

                DeJumpStamina(Hud.Instance.jumpStamina); //점프시 스태미나 감소
            }
        }
        //앉기
        if (Input.GetKey(KeyCode.S))
        {
            currentSpeed = 0f;
            anim.SetBool("isCrouch",true);
            IncDownrecovery(Hud.Instance.downRecoveryStamina * Time.deltaTime); //앉는 중일때 스태미나 회복

        }
        
        //애니메이션
        if (Mathf.Abs(rigid.velocity.x) < 0.2)
        {
            anim.SetBool("iswalking", false);
        }
        else
        {
            
            anim.SetBool("iswalking", true);
            anim.SetBool("isCrouch", false);
        }

        //Direction
        bool hDown = Input.GetButtonDown("Horizontal");
        bool hUp = Input.GetButtonUp("Horizontal");
        float h = Input.GetAxisRaw("Horizontal");

        if (hDown && h == -1)
            dirVec = Vector3.left;
        else if (hDown && h == 1)
            dirVec = Vector3.right;

        //대사출력
        if(Input.GetKeyDown(KeyCode.F) && scanObject != null)
        {
            manager.Action(scanObject);
        }
    }
    void FixedUpdate()
    {
        

        Movement();


        //레이캐스트 눈에 보이게 하기
        if (rigid.velocity.y < 0)
        {
            //Debug.DrawRay(rigid.position, Vector3.down * 2f, new Color(0, 1, 0));
            //레이 캐스트를 사용하여 무한점프 막기
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 2f, LayerMask.GetMask("platfrom"));

            if (rayHit.collider != null)
            {
                //Debug.DrawRay(rigid.position, rayHit.point - rigid.position, Color.yellow);
                //if (rayHit.distance < GetComponent<CapsuleCollider2D>().size.y + 0.5f)
                if (rayHit.distance < 5f)
                {
                    anim.SetBool("isJumping", false);
                }

            }
        }
        Debug.DrawRay(rigid.position, dirVec * 1f, new Color(0,1,0));
        RaycastHit2D rayHitObject = Physics2D.Raycast(rigid.position, dirVec, 1f, LayerMask.GetMask("Object"));

        if (rayHitObject.collider != null)
        {
            scanObject = rayHitObject.collider.gameObject;
        }
        else
            scanObject = null;
    }
        
    void Movement() //가로 움직임
    {
        float InputX = manager.isAction ? 0 : Input.GetAxis("Horizontal");
        if (InputX != 0f)
        {
            spriteRenderer.flipX = InputX < 0;//? true : false;
        }

        rigid.velocity = new Vector2(currentSpeed * InputX, rigid.velocity.y);

        currentSpeed = walkSpeed;

        if (Hud.Instance.currentStamina > 5f)
        {
            if (Input.GetKey(KeyCode.LeftShift)) //달리기
            {
                currentSpeed = runSpeed;

                DeRunStamina(Hud.Instance.runStamina * Time.deltaTime); //스태미나 감소


            }
        }
        
    }   
    
    

    //스태미너 증감
    void DeRunStamina(float amount) //달렸을때 스태미나 감소
    {
        Hud.Instance.currentStamina = Mathf.Clamp(Hud.Instance.currentStamina - amount, 0f, Hud.Instance.maxStamina);
        Hud.Instance.UpdateUI();
    }
    void DeJumpStamina(float amount) //점프했을때 스태미나 감소
    {
        Hud.Instance.currentStamina = Mathf.Clamp(Hud.Instance.currentStamina - amount, 0f, Hud.Instance.maxStamina);
        Hud.Instance.UpdateUI();
    }
    void Increcovery(float amount) //가만히있을때 스태미나 회복
    {
        Hud.Instance.currentStamina = Mathf.Clamp(Hud.Instance.currentStamina + amount, 0f, Hud.Instance.maxStamina);
        Hud.Instance.UpdateUI();
    }
    void IncDownrecovery(float amount) //앉았을때 스태미나 회복
    {

        Hud.Instance.currentStamina = Mathf.Clamp(Hud.Instance.currentStamina + amount, 0f, Hud.Instance.maxStamina);
        Hud.Instance.UpdateUI();
    }


    //private IEnumerator StaminaLatecortine(float amount)
    //{
    //    yield return new WaitForSeconds(1f);
    //}
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 만약 현재 씬이 지정된 씬 이름과 같다면
        if (scene.name == sceneToDestroy)
        {
            Destroy(gameObject); // 오브젝트를 파괴합니다.
        }
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 오브젝트가 파괴될 때 이벤트 핸들러를 제거합니다.
    }
}
