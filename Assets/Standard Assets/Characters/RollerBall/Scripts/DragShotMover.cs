using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
 
public class DragShotMover : MonoBehaviour {
 
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject stretchLine;

    public float magBase = 2; // this is the base magnitude and the maximum length of the line drawn in the user interface
    public float magMultiplier = 5; // multiply the line length by this to allow for higher force values to be represented by shorter lines
    public Vector3 dragPlaneNormal = Vector3.up; // a vector describing the orientation of the drag plan relative to world-space but centered on the target
    public SnapDir snapDirection = SnapDir.away; // force is applied either toward or away from the mouse on release
    public ForceMode forceTypeToApply = ForceMode.VelocityChange;
 
    public bool  overrideVelocity = true; // cancel the existing velocity before applying the new force
    public bool  pauseOnDrag = true; // causes the simulation to pause when the object is clicked and unpause when released
 
    public enum SnapDir {toward, away}
 
    private Vector3 forceVector;
 
    private bool  mouseDragging = false;
    private Vector3 mousePos3D;
    private float dragDistance;
    private Plane dragPlane;
    private Ray mouseRay;

    void Stretch(GameObject _sprite,Vector3 _initialPosition, Vector3 _finalPosition, bool _mirrorZ) {
        _initialPosition.x = _sprite.transform.position.x;
        _finalPosition.x = _sprite.transform.position.x;
        Vector3 centerPos = (_initialPosition + _finalPosition) / 2f;
        _sprite.transform.position = centerPos;
        Vector3 direction = _finalPosition - _initialPosition;
        direction = Vector3.Normalize(direction);
        Vector3 newDirection = direction;
        Vector3 camPosition;
        camPosition.x = cam.transform.position.x;
        camPosition.y = centerPos.y;
        camPosition.z = centerPos.z;
        newDirection.y = -direction.z;
        newDirection.z = direction.y;
		_sprite.transform.LookAt(camPosition,newDirection);
        //Debug.Log(newDirection);
		//_sprite.transform.localEulerAngles = new Vector3(0,90,_sprite.transform.localEulerAngles.z);
        //if (_mirrorZ) _sprite.transform.right *= -1f;
        Vector3 scale = new Vector3(1,1,1);
        scale.x = Vector3.Distance(_initialPosition, _finalPosition);
        _sprite.transform.localScale = scale;
    }
 
    void  Start (){ 
        // create the dragplane
        dragPlane = new Plane(dragPlaneNormal, GetComponent<Rigidbody>().transform.position);
        stretchLine.GetComponent<Renderer>().enabled = false;
 
    }
 
    void  OnMouseDown (){
        mouseDragging = true;
        stretchLine.GetComponent<Renderer>().enabled = true;

        if (pauseOnDrag) {
            // pause the simulation
            Time.timeScale = 0;
        }
        // update the dragplane
        dragPlane = new Plane(dragPlaneNormal, GetComponent<Rigidbody>().transform.position);

    }
 
   
 
    void  OnMouseDrag (){

        Vector3 pos = Input.mousePosition;
        pos.z = 0;
        pos = cam.ScreenToWorldPoint(pos);
        pos.x = GetComponent<Rigidbody>().position.x;

        // update the plane if the target object has left it
        if (dragPlane.GetDistanceToPoint(transform.position) != 0) {
            // update dragplane by constructing a new one -- I should check this with a profiler
            dragPlane = new Plane(dragPlaneNormal, GetComponent<Rigidbody>().transform.position);
        }
 
        // create a ray from the camera, through the mouse position in 3D space
        mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        mouseRay.origin = pos;
        //Debug.Log(GetComponent<Rigidbody>().transform.position);
 
        // update the world space point for the mouse position on the dragPlane
        mousePos3D = pos;

        // calculate the distance between the 3d mouse position and the object position
        dragDistance = Mathf.Clamp((mousePos3D - transform.position).magnitude, 0, magBase);

        // calculate the force vector
        if (dragDistance*magMultiplier < 1) dragDistance = 0; // this is to allow for a "no move" buffer close to the object
        forceVector = mousePos3D - transform.position;
        forceVector.Normalize();
        forceVector *= dragDistance * magMultiplier;

        Stretch(stretchLine,GetComponent<Rigidbody>().position,pos,true);
    }
 
    void  OnMouseUp (){
        mouseDragging = false;
        stretchLine.GetComponent<Renderer>().enabled = false;
 
        if (overrideVelocity) {
            // cancel existing velocity
            GetComponent<Rigidbody>().AddForce(-GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);
 
        }
 
        // add new force
        int snapD = 1;
        if (snapDirection == SnapDir.away) snapD = -1; // if snapdirection is "away" set the force to apply in the opposite direction
        GetComponent<Rigidbody>().AddForce(snapD * forceVector, forceTypeToApply);

 
        if (pauseOnDrag) {
            // un-pause the simulation
            Time.timeScale = 1;
        }
 
    }
 
    void  OnGUI (){
        if (mouseDragging) {
            Vector2 guiMouseCoord = GUIUtility.ScreenToGUIPoint(Input.mousePosition);
            GUI.Box ( new Rect(guiMouseCoord.x-30, Screen.height-guiMouseCoord.y+15, 100, 20), "force: "+Mathf.Round((forceVector).magnitude));
        }
    }
}