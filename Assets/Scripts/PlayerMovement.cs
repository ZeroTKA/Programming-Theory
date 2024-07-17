using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float speed = 12f;
    [SerializeField] float gravity = -20f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float groundDistance = .4f;
    [SerializeField] float ceilingDistance = .2f;

    [SerializeField] CharacterController controller;

    [SerializeField] LayerMask groundMask;

    [SerializeField] Transform groundCheck;
    [SerializeField] Transform ceilingCheck;

    [SerializeField] TextMeshProUGUI _readyUp;

    Vector3 velocity;
    bool isGrounded;
    bool isHittingCeiling;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        MoveCharacter();
        Jump();
        ApplyGravity();
        ReadyUp();
    }
    void MoveCharacter()
    {
        //moves characetr
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }
    void Jump()
    {
        //jumps
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //checks if the player hits the ceiling
        if (!isGrounded && velocity.y > 0)
        {
            isHittingCeiling = Physics.CheckSphere(ceilingCheck.position, ceilingDistance, groundMask);
            if (isHittingCeiling) { velocity.y = 0; }
        }
    }
    void ApplyGravity()
    {
        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void ReadyUp()
    {
        if (Input.GetKeyDown(KeyCode.G) && TheDirector.instance.State == TheDirector.GameState.Player)
        {
            if (WaveManager.instance.CheckIfIsLastWave())
            {
                StartCoroutine(LastWave());
            }
            else
            {
                _readyUp.transform.gameObject.SetActive(false);
                TheDirector.instance.UpdateGameState(TheDirector.GameState.Wave);
            }
        }
    }
    IEnumerator LastWave()
    {
        SoundManager.instance.PlaySoundFXClip(SoundManager.instance.lastWaveAudio,transform, .3f);
        yield return new WaitForSeconds(SoundManager.instance.lastWaveAudio.length);
        _readyUp.transform.gameObject.SetActive(false);
        TheDirector.instance.UpdateGameState(TheDirector.GameState.Wave);
    }
}
