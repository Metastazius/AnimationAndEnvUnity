using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

public class MediapipeRTStream : MonoBehaviour
{
    NamedPipeServerStream server;
    // Start is called before the first frame update
    public float angleRightElbow = 0f;
    public float angleRightShoulder = 0f;
    public float angleRightHip = 0f;
    public float angleRightKnee = 0f;
    public float angleLeftElbow = 0f;
    public float angleLeftShoulder = 0f;
    public float angleLeftHip = 0f;
    public float angleLeftKnee = 0f;
   
    const int LINES_COUNT = 11;
    void Start()
    {
        Thread t = new Thread(new ThreadStart(Run));
        t.Start();
    }
    // Update is called once per frame
    void Update()
    {  
    
    }
    void Run()
    {
        // Open the named pipe.
        server = new NamedPipeServerStream("AnimationAndEnvUnity", PipeDirection.InOut, 99, PipeTransmissionMode.Message);

        print("Waiting for connection...");
        server.WaitForConnection();

        print("Connected.");
        var br = new BinaryReader(server);
        while (true)
        {
            try
            {
                
                var len = (int)br.ReadUInt32();
                var str = new string(br.ReadChars(len));

                string[] lines = str.Split('\n');
                List<Vector3> frameInterest = new List<Vector3>(12);
                foreach (string l in lines)
                {
                    if (string.IsNullOrWhiteSpace(l))
                        continue;
                    string[] s = l.Split('|');
                    if (s.Length < 4) continue;
                    float x = float.Parse(s[1]);
                    float y = float.Parse(s[2]);
                    float z = float.Parse(s[3]);
                    //15, 13, 11, 23, 25, 27, 16, 14, 12, 24, 26, 28
                    switch (s[0])
                    {
                        case "15":
                            
                            frameInterest[0] = new Vector3(x, y, z);
                            break;
                        case "13":
                            
                            frameInterest[1] = new Vector3(x, y, z);
                            break;
                        case "11":
                            
                            frameInterest[2] = new Vector3(x, y, z);
                            break;
                        case "23":
                            
                            frameInterest[3] = new Vector3(x, y, z);
                            break;
                        case "25":
                            
                            frameInterest[4] = new Vector3(x, y, z);
                            break;
                        case "27":
                            
                            frameInterest[5] = new Vector3(x, y, z);
                            break;
                        case "16":
                            
                            frameInterest[6] = new Vector3(x, y, z);
                            break;
                        case "14":
                            
                            frameInterest[7] = new Vector3(x, y, z);
                            break;
                        case "12":
                            
                            frameInterest[8] = new Vector3(x, y, z);
                            break;
                        case "24":
                            
                            frameInterest[9] = new Vector3(x, y, z);
                            break;
                        case "26":
                            
                            frameInterest[10] = new Vector3(x, y, z);
                            break;
                        case "28":
                            
                            frameInterest[11] = new Vector3(x, y, z);
                            break;
                    }
                    //Debug.Log(l); 
                    Vector3 rForearm = (frameInterest[0] - frameInterest[1]).normalized;
                    Vector3 rArm = (frameInterest[1] - frameInterest[2]).normalized;
                    Vector3 rTorso = (frameInterest[2] - frameInterest[3]).normalized;
                    Vector3 rLeg = (frameInterest[3] - frameInterest[4]).normalized;
                    Vector3 rCalf = (frameInterest[4] - frameInterest[5]).normalized;
                    Vector3 lForearm = (frameInterest[6] - frameInterest[7]).normalized;
                    Vector3 lArm = (frameInterest[7] - frameInterest[8]).normalized;
                    Vector3 lTorso = (frameInterest[8] - frameInterest[9]).normalized;
                    Vector3 lLeg = (frameInterest[9] - frameInterest[10]).normalized;
                    Vector3 lCalf = (frameInterest[10] - frameInterest[11]).normalized;
                   
                    angleRightElbow = Vector3.Angle(rForearm, rArm);
                    angleRightShoulder = Vector3.Angle(rArm, rTorso);
                    angleRightHip = Vector3.Angle(rTorso, rLeg);
                    angleRightKnee = Vector3.Angle(rLeg, rCalf);
                    angleLeftElbow = Vector3.Angle(lForearm, lArm);
                    angleLeftShoulder = Vector3.Angle(lArm, lTorso);
                    angleLeftHip = Vector3.Angle(lTorso, lLeg);
                    angleLeftKnee = Vector3.Angle(lLeg, lCalf);
                    
                    Debug.Log("Right Elbow Angle in real-time stream: " + angleRightElbow);
                    Debug.Log("Right Shoulder Angle in real-time stream: " + angleRightShoulder);
                    Debug.Log("Right Hip Angle in real-time stream: " + angleRightHip);
                    Debug.Log("Right Knee Angle in real-time stream: " + angleRightKnee);
                    Debug.Log("Left Elbow Angle in real-time stream: " + angleLeftElbow);
                    Debug.Log("Left Shoulder Angle in real-time stream: " + angleLeftShoulder);
                    Debug.Log("Left Hip Angle in real-time stream: " + angleLeftHip);
                    Debug.Log("Left Knee Angle in real-time stream: " + angleLeftKnee);
                }
                
            }
            catch (EndOfStreamException)
            {
                break;                    // When client disconnects
            }
        }

    }

    private void OnDisable()
    {
        print("Client disconnected.");
        server.Close();
        server.Dispose();
    }
}
