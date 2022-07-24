using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    
    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        // Don't use the string version of the method.
        if (Input.GetKey(KeyCode.Space)) 
        {
            startThrusting();
        } 
        else 
        {
            stopThrusting();
        }  
    }

    void ProcessRotation() 
    {      
        if (Input.GetKey(KeyCode.A)) 
        {
            rotateRight();
        }
        else if (Input.GetKey(KeyCode.D)) 
        {
            rotateLeft();
        }
        else 
        {
            stopRotation();
        }
    }

    void applyRotation(float rotateFrame) 
    {
        //  we need to disable the phyiscs system so that bumping to a an object doesn't effect our rotation system. So that we can manually rotate.
        //  Solution starts here 
        rb.freezeRotation = true; 
        //  Solution ends here
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateFrame);
        rb.freezeRotation = false; // unfreeze rotation after the rotation  
    }

    void startThrusting() 
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying) 
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!mainThrusterParticles.isPlaying)
            {
                mainThrusterParticles.Play();
            }
    }

    void stopThrusting()
    {
        audioSource.Stop();
        mainThrusterParticles.Stop();
    }

    void rotateRight() 
    {
        applyRotation(rotationThrust);
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    void rotateLeft()
    {
        applyRotation(-rotationThrust);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    void stopRotation()
    {
        rightThrusterParticles.Play();
        leftThrusterParticles.Play();
    }
}