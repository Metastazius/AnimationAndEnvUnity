using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class angleCalculator : MonoBehaviour
{
    public csvReader csvreader;
    public GameObject csvReaderGameObject;
    private List<List<Vector3>> positionsInterest = new List<List<Vector3>>();
    public float angleRightElbow = 0f;
    public float angleRightShoulder = 0f;
    public float angleRightHip = 0f;
    public float angleRightKnee = 0f;
    public float angleLeftElbow = 0f;
    public float angleLeftShoulder = 0f;
    public float angleLeftHip = 0f;
    public List<List<Vector3>> positions = new List<List<Vector3>>();
    public float angleLeftKnee = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //csvreader = csvReaderGameObject.GetComponent<csvReader>();
        positions = csvreader.globalList;
    }

    // Update is called once per frame
    void Update()
    {
        positions = csvreader.globalList;

        Debug.Log(positions.Count());
        /* To reduce the size of the list to calculate the angles of interest we will create a new list containing only the points of interest:
         * 
         * 15,13,11,23,25,27 - right side of the body
         * 16,14,12,24,26,28 - left side of the body
         * 
         * The points in the new list will become, in order:
         *   15,13,11,23,25,27,16,14,12,24,26,28
         * => 0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11
         */

        int[] step = { 15, 13, 11, 23, 25, 27, 16, 14, 12, 24, 26, 28 };
        int j = 0;
        if (positions != null)
        {
            foreach (List<Vector3> list in positions)
            {
                List<Vector3> interest = new List<Vector3>();
                for (int i = 0; i < 12; i++)
                {
                    interest.Add(list[step[i]]);

                }
                positionsInterest.Add(interest);
            }
        }
        List<Vector3> cac = positionsInterest.First();
        Debug.Log(cac[0]);
        int currentFrame = Time.frameCount;
        Debug.Log(currentFrame+" "+positionsInterest.Count());
        List<Vector3> currentFrameList = positionsInterest[currentFrame-1];

        Vector3 rForearm = (currentFrameList[0] - currentFrameList[1]).normalized;
        Vector3 rArm = (currentFrameList[1] - currentFrameList[2]).normalized;
        Vector3 rTorso = (currentFrameList[2] - currentFrameList[3]).normalized;
        Vector3 rLeg = (currentFrameList[3] - currentFrameList[4]).normalized;
        Vector3 rCalf = (currentFrameList[4] - currentFrameList[5]).normalized;
        Vector3 lForearm = (currentFrameList[6] - currentFrameList[7]).normalized;
        Vector3 lArm = (currentFrameList[7] - currentFrameList[8]).normalized;
        Vector3 lTorso = (currentFrameList[8] - currentFrameList[9]).normalized;
        Vector3 lLeg = (currentFrameList[9] - currentFrameList[10]).normalized;
        Vector3 lCalf = (currentFrameList[10] - currentFrameList[11]).normalized;

        angleRightElbow = Vector3.Angle(rForearm,rArm);
        angleRightShoulder = Vector3.Angle(rArm,rTorso);
        angleRightHip = Vector3.Angle(rTorso,rLeg);
        angleRightKnee = Vector3.Angle(rLeg,rCalf);
        angleLeftElbow = Vector3.Angle(lForearm,lArm);
        angleLeftShoulder = Vector3.Angle(lArm,lTorso);
        angleLeftHip = Vector3.Angle(lTorso,lLeg);
        angleLeftKnee = Vector3.Angle(lLeg,lCalf);

        Debug.Log("Right Elbow Angle: " + angleRightElbow);
        Debug.Log("Right Shoulder Angle: " + angleRightShoulder);
        Debug.Log("Right Hip Angle: " + angleRightHip);
        Debug.Log("Right Knee Angle: " + angleRightKnee);
        Debug.Log("Left Elbow Angle: " + angleLeftElbow);
        Debug.Log("Left Shoulder Angle: " + angleLeftShoulder);
        Debug.Log("Left Hip Angle: " + angleLeftHip);
        Debug.Log("Left Knee Angle: " + angleLeftKnee);
    }
}
