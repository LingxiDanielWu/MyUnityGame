using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class focusTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float minZoomDis;
    public float maxZoomDis;
    public float maxAngleY = 80;
    public float minAngleY = 0;
    public float startDis = 5;
    public float startAngleY = 30;
    public float startAngleZ = 0;
    [SerializeField]
    [Range(0, 10)]
    public float rotateSpeed = 0;
    [SerializeField]
    [Range(0, 10)]
    public float zoomSpeed = 0;

    private float _curDis;
    private float _angleY;

    void Start()
    {
        transform.LookAt(target);

        float dis = Vector3.Distance(transform.position, target.transform.position);
        _curDis = dis;
        zoom(startDis - dis);

        Vector3 n = target.transform.position - transform.position;
        float curAngleY = Vector3.Angle(n, new Vector3(n.x, 0, n.z));
        _angleY = curAngleY;
        rotateVertical(startAngleY - curAngleY);

        float curAngleZ = Vector3.Angle(n, target.transform.forward);
        rotateHorizontal(startAngleZ - curAngleZ);
    }

    //deltaDis：摄像机在背向目标方向上的移动距离，大于0表示远离目标
    void zoom(float deltaDis)
    {
        if ((_curDis == maxZoomDis && deltaDis > 0) || (_curDis == minZoomDis && deltaDis < 0))
            return;

        Vector3 n = target.transform.position - transform.position;
        float newZoomDis = n.magnitude + deltaDis;
        if (newZoomDis < minZoomDis)
            newZoomDis = minZoomDis;
        if (newZoomDis > maxZoomDis)
            newZoomDis = maxZoomDis;

        Vector3 newPos = transform.position - n.normalized * (newZoomDis - n.magnitude);
        transform.position = newPos;
        _curDis = newZoomDis;
    }

    void rotate(float horizonAngle, float verticalAngle)
    {
        rotateHorizontal(horizonAngle);
        rotateVertical(verticalAngle);
    }

    void rotateVertical(float angle)
    {
        if ((_angleY == maxAngleY && angle > 0) || (_angleY == minAngleY && angle < 0))
            return;

        Vector3 dir = target.transform.position - transform.position;
        float newAngleY = _angleY + angle;
        if (newAngleY > maxAngleY)
            angle = maxAngleY - _angleY;
        if (newAngleY < minAngleY)
            angle = minAngleY - _angleY;
        transform.RotateAround(target.transform.position, transform.right, angle);
        _angleY = _angleY + angle;
    }

    void rotateHorizontal(float angle)
    {
        transform.RotateAround(target.transform.position, Vector3.up, angle);
    }

    void Update()
    {
        transform.LookAt(target);
    }

    private Vector2 _lastPressPos;
    public void OnPointerDown(BaseEventData eventData)
    {
        _lastPressPos = eventData.currentInputModule.input.mousePosition;
    }

    public void OnDrag(BaseEventData eventData)
    {
        Vector2 mousePos = eventData.currentInputModule.input.mousePosition;
        float horizonRotateAngle = (mousePos.x - _lastPressPos.x) * rotateSpeed;
        float verticalRotateAngle = (mousePos.y - _lastPressPos.y) * rotateSpeed;
        rotateHorizontal(horizonRotateAngle);
        rotateVertical(verticalRotateAngle);
        _lastPressPos = mousePos;
    }

    public void OnScroll(BaseEventData eventData)
    {
        float zoomDis = eventData.currentInputModule.input.mouseScrollDelta.y * zoomSpeed;
        zoom(-zoomDis);
    }
}
