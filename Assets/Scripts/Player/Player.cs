using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// This script serves to other other classes get the player components
    /// and make player "floating" when didn't starts the game
    /// </summary>

    [SerializeField] private Transform righLeg;
    [SerializeField] private Transform leftLeg;

    [SerializeField] private HingeJoint2D joint;

    public Transform RightLeg { get { return righLeg; } }
    public Transform LeftLeg { get { return leftLeg; } }

    public HingeJoint2D HingeJoint { get { return joint; } }

    /// <summary>
    /// Down here is the code that makes the player float
    /// i don't know if this is the best way to make this, but in my case it served well
    /// </summary>
    [SerializeField] private float floatStrenght;
    bool floatUp;
    bool doOnce;
    public bool canFloat;

    Vector3 targetPos = new Vector3();

    void Start()
    {
        targetPos = transform.position;
        floatUp = true;
        doOnce = true;
        canFloat = true;
    }

    void Update()
    {
        //When canFloat gonna false, the float effect turns off
        if (canFloat)
        {
            Floating();
            HingeJoint.useMotor = false;
        }
        else
        {
            HingeJoint.useMotor = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("finish"))
        {
            GameManager.Instance.RestarWindowOn();
            Time.timeScale = 0f;
        }
    }

    public void Floating()
    {
        //floatUp make the targetPos up/down
        if (floatUp)
        {
            targetPos.y += floatStrenght * Time.deltaTime;
            if (doOnce)
            {
                StartCoroutine(FloatController());
                doOnce = false;
            }
        }
        else if (!floatUp)
        {
            targetPos.y -= floatStrenght * Time.deltaTime;
            if (doOnce)
            {
                StartCoroutine(FloatController());
                doOnce = false;
            }
        }

        transform.position = targetPos;
    }

    IEnumerator FloatController()
    {
        yield return new WaitForSeconds(.4f);
        if (floatUp)
        {
            floatUp = false;
        }
        else if (!floatUp)
        {
            floatUp = true;
        }
        doOnce = true;
    }
}
