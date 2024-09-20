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
    static public Player Instance; //��Ŭ��

    
    public GameManager manager;
    [Header("�÷��̾��� ���� �� ��ġ�� �˷��ִ� ���")]
    public string currentMapName; //transferMap ��ũ��Ʈ�� �ִ� transferMapName ������ ���� ����.

    [Header("�÷��̾� �̵� ���� ���")]
    public float runSpeed;
    public float walkSpeed;
    public float currentSpeed;
    public float jumpPower;

    public LayerMask platformLayer; // �÷����� �ִ� ���̾ �����մϴ�.
    public float checkDistance = 0.1f; // üũ�� �Ÿ�
    public float checkWidth = 0.5f; // ����ĳ��Ʈ �߻� ����
    public Color rayColorAbove = Color.green; // ���� ���� ����
    public Color rayColorBelow = Color.red; // �Ʒ��� ���� ����
    //��ɵ� �־��ֱ� ������ٵ�(����), ��������Ʈ������(�÷��̾� ȸ��),�ִϸ�����(�÷��̾� �����϶� ���)
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

        // �� �Ʒ������� ����ĳ��Ʈ
        RaycastHit2D belowHit = Physics2D.Raycast(position, Vector2.down, checkDistance, platformLayer);

        // �� �Ʒ����� ���� �ð�ȭ
        Debug.DrawRay(position, Vector2.down * checkDistance, rayColorBelow);

        // �� �Ʒ��� �÷����� ������ isTrigger ��Ȱ��ȭ
        if (belowHit.collider != null)
        {
            belowHit.collider.isTrigger = false;
        }

        // �� ���� �������� ���� ���� ����ĳ��Ʈ �߻�
        for (float x = -checkWidth; x <= checkWidth; x += checkWidth)
        {
            Vector2 origin = position + new Vector2(x, 0);
            RaycastHit2D aboveHit = Physics2D.Raycast(origin, Vector2.up, checkDistance, platformLayer);

            // �� ������ ���� �ð�ȭ
            Debug.DrawRay(origin, Vector2.up * checkDistance, rayColorAbove);

            // �� ���� �÷����� ������ isTrigger Ȱ��ȭ
            if (aboveHit.collider != null)
            {
                aboveHit.collider.isTrigger = true;
            }
        }

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
                soundManager.PlaySound(3);
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

        //����ĳ��Ʈ ���� ���̰� �ϱ�
        if (rigid.velocity.y < 0)
        {
            //���� ĳ��Ʈ�� ����Ͽ� �������� ����
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
        
    void Movement() //���� ������
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
            if (Input.GetKey(KeyCode.LeftShift)) //�޸���
            {
                soundManager.StopSound(0);
                soundManager.PlaySound(1);

                currentSpeed = runSpeed;
                if(InputX != 0)
                    DeRunStamina(Hud.Instance.runStamina * Time.deltaTime); //���¹̳� ����


            }
            else
            {
                soundManager.StopSound(1);
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
}
