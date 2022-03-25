using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mediapipe.Unity.HandTracking
{
  public static class SessionManager
  {
    private enum HandJoint
    {
      WRIST = 0,
      THUMB_CMC = 1,
      THUMB_MCP = 2,
      THUMB_IP = 3,
      THUMB_TIP = 4,
      INDEX_FINGER_MCP = 5,
      INDEX_FINGER_PIP = 6,
      INDEX_FINGER_DIP = 7,
      INDEX_FINGER_TIP = 8,
      MIDDLE_FINGER_MCP = 9,
      MIDDLE_FINGER_PIP = 10,
      MIDDLE_FINGER_DIP = 11,
      MIDDLE_FINGER_TIP = 12,
      RING_FINGER_MCP = 13,
      RING_FINGER_PIP = 14,
      RING_FINGER_DIP = 15,
      RING_FINGER_TIP = 16,
      PINKY_MCP = 17,
      PINKY_PIP = 18,
      PINKY_DIP = 19,
      PINKY_TIP = 20
    }

    private static int[] jointList = {
        (int)HandJoint.WRIST,
        (int)HandJoint.THUMB_CMC,
        (int)HandJoint.THUMB_MCP,
        (int)HandJoint.THUMB_IP,
        (int)HandJoint.INDEX_FINGER_MCP,
        (int)HandJoint.INDEX_FINGER_PIP,
        (int)HandJoint.INDEX_FINGER_DIP,
        (int)HandJoint.MIDDLE_FINGER_MCP,
        (int)HandJoint.MIDDLE_FINGER_PIP,
        (int)HandJoint.MIDDLE_FINGER_DIP,
        (int)HandJoint.RING_FINGER_MCP,
        (int)HandJoint.RING_FINGER_PIP,
        (int)HandJoint.RING_FINGER_DIP,
        (int)HandJoint.PINKY_MCP,
        (int)HandJoint.PINKY_PIP,
        (int)HandJoint.PINKY_DIP
    };

    public static string ConvertPayload(NormalizedLandmarkList landmarks)
    {
      Dictionary<int, Vector3> rotations = new Dictionary<int, Vector3>();

      Vector3 wrist = GetPoint(landmarks, HandJoint.WRIST);
      Vector3 thumbRoot = GetPoint(landmarks, HandJoint.THUMB_CMC);
      Vector3 thumbMid = GetPoint(landmarks, HandJoint.THUMB_MCP);
      Vector3 thumbTip = GetPoint(landmarks, HandJoint.THUMB_IP);
      Vector3 thumbTipEnd = GetPoint(landmarks, HandJoint.THUMB_TIP);
      Vector3 indexRoot = GetPoint(landmarks, HandJoint.INDEX_FINGER_MCP);
      Vector3 indexMid = GetPoint(landmarks, HandJoint.INDEX_FINGER_PIP);
      Vector3 indexTip = GetPoint(landmarks, HandJoint.INDEX_FINGER_DIP);
      Vector3 indexTipEnd = GetPoint(landmarks, HandJoint.INDEX_FINGER_TIP);
      Vector3 middleRoot = GetPoint(landmarks, HandJoint.MIDDLE_FINGER_MCP);
      Vector3 middleMid = GetPoint(landmarks, HandJoint.MIDDLE_FINGER_PIP);
      Vector3 middleTip = GetPoint(landmarks, HandJoint.MIDDLE_FINGER_DIP);
      Vector3 middleTipEnd = GetPoint(landmarks, HandJoint.MIDDLE_FINGER_TIP);
      Vector3 ringRoot = GetPoint(landmarks, HandJoint.RING_FINGER_MCP);
      Vector3 ringMid = GetPoint(landmarks, HandJoint.RING_FINGER_PIP);
      Vector3 ringTip = GetPoint(landmarks, HandJoint.RING_FINGER_DIP);
      Vector3 ringTipEnd = GetPoint(landmarks, HandJoint.RING_FINGER_TIP);
      Vector3 pinkyRoot = GetPoint(landmarks, HandJoint.PINKY_MCP);
      Vector3 pinkyMid = GetPoint(landmarks, HandJoint.PINKY_PIP);
      Vector3 pinkyTip = GetPoint(landmarks, HandJoint.PINKY_DIP);
      Vector3 pinkyTipEnd = GetPoint(landmarks, HandJoint.PINKY_TIP);

      Vector3 rotation;

      rotation = Quaternion.FromToRotation(Vector3.up, indexMid - indexRoot).eulerAngles;
      rotations[(int)HandJoint.INDEX_FINGER_MCP] = new Vector3(rotation.x, rotation.y, rotation.z);

      rotation = Quaternion.FromToRotation(Vector3.up, indexTip - indexMid).eulerAngles;
      rotations[(int)HandJoint.INDEX_FINGER_PIP] = new Vector3(rotation.x, rotation.y, rotation.z);

      rotation = Quaternion.FromToRotation(Vector3.up, indexTipEnd - indexTip).eulerAngles;
      rotations[(int)HandJoint.INDEX_FINGER_DIP] = new Vector3(rotation.x, rotation.y, rotation.z);

      rotation = Quaternion.FromToRotation(Vector3.up, middleMid - middleRoot).eulerAngles;
      rotations[(int)HandJoint.MIDDLE_FINGER_MCP] = new Vector3(rotation.x, rotation.y, rotation.z);

      rotation = Quaternion.FromToRotation(Vector3.up, middleTip - middleMid).eulerAngles;
      rotations[(int)HandJoint.MIDDLE_FINGER_PIP] = new Vector3(rotation.x, rotation.y, rotation.z);

      rotation = Quaternion.FromToRotation(Vector3.up, middleTipEnd - middleTip).eulerAngles;
      rotations[(int)HandJoint.MIDDLE_FINGER_DIP] = new Vector3(rotation.x, rotation.y, rotation.z);

      rotation = Quaternion.FromToRotation(Vector3.up, ringMid - ringRoot).eulerAngles;
      rotations[(int)HandJoint.RING_FINGER_MCP] = new Vector3(rotation.x, rotation.y, rotation.z);

      rotation = Quaternion.FromToRotation(Vector3.up, ringTip - ringMid).eulerAngles;
      rotations[(int)HandJoint.RING_FINGER_PIP] = new Vector3(rotation.x, rotation.y, rotation.z);

      rotation = Quaternion.FromToRotation(Vector3.up, ringTipEnd - ringTip).eulerAngles;
      rotations[(int)HandJoint.RING_FINGER_DIP] = new Vector3(rotation.x, rotation.y, rotation.z);

      rotation = Quaternion.FromToRotation(Vector3.up, pinkyMid - pinkyRoot).eulerAngles;
      rotations[(int)HandJoint.PINKY_MCP] = new Vector3(rotation.x, rotation.y, rotation.z);

      rotation = Quaternion.FromToRotation(Vector3.up, pinkyTip - pinkyMid).eulerAngles;
      rotations[(int)HandJoint.PINKY_PIP] = new Vector3(rotation.x, rotation.y, rotation.z);

      rotation = Quaternion.FromToRotation(Vector3.up, pinkyTipEnd - pinkyTip).eulerAngles;
      rotations[(int)HandJoint.PINKY_DIP] = new Vector3(rotation.x, rotation.y, rotation.z);

      rotation = Quaternion.FromToRotation(Vector3.left, thumbRoot - thumbMid).eulerAngles;
      rotations[(int)HandJoint.THUMB_CMC] = new Vector3(rotation.x, rotation.y, rotation.z);

      rotation = Quaternion.FromToRotation(Vector3.up, thumbTip - thumbMid).eulerAngles;
      rotations[(int)HandJoint.THUMB_MCP] = new Vector3(rotation.x, rotation.y, rotation.z);

      rotation = Quaternion.FromToRotation(Vector3.up, thumbTipEnd - thumbTip).eulerAngles;
      rotations[(int)HandJoint.THUMB_IP] = new Vector3(rotation.x, rotation.y, rotation.z);

      Vector3 meanPoint = new Vector3((middleRoot.x + ringRoot.x) / 2, (middleRoot.y + ringRoot.y) / 2, (middleRoot.z + ringRoot.z) / 2);
      Plane handPlane = new Plane(indexRoot, pinkyRoot, wrist);

      rotation = Quaternion.FromToRotation(Vector3.back, handPlane.normal).eulerAngles;
      Quaternion zRotation = Quaternion.FromToRotation(Vector3.up, meanPoint - wrist);

      Vector3 euler = rotation;
      euler = new Vector3(-euler.x, -euler.y, zRotation.eulerAngles.z);
      rotations[(int)HandJoint.WRIST] = new Vector3(rotation.x, rotation.y, rotation.z);

      return GeneratePayload(rotations);
    }

    private static Vector3 GetPoint(NormalizedLandmarkList landmarks, HandJoint idx_point)
    {
      NormalizedLandmark joint;
      switch (idx_point)
      {
        case HandJoint.WRIST: joint = landmarks.Landmark[0]; break;
        case HandJoint.THUMB_CMC: joint = landmarks.Landmark[1]; break;
        case HandJoint.THUMB_MCP: joint = landmarks.Landmark[2]; break;
        case HandJoint.THUMB_IP: joint = landmarks.Landmark[3]; break;
        case HandJoint.THUMB_TIP: joint = landmarks.Landmark[4]; break;
        case HandJoint.INDEX_FINGER_MCP: joint = landmarks.Landmark[5]; break;
        case HandJoint.INDEX_FINGER_PIP: joint = landmarks.Landmark[6]; break;
        case HandJoint.INDEX_FINGER_DIP: joint = landmarks.Landmark[7]; break;
        case HandJoint.INDEX_FINGER_TIP: joint = landmarks.Landmark[8]; break;
        case HandJoint.MIDDLE_FINGER_MCP: joint = landmarks.Landmark[9]; break;
        case HandJoint.MIDDLE_FINGER_PIP: joint = landmarks.Landmark[10]; break;
        case HandJoint.MIDDLE_FINGER_DIP: joint = landmarks.Landmark[11]; break;
        case HandJoint.MIDDLE_FINGER_TIP: joint = landmarks.Landmark[12]; break;
        case HandJoint.RING_FINGER_MCP: joint = landmarks.Landmark[13]; break;
        case HandJoint.RING_FINGER_PIP: joint = landmarks.Landmark[14]; break;
        case HandJoint.RING_FINGER_DIP: joint = landmarks.Landmark[15]; break;
        case HandJoint.RING_FINGER_TIP: joint = landmarks.Landmark[16]; break;
        case HandJoint.PINKY_MCP: joint = landmarks.Landmark[17]; break;
        case HandJoint.PINKY_PIP: joint = landmarks.Landmark[18]; break;
        case HandJoint.PINKY_DIP: joint = landmarks.Landmark[19]; break;
        case HandJoint.PINKY_TIP: joint = landmarks.Landmark[20]; break;
        default: joint = null; break;
      }

      if (joint == null) return Vector3.zero;
      return new Vector3(joint.X * 640, (1f - joint.Y) * 480, -320 * joint.Z);
    }

    private static string GeneratePayload(Dictionary<int, Vector3> rotations)
    {
      List<float> X = new List<float>();
      List<float> Y = new List<float>();
      List<float> Z = new List<float>();

      foreach (int joint in jointList)
      {
        X.Add(rotations[joint].x);
        Y.Add(rotations[joint].y);
        Z.Add(rotations[joint].z);
      }

      List<float> completeRotations = new List<float>(X.Count + Y.Count + Z.Count);
      completeRotations.AddRange(X);
      completeRotations.AddRange(Y);
      completeRotations.AddRange(Z);

      return ListToPayload(completeRotations);
    }

    private static string ListToPayload(List<float> list)
    {
      string str = "{\"rotations\":[";
      for (int i = 0; i < list.Count; i++)
      {
        str += Math.Round(list[i], 2).ToString().Replace(',', '.');
        if (i < list.Count - 1) str += ",";
      }
      str += "]}";
      return str;
    }
  }
}
