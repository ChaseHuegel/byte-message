using System;
using UnityEngine;

public class UpdatePositionExample : MonoBehaviour
{
	private ByteMessage Bytes = new ByteMessage();
	
	void Update()
	{
		//  Reset our message
		Bytes.ResetMessage();
		
		//  Write our position
		Bytes.Write(transform.position.x);
		Bytes.Write(transform.position.y);
		Bytes.Write(transform.position.z);
		
		//  Write our rotation
		Bytes.Write(transform.rotation.x);
		Bytes.Write(transform.rotation.y);
		Bytes.Write(transform.rotation.z);
		
		//  Send the RPC
		GetComponent<NetworkView>().RPC("UpdatePlayerPosition", RPCMode.Others, Bytes.GetMessage());
	}
	
	[RPC]
	private void UpdatePlayerPosition(byte[] data, NetworkMessageInfo info)
	{
		//  Assign the message to the one we recieved
		Bytes.SetMessage(data);
		
		//  IMPORTANT: Read in the same order we wrote it!!
		
		//  Read our position
		float transformX = Bytes.ReadFloat();
		float transformY = Bytes.ReadFloat();
		float transformZ = Bytes.ReadFloat();
		
		//  Read our rotation
		float rotationX = Bytes.ReadFloat();
		float rotationY = Bytes.ReadFloat();
		float rotationZ = Bytes.ReadFloat();
		
		//  Set our position and rotation
		transform.position = new Vector3(transformX, transformY, transformZ);
		transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
	}
}