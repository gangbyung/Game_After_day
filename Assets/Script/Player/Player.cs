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
    static public Player Instance; //��Ŭ��

    
    public GameManager manager;
    [Header("�÷��̾��� ���� �� ��ġ�� �˷��ִ� ���")]
    public string currentMapName; //transferMap ��ũ��Ʈ�� �ִ� transferMapName ������ ���� ����.

    [Header("�÷��̾� �̵� ���� ���")]
    public float runSpeed;
    public float walkSpeed;
    public float currentSpeed;
    public float jumpPower;
    

    //��ɵ� �־��ֱ� ������ٵ�(����), ��������Ʈ������(�÷��̾� ȸ��),�ִϸ�����(�÷��̾� �����϶� ���)
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
        
        if (!Input.GetButtonUp("Horizontal") && (!Input.GetKey(KeyCode.Space))) //���¹̳� ȸ��
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
                

                DeJumpStamina(Hud.Instance.jumpStamina); //������ ���¹̳� ����
            }
        }
        //�ɱ�
        if (Input.GetKey(KeyCode.S))
        {
            currentSpeed = 0f;
            anim.SetBool("isCrouch",true);
            IncDownrecovery(Hud.Instance.downRecoveryStamina * Time.deltaTime); //�ɴ� ���϶� ���¹̳� ȸ��

        }
        
        //�ִϸ��̼�
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

        //������
        if(Input.GetKeyDown(KeyCode.F) && scanObject != null)
        {
            manager.Action(scanObject);
        }
    }
    void FixedUpdate()
    {
        

        Movement();


        //����ĳ��Ʈ ���� ���̰� �ϱ�
        if (rigid.velocity.y < 0)
        {
            //Debug.DrawRay(rigid.position, Vector3.down * 2f, new Color(0, 1, 0));
            //���� ĳ��Ʈ�� ����Ͽ� �������� ����
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
        
    void Movement() //���� ������
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
            if (Input.GetKey(KeyCode.LeftShift)) //�޸���
            {
                currentSpeed = runSpeed;

                DeRunStamina(Hud.Instance.runStamina * Time.deltaTime); //���¹̳� ����


            }
        }
        
    }   
    
    

    //���¹̳� ����
    void DeRunStamina(float amount) //�޷����� ���¹̳� ����
    {
        Hud.Instance.currentStamina = Mathf.Clamp(Hud.Instance.currentStamina - amount, 0f, Hud.Instance.maxStamina);
        Hud.Instance.UpdateUI();
    }
    void DeJumpStamina(float amount) //���������� ���¹̳� ����
    {
        Hud.Instance.currentStamina = Mathf.Clamp(Hud.Instance.currentStamina - amount, 0f, Hud.Instance.maxStamina);
        Hud.Instance.UpdateUI();
    }
    void Increcovery(float amount) //������������ ���¹̳� ȸ��
    {
        Hud.Instance.currentStamina = Mathf.Clamp(Hud.Instance.currentStamina + amount, 0f, Hud.Instance.maxStamina);
        Hud.Instance.UpdateUI();
    }
    void IncDownrecovery(float amount) //�ɾ����� ���¹̳� ȸ��
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
        // ���� ���� ���� ������ �� �̸��� ���ٸ�
        if (scene.name == sceneToDestroy)
        {
            Destroy(gameObject); // ������Ʈ�� �ı��մϴ�.
        }
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // ������Ʈ�� �ı��� �� �̺�Ʈ �ڵ鷯�� �����մϴ�.
    }
}
