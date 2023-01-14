using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerID : NetworkBehaviour
{

    [SyncVar] public string playerUniqueName;
    NetworkInstanceId playerNetID;

    public override void OnStartLocalPlayer()
    {
        GetNetIdentity();
        SetIdentity();
        SetCameraTarget();

    }

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.name == "" || transform.name == "Player_prefab(Clone)")
        {
            SetIdentity();
        }
    }

    [Client]
    void GetNetIdentity()
    {
        playerNetID = GetComponent<NetworkIdentity>().netId;

        CmdTellServerMyIdentity(MakeUniqueIdentity());
    }

    void SetIdentity()
    {
        if (!isLocalPlayer)
        {
            this.transform.name = playerUniqueName;
        }
        else
        {
            transform.name = MakeUniqueIdentity();
        }
    }

    string MakeUniqueIdentity()
    {
        // throw new NotImplementedException();
        return "Player" + playerNetID.ToString();
    }

    [Command]
    void CmdTellServerMyIdentity(string name)
    {
        playerUniqueName = name;
    }

    void SetCameraTarget()
    {
        if (isLocalPlayer)
        {
            Camera.main.GetComponent<CameraFollow>().Target = this.gameObject;
            Camera.main.GetComponent<CameraFollow>().SetCamaraOffset(this.gameObject.transform.position);
        }

    }
}
