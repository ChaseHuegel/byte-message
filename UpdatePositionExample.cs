using System;
using UnityEngine;

public class UpdatePositionExample : MonoBehaviour
{
	private float x, y, z;

	public byte[] CreatePacket()
	{
		Packet packet = new Packet();

		//	Write to the packet
		//	Important to note that a packet must be read back in the same order it was written
		packet.Write(x);
		packet.Write(y);
		packet.Write(z);

		//	Calling pack will insert the packet's total byte count as an integer at the start
		//	This is intended for determining whether an incoming packet is complete.
		//	I.E. on receiving a packet, call packet.ReadInt() to get the packet's length
		return packet.Pack();
	}

	public void ReceivePacket(Packet packet)
	{
		//	Get the packet's total byte length..
		int packetLength = packet.ReadInt();
		//	...	Confirm the packet is completed

		packet.Unpack();

		x = packet.ReadFloat();
		y = packet.ReadFloat();
		z = packet.ReadFloat();
	}
}