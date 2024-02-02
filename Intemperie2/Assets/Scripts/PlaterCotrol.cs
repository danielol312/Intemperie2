using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

public class PlaterCotrol : MonoBehaviour
{

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

    public Image malestarBar;
    public float malestar, maxMalestar;

    [Header("Animations")]
    [SerializeField]
    private Animator animator;


    private bool controlPerdido = false;

    public float radio;
    public float velocidadRotacion =2f;

    public float attackCost;
    public float costoQuieto;
    public float cantidadRecarga;

    private Coroutine recarga;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraMainTransform;

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

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        cameraMainTransform = Camera.main.transform;
    }

    void Update()
    {

        if (malestar <= 0f && !controlPerdido)
        {
            controlPerdido = true;
            StartCoroutine(MoverAleatorio());
            Debug.Log("Control perdido , muévete frente a la cámara");
        }
        if (!controlPerdido) { 

            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
            Vector2 movement = movementControl.action.ReadValue<Vector2>();
            Vector3 move = new Vector3(movement.x, 0, movement.y );

            move = cameraMainTransform.forward*move.z+cameraMainTransform.right*move.x;
            move.y = 0f;
            controller.Move(move * Time.deltaTime * playerSpeed);

            animator.SetBool("IsRunning", true);


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

            if (Input.GetKeyDown("f"))
            {
                Debug.Log("Attack");
                malestar -= attackCost;

                if (malestar < 0f) malestar = 0f;

                malestarBar.fillAmount = malestar / maxMalestar;
            }

            if (movement == Vector2.zero)
            {
                malestar-=costoQuieto*Time.deltaTime;
                if(malestar < 0f) malestar= 0f;
                malestarBar.fillAmount=malestar / maxMalestar;

                if (recarga != null)StopCoroutine(recarga);
                recarga = StartCoroutine(recargaMalestar());
            }
        }


    }

    private IEnumerator recargaMalestar()
    {
        yield return new WaitForSeconds(1f);

        while (malestar < maxMalestar)
        {
            malestar += cantidadRecarga / 10f;

            if (malestar > maxMalestar) malestar=maxMalestar;
            malestarBar.fillAmount= malestar / maxMalestar;

            yield return new WaitForSeconds(.1f);
        }
    }

   

    private IEnumerator MoverAleatorio()
    {
        yield return new WaitForSeconds(1f);

        // Calcular la posición en el círculo
        float angulo = Time.time * velocidadRotacion;
        float x = Mathf.Cos(angulo) * radio;
        float z = Mathf.Sin(angulo) * radio;

        // Actualizar la posición del personaje
        transform.position = new Vector3(x, 0, z);
    }

   
}
