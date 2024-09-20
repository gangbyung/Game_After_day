using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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

    public LayerMask platformLayer; // 플랫폼이 있는 레이어를 지정합니다.
    public float checkDistance = 0.1f; // 체크할 거리
    public float checkWidth = 0.5f; // 레이캐스트 발사 간격
    public Color rayColorAbove = Color.green; // 위쪽 레이 색상
    public Color rayColorBelow = Color.red; // 아래쪽 레이 색상
    //기능들 넣어주기 리지드바디(물리), 스프라이트렌더러(플레이어 회전),애니메이터(플레이어 움직일때 모습)
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    GameObject scanObject;

    Vector3 dirVec;

    ImageFader imgfade;
    Danger dan;
    public SoundManager soundManager;
    
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);

            rigid = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
            imgfade = FindObjectOfType<ImageFader>();
            dan = FindObjectOfType<Danger>();
            soundManager = FindObjectOfType<SoundManager>();
        }
        else
        {
            Destroy(this.gameObject);
        }

        
        
        Hud.Instance.UpdateUI();
    }
    void Update()
    {
        Vector2 position = transform.position;

        // 발 아래에서의 레이캐스트
        RaycastHit2D belowHit = Physics2D.Raycast(position, Vector2.down, checkDistance, platformLayer);

        // 발 아래에서 레이 시각화
        Debug.DrawRay(position, Vector2.down * checkDistance, rayColorBelow);

        // 발 아래에 플랫폼이 있으면 isTrigger 비활성화
        if (belowHit.collider != null)
        {
            belowHit.collider.isTrigger = false;
        }

        // 발 위쪽 방향으로 여러 개의 레이캐스트 발사
        for (float x = -checkWidth; x <= checkWidth; x += checkWidth)
        {
            Vector2 origin = position + new Vector2(x, 0);
            RaycastHit2D aboveHit = Physics2D.Raycast(origin, Vector2.up, checkDistance, platformLayer);

            // 발 위에서 레이 시각화
            Debug.DrawRay(origin, Vector2.up * checkDistance, rayColorAbove);

            // 발 위에 플랫폼이 있으면 isTrigger 활성화
            if (aboveHit.collider != null)
            {
                aboveHit.collider.isTrigger = true;
            }
        }

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
                soundManager.PlaySound(3);
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
        if (Mathf.Abs(rigid.velocity.x) < 1)
        {
            anim.SetBool("iswalking", false);
        }
        else
        {
            anim.SetBool("iswalking", true);
            anim.SetBool("isCrouch", false);
        }

        //Direction
        

        Next();
    }
    public void Next()
    {
        if (Input.GetKeyDown(KeyCode.F) && scanObject != null)
        {
            manager.NextTalk();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        scanObject = collision.gameObject;
        Debug.Log(scanObject);
        if (collision.CompareTag("NPC") && scanObject != null)
        {
            manager.Action(scanObject);
            
        }
        if (collision.CompareTag("Potal"))
        {
            imgfade.FadeInImgh();
        }
        if (collision.CompareTag("Danger"))
        {
            dan.FadeInOutRoutine();
        }
        

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("NPC") && scanObject != null)
        {
            scanObject = null;
        }
    }
    

    void FixedUpdate()
    {
        Movement();

        //레이캐스트 눈에 보이게 하기
        if (rigid.velocity.y < 0)
        {
            //레이 캐스트를 사용하여 무한점프 막기
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 2f, LayerMask.GetMask("platfrom"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 5f)
                {
                    anim.SetBool("isJumping", false);
                }

            }
        }
        
    }
        
    void Movement() //가로 움직임
    {
        bool hDown = Input.GetButtonDown("Horizontal");
        float h = Input.GetAxisRaw("Horizontal");

        if (hDown && h == -1)
            dirVec = Vector3.left;
        else if (hDown && h == 1)
            dirVec = Vector3.right;

        float InputX = manager.isAction ? 0 : Input.GetAxis("Horizontal");
        if (InputX != 0f)
        {
            soundManager.PlaySound(0);

            spriteRenderer.flipX = InputX < 0;//? true : false;
        }

        rigid.velocity = new Vector2(currentSpeed * InputX, rigid.velocity.y);

        currentSpeed = walkSpeed;

        if (Hud.Instance.currentStamina > 5f)
        {
            if (Input.GetKey(KeyCode.LeftShift)) //달리기
            {
                soundManager.StopSound(0);
                soundManager.PlaySound(1);

                currentSpeed = runSpeed;
                if(InputX != 0)
                    DeRunStamina(Hud.Instance.runStamina * Time.deltaTime); //스태미나 감소


            }
            else
            {
                soundManager.StopSound(1);
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
}
