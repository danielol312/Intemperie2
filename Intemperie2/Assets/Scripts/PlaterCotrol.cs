using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

public class PlaterCotrol : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    public float rotationSpeed = 4f;
    [SerializeField]
    private InputActionReference movementControl;
    [SerializeField]
    private InputActionReference jumpControl;
    [SerializeField]
    public float playerSpeed = 2.0f;
    [SerializeField]
    public float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    public GameObject Smoke;
    [SerializeField]
    public GameObject Heal;

    [SerializeField]
    

    public Image malestarBar;
    public float malestar, maxMalestar;

    [Header("Animations")]
    [SerializeField]
    private Animator animator;

    [Header("Perder Control")]
    public Transform target;
    public float rotateSpeed = 30f;

    //[Header("Dash Variables")]
    //[SerializeField] bool canDash=false;
    //[SerializeField] bool isDashing;
    //[SerializeField] float dashPower;
    //[SerializeField] float dashTime;
    //[SerializeField] float dashCoolDown;
    //private TrailRenderer dashTrail;
    //[SerializeField] float dashGravity;
    //private float normalGravity;
    //private float waitTime;


    private bool controlPerdido = false;
    public GameObject controlPerdidoUI;

    public float radio;
    public float velocidadRotacion =2f;

    public float attackCost=10f;
    public float costoQuieto;
    public float cantidadRecarga;

    private Coroutine recarga;

    public CharacterController controller;
    public Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraMainTransform;

    public Blackhole blackHole;
    

    float dashTimer = 0.15f;
    float initialDashTimer;
    bool canDash = true;
    bool isDashing;
    float dashCoolDown = 2f;


    private void OnEnable()
    {
        movementControl.action.Enable();
        jumpControl.action.Enable();
    }
    private void OnDisable()
    {
        movementControl.action.Disable();
        jumpControl.action.Disable();
    }
    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }
    private void Start()
    {
        initialDashTimer = dashTimer;
        cameraMainTransform = Camera.main.transform;
    }

    void Update()
    {

        if (malestar <= 0f && !controlPerdido && malestarBar != null)
        {
            //controlPerdido = true;
            MoverAleatorio1();
            Debug.Log("Control perdido , muévete frente a la cámara");
            controlPerdidoUI.gameObject.SetActive(true);

        }
        if (!controlPerdido) { 

            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
                animator.SetBool("IsJumping", false);
            }
            
            Vector2 movement = movementControl.action.ReadValue<Vector2>();
          
            Vector3 move = new Vector3(movement.x, 0, movement.y );
            move = cameraMainTransform.forward*move.z+cameraMainTransform.right*move.x ;
            if(blackHole != null) move += new Vector3(blackHole.AddedForce.normalized.x, 0f, blackHole.AddedForce.normalized.z)*0.6f;
            //move.y = 0f;

            //Dashing
            if (Input.GetKeyDown(KeyCode.Q) && !isDashing && canDash)
            {
                isDashing = true;
            }
            if (isDashing)
            {
                isDashing = true;
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    isDashing = false;
                    dashTimer = initialDashTimer;
                    canDash = false;
                    StartCoroutine(DashCoolDown());
                }
                Debug.Log("Dashed");
                move += Time.deltaTime * cameraMainTransform.forward * 350f;
            }
            controller.Move(move * Time.deltaTime * playerSpeed);
            if (movement.magnitude == 0f)
            {
                animator.SetBool("IsRunning", false);
            }
            else
            {
                animator.SetBool("IsRunning", true);
            }
           
            


            // Changes the height position of the player..
            if (jumpControl.action.triggered && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                animator.SetBool("IsJumping", true);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            

            if (movement != Vector2.zero)
            {
                float targerAngle= Mathf.Atan2(movement.x,movement.y)* Mathf.Rad2Deg+ cameraMainTransform.eulerAngles.y;
                Quaternion rotation= Quaternion.Euler(0f, targerAngle, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation,rotation,Time.deltaTime * rotationSpeed);
                animator.SetBool("IsRunning", true);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Attack");
                malestar += attackCost ;

                //if (malestar < 0f) malestar = 0f;

                malestarBar.fillAmount = malestar / maxMalestar;
                movement = Vector2.zero;
                Heal.gameObject.SetActive(true);
            }

            if (movement == Vector2.zero)
            {
                malestar-=costoQuieto*Time.deltaTime;
                if(malestar < 0f) malestar= 0f;
                if (malestarBar != null) malestarBar.fillAmount = malestar / maxMalestar;
                if (recarga != null)StopCoroutine(recarga);
                //recarga = StartCoroutine(recargaMalestar());
                
            }
            //Smoke
            if (malestarBar != null)
            {
                if (malestar <= 25)
                {
                    Smoke.gameObject.SetActive(true);
                }
                if (malestar >= 26)
                {
                    Smoke.gameObject.SetActive(false);
                }
            }
        }


    }
    private IEnumerator DashCoolDown()
    {
        float timer = 0;
        while (timer< dashCoolDown)
        {
            Debug.Log("timer" + timer);
            timer += Time.deltaTime;
            yield return null;  
        }
        canDash = true;
        yield break;
    }
    private IEnumerator recargaMalestar()
    {
        yield return new WaitForSeconds(1f);

        if (malestarBar != null)
        {
            while (malestar < maxMalestar)
            {

                malestar += cantidadRecarga / 10f;

                if (malestar > maxMalestar) malestar = maxMalestar;
                malestarBar.fillAmount = malestar / maxMalestar;

                yield return new WaitForSeconds(.1f);
            }
        }
        else
        {
            yield return null;
        }
    }

    private void MoverAleatorio1()
    {
        if (malestarBar != null)
        {
            transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.up * 70f);
        }
    }

    //private IEnumerator MoverAleatorio()
    //{
    //    yield return new WaitForSeconds(1f);

    //    // Calcular la posición en el círculo
    //    float angulo = Time.time * velocidadRotacion;
    //    float x = Mathf.Cos(angulo) * radio;
    //    float z = Mathf.Sin(angulo) * radio;

    //    // Actualizar la posición del personaje
    //    transform.position = new Vector3(x, 0, z);
    //}

   
}
