using System.Collections.Generic;
using UnityEngine;
//using NDream.AirConsole;
//using Newtonsoft.Json.Linq;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerController instance;
    public bool right, left;
    public Hand leftHandRef, rightHandRef;
    public float scrollSpeed = -1.5f;
    public List<int> remoteUsers;
    public State state;
    public int level = 1;
    private void Awake()
    {
        //AirConsole.instance.onMessage += OnMessage;
        //AirConsole.instance.onConnect += OnConnect;
        //AirConsole.instance.onDisconnect += OnDisconnect;
        state.currentLevel = level;
    }
    private void OnConnect(int device_id)
    {
        //Log to on-screen Console
        remoteUsers.Add(device_id);
        sendMessageToDevices();
        

    }

    void OnDisconnect(int device_id)
    {
        remoteUsers.Remove(device_id);
        sendMessageToDevices();
    }

    public void SetGrabbed(bool isLeft)
    {
        if (isLeft)
        {
            leftHandRef.isGrabbed = true;
            if(rightHandRef.transform.position.y < leftHandRef.transform.position.y)
            {
                GetComponent<CameraController>().newYPos = leftHandRef.transform.position.y + 2f;
            }
        }
        else
        {
            rightHandRef.isGrabbed = true;
            if (rightHandRef.transform.position.y > leftHandRef.transform.position.y)
            {
                GetComponent<CameraController>().newYPos = rightHandRef.transform.position.y + 2f;
            }
        }
    }

    private void OnMessage(int DeviceID, string data)
    {
        leftHandRef.isRaised |= (string)data == "leftHand";
        rightHandRef.isRaised |= (string)data == "rightHand";


        if ((string)data == "leftHandEnd")
        {
            leftHandRef.isRaised = false;
            leftHandRef.BroadcastMessage("HandEnd");
        }
        if ((string)data == "rightHandEnd")
        {
            rightHandRef.isRaised = false;
            rightHandRef.BroadcastMessage("HandEnd");
        }

        left |= (string)data == "leftBody";
        right |= (string)data == "rightBody";
        left &= (string)data != "leftBodyEnd";
        right &= (string)data != "rightBodyEnd";
    }

    void Start()
    {

        leftHandRef.isGrabbed = true;
        rightHandRef.isGrabbed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("quit");

            Application.Quit();
        }

        if (remoteUsers.Count == 0)
        {
        
        right = left = leftHandRef.isRaised = rightHandRef.isRaised = false;
            left |= Input.GetKey("a");
            right |= Input.GetKey("d");
            leftHandRef.isRaised |= Input.GetKey("j");
            rightHandRef.isRaised |= Input.GetKey("l");

            if (Input.GetKeyUp("j"))
            {
                leftHandRef.BroadcastMessage("HandEnd");
            }
            if (Input.GetKeyUp("l"))
            {
                rightHandRef.BroadcastMessage("HandEnd");
            }
        }
    }
    private void OnDestroy()
    {
        //if (AirConsole.instance != null)
        //{
        //    AirConsole.instance.onMessage -= OnMessage;
        //    AirConsole.instance.onConnect -= OnConnect;
        //    AirConsole.instance.onDisconnect -= OnDisconnect;

        //}
    }
    private void sendMessageToDevices()
    {
        //List<int> ids = AirConsole.instance.GetControllerDeviceIds();
        //foreach(int id in ids)
        //{
        //    Dictionary<string, int> obj = new Dictionary<string, int>();
        //    obj.Add("index", ids.IndexOf(id));
        //    obj.Add("count", ids.Count);
        //    AirConsole.instance.Message(id, Newtonsoft.Json.JsonConvert.SerializeObject(obj));
        //}
       
    }
}
