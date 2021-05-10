using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draw : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    /// <summary>
    /// This is the Core Class that make the principal mechanic (Drawn and Create)
    /// </summary>
    [SerializeField]
    private Player player;

    [SerializeField]
    private Camera drawCamera;

    [SerializeField]
    private GameObject linePrefab;
    private GameObject drawingLine;

    [SerializeField] float reductionRatio = 0.3f;

    private List<Vector2> touchPoints = new List<Vector2>();

    public void OnPointerDown(PointerEventData eventData)
    {
        //Gets the mouse position in the range of the "drawCamera"
        Vector2 input = drawCamera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, drawCamera.transform.position.z * -1));
        CreateLine(input); //Call the CreateLine functions and pass the input on the argument
    }
    public void OnDrag(PointerEventData eventData)
    {
        //Every frame on the Drag function, pass the input to UpdateLine function
        Vector2 input = drawCamera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, drawCamera.transform.position.z * -1));
        UpdateLine(input);
    }
    public void OnPointerUp(PointerEventData eventData) 
    {
        //For fixing issues when the new wheels are created
        player.transform.position += new Vector3(0, .5f, 0);

        //Destroy the previous wheels
        GameObject.Destroy(player.RightLeg.GetChild(0).gameObject); 
        GameObject.Destroy(player.LeftLeg.GetChild(0).gameObject);  
        player.HingeJoint.transform.localPosition = Vector3.zero; //Make the Wheel gets the Vector3.zero position

        //Instantiate the drawingLine on the player.rightWheel transform position
        var wheel1 = Instantiate(drawingLine, Vector3.zero, Quaternion.identity);
        wheel1.layer = LayerMask.NameToLayer("Wheel"); //Set the wheel layer for wheel1 (RightWheel)
        
        //Down here make the pivot point on the position 0 of the line and adjust the scale of Line
        Transform pivot1 = new GameObject("Wheel_Right").transform; 
        pivot1.position = touchPoints[0];
        wheel1.transform.SetParent(pivot1);

        pivot1.SetParent(player.RightLeg);
        pivot1.localPosition = Vector3.zero;
        pivot1.localEulerAngles = Vector3.zero;
        pivot1.localScale *= reductionRatio;

        //Make the same but in the Left Wheel
        var wheel2 = Instantiate(drawingLine, Vector3.zero, Quaternion.identity);
        wheel2.layer = LayerMask.NameToLayer("Wheel"); //Set the wheel layer for wheel2 (LeftWheel)
        Transform pivot2 = new GameObject("Wheel_Left").transform; 
        pivot2.position = touchPoints[0];
        wheel2.transform.SetParent(pivot2);

        pivot2.SetParent(player.LeftLeg);
        pivot2.localPosition = Vector3.zero;
        pivot2.localEulerAngles = Vector3.zero + new Vector3(0, 0, 180);
        pivot2.localScale *= reductionRatio;
        GameObject.Destroy(drawingLine);

        //To make the player stop floating
        if (player.canFloat)
        {
            player.canFloat = false;
        }
    }
    public void CreateLine(Vector2 firstTouchPos)
    {
        //Clear the dragged line of the screen
        touchPoints.Clear();
        touchPoints.Add(firstTouchPos); //Add the firstTouchPos in [0]position on touchPoints list
        touchPoints.Add(firstTouchPos); //Add the firstTouchPos in [1]position on touchPoints list

        drawingLine = Instantiate(linePrefab); //Stores the linePrefab in the drawingLine object
        var wheelObject = drawingLine.GetComponent<Wheel>(); //Gets the Wheel Class component

        //Set the positions of the wheelObject
        wheelObject.LineRenderer.SetPosition(0, touchPoints[0]); 
        wheelObject.LineRenderer.SetPosition(1, touchPoints[1]);

        wheelObject.EdgeCollider.points = touchPoints.ToArray(); //Make the collider
    }
    public void UpdateLine(Vector2 newTouchPos)
    {
        //Checks if the distance of newTouchPos is great than previous newTouchPos, to make the line
        if (Vector2.Distance(newTouchPos, touchPoints[touchPoints.Count - 1]) > .2f)
        {
            //Add the newTouchPos to touchpoints list
            touchPoints.Add(newTouchPos);

            var wheelObject = drawingLine.GetComponent<Wheel>(); //Var to get the Wheel class of the drawingLine object

            //Create the positions of the line on the LineRenderer.SetPosition method
            wheelObject.LineRenderer.positionCount += 1;
            wheelObject.LineRenderer.SetPosition(wheelObject.LineRenderer.positionCount - 1, touchPoints[touchPoints.Count - 1]);

            //Make the collisions of line with the EdgeCollider component
            wheelObject.EdgeCollider.points = touchPoints.ToArray();
        }
    }
}
