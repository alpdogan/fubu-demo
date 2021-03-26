using Dweiss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;
    VariableJoystick variableJoystick;
    public GameObject bombPrefab;
    public float throwForce;
    public LineRenderer _lr;
    public Rigidbody referansRb;
    public float timeBeteenStep = 1;
    public int stepCount = 30;
    Rigidbody rb;
    public GameObject rangeSprite;
    public GameObject shootCam;
    public GameObject walkCam;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        variableJoystick = FindObjectOfType<VariableJoystick>();
    }

    public void Update()
    {
        if (isHoldingJoystick)
            DrawMovementLine();
        else
        {
            Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
            //  transform.Translate(direction * speed * Time.deltaTime, Space.World);
            rb.velocity = direction * speed;
            transform.LookAt(transform.position + direction);
        }
    }





    public GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    bool isHoldingJoystick;
    public void onJoystickDown()
    {
        m_PointerEventData = new PointerEventData(null);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.name == "BombButton")
            {
            //    shootCam.SetActive(false);
             //   shootCam.SetActive(true);
                rangeSprite.SetActive(true);
                isHoldingJoystick = true;
            }
        }
    }
    public void onJoystickUp()
    {
        if (isHoldingJoystick)
        {
            rangeSprite.SetActive(false);
           // walkCam.SetActive(false);
            //walkCam.SetActive(true);
            isHoldingJoystick = false;
            _lr.positionCount = 0;
            GameObject g = Instantiate(bombPrefab, referansRb.transform.position, Quaternion.identity);
            g.GetComponent<Rigidbody>().AddForce((Vector3.up + direction) * throwForce);
        }
    }
    Vector3 direction;
    private void DrawMovementLine()
    {
        direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        /*  if (variableJoystick.Vertical > 0)
          {
              throwForce += 1;
          }
          else if (variableJoystick.Vertical < 0)
          {
              throwForce -= 1;
          }*/
       
        var res = referansRb.CalculateMovement(stepCount, timeBeteenStep, Vector3.zero, (Vector3.up + direction) * throwForce);

        _lr.positionCount = stepCount + 1;
        _lr.SetPosition(0, referansRb.transform.position);
        for (int i = 0; i < res.Length; ++i)
        {
            _lr.SetPosition(i + 1, res[i]);
        }
        rangeSprite.transform.position = res[res.Length - 1];
        transform.LookAt(rangeSprite.transform.position);
    }

    private void OnDrawGizmos()
    {
        // Gizmos.DrawWireSphere(transform.position, lootRange);
    }
}
