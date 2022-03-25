using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using UnityEngine;
using UnityEngine.UI;

namespace Mediapipe.Unity.HandTracking
{
  public class AppManager : MonoBehaviour
  {
    //[SerializeField] private string serverURL = "ws://200.145.157.219:5002/live";
    [SerializeField] private string serverURL = "ws://localhost:8000/live";
    [SerializeField] Text text;
    private string str = "";
    private WebSocket webSocket;

    private void Awake()
    {
      ClearText();
    }

    void Start()
    {
      webSocket = new WebSocket(serverURL);
      webSocket.OnOpen += (dunno, dontcare) =>
      {
        Debug.Log($"Connected to {serverURL}");
      };
      webSocket.OnMessage += (sender, message) =>
      {
        str = message.Data.Replace("libras_", "").Replace("'", "");
      };
      webSocket.OnError += (sender, error) =>
      {
        Debug.LogError($"WebSocket at {((WebSocket)sender).Url} closed with error {error.Message}");
        webSocket.Close();
      };
      Connect();
    }

    void Update()
    {
      if (str != "") text.text = str;
    }

    public void SetLandmarkList(List<NormalizedLandmarkList> landmarkLists)
    {
      if (landmarkLists != null)
      {
        foreach (NormalizedLandmarkList lndmk in landmarkLists)
        {
          string rotations = SessionManager.ConvertPayload(lndmk);
          if (webSocket != null && webSocket.IsAlive) webSocket.Send(rotations);
        }
      }
    }

    public void ClearText()
    {
      str = "";
      text.text = "";
    }

   private void Connect()
    {
      Debug.Log($"Trying to connect to C-Libras websocket at {serverURL}...");
      webSocket.Connect();
    }

    public void Reconnect()
    {
      if (webSocket.IsAlive) webSocket.Close();
      Connect();
    }

    public void OnDestroy()
    {
      webSocket.Close();
    }
  }
}
