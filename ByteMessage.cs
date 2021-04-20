//------------------------------------------------------------------------------
//	ByteMessage.cs
//		Created by Chase Huegel
//
//	Description:
//		This class allows you to pack variables into a byte array, and unpack variables from a byte array.
//		This can be used in a variety of ways, the original intended usage being for networking and RPCs.
//		Currently this only supports floats, integers, strings, and booleans.
//
//		Thanks for choosing ByteMessage, and goodluck on your ventures!
//
//	Usage (C#):
//		Initialize your ByteMessage with:
//			ByteMessage bytes = new ByteMessage();
//
//		Write variables to the message using:
//			bytes.Write(myVariable);
//
//		Read variables from the message using:
//			int myInt = bytes.ReadInt();
//			float myFloat = bytes.ReadFloat();
//			bool myBool = bytes.ReadBool();
//			string myString = bytes.ReadString();
//
//		Get/Set/Reset the message using:
//			byte[] myMessage = bytes.GetMessage();
//			bytes.SetMessage(myMessage);
//			bytes.ResetMessage();
//
//------------------------------------------------------------------------------
using System;

public class ByteMessage
{
	private byte[] message = new byte[0];
	private int readIndex = 0;

	//  Retrieve the message (Byte array)
	public byte[] GetMessage() { return message; }

	//  Set the message (Byte array), and reset the read index
	public void SetMessage(byte[] temp) { message = temp; readIndex = 0; }

	//  Reset the message (Byte array), and reset the read index
	public void ResetMessage() { message = new byte[0]; readIndex = 0; }

	//  Convert a variable to bytes then write them to the message
	public void Write(object value)
	{
		if (value is string)
		{
			string tempString = (string)value;
			Char[] tempChars = tempString.ToCharArray();

			//  First write an integer to state the length of the string
			byte[] bytes = BitConverter.GetBytes(tempChars.Length);
			BytesToMessage(bytes);

			//  Write each character in the string to the message
			for (int i = 0; i < tempChars.Length; i++)
			{
				bytes = BitConverter.GetBytes(tempChars[i]);
				BytesToMessage(bytes);
			}
		}

		if (value is int)
		{
			byte[] bytes = BitConverter.GetBytes((int)value);
			BytesToMessage(bytes);
		}

		if (value is float)
		{
			byte[] bytes = BitConverter.GetBytes((float)value);
			BytesToMessage(bytes);
		}

		if (value is bool)
		{
			byte[] bytes = BitConverter.GetBytes((bool)value);
			BytesToMessage(bytes);
		}
	}

	//  Write bytes to the message
	private void BytesToMessage(byte[] bytes)
	{
		byte[] temp = new byte[message.Length + bytes.Length];

		for (int i = 0; i < message.Length; i++)
		{
			temp[i] = message[i];
		}

		for (int i = 0; i < bytes.Length; i++)
		{
			temp[message.Length + i] = bytes[i];
		}

		message = temp;
	}

	//  Read a string from the message
	public string ReadString()
	{
		byte[] temp = new byte[4];

		for (int i = 0; i < temp.Length; i++)
		{
			temp[i] = message[readIndex + i];
		}

		readIndex += temp.Length;

		int length = BitConverter.ToInt32(temp, 0);
		Char[] tempChars = new Char[length];

		temp = new byte[1];

		for (int i = 0; i < tempChars.Length; i++)
		{
			temp[0] = message[readIndex];
			readIndex += temp.Length;

			tempChars[i] = BitConverter.ToChar(temp, 0);
		}

		return tempChars.ToString();
	}

	//  Read an int from the message
	public int ReadInt()
	{
		byte[] temp = new byte[4];

		for (int i = 0; i < temp.Length; i++)
		{
			temp[i] = message[readIndex + i];
		}

		readIndex += temp.Length;

		return BitConverter.ToInt32(temp, 0);
	}

	//  Read a float from the message
	public float ReadFloat()
	{
		byte[] temp = new byte[4];

		for (int i = 0; i < temp.Length; i++)
		{
			temp[i] = message[readIndex + i];
		}

		readIndex += temp.Length;

		return BitConverter.ToSingle(temp, 0);
	}

	//  Read a boolean from the message
	public bool ReadBool()
	{
		byte[] temp = new byte[1];

		temp[0] = message[readIndex];
		readIndex += 1;

		return BitConverter.ToBoolean(temp, 0);
	}
}