using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerPortal : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private float offset = 1.5f; // How far to move player to the other side
    public bool flipGravity = false; // Should we flip gravity when exiting?
    public enum PortalDirection { Up, Down, Left, Right }
    public PortalDirection teleportDirection;
    [SerializeField] private bool isBlack;
    [SerializeField] private AudioClip portalSound;
    private AudioSource audioSource;

    private void Start()
    {
        //setting the animation color for the portal 
        animator.SetBool("isBlack", isBlack);

        audioSource = GetComponent<AudioSource>();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        audioSource.PlayOneShot(portalSound);

        Vector3 direction;
        switch (teleportDirection)
        {
            case PortalDirection.Up:
                direction = Vector3.up;
                break;
            case PortalDirection.Down:
                direction = Vector3.down;
                break;
            case PortalDirection.Left:
                direction = Vector3.left;
                break;
            case PortalDirection.Right:
                direction = Vector3.right;
                break;
            default:
                direction = Vector3.zero;
                break;
        }

        Vector3 newPos = other.transform.position + direction * offset;
        other.transform.position = newPos;

        // Optional: Flip gravity
        if (flipGravity)
        {
            other.GetComponent<CharacterScript>().FlipGravity();
        }
    }
}
