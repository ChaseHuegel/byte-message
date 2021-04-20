# byte-message
Easy to use tool to (un)pack variables in a byte array. Intended for sending data in networked applications and games.

This class allows you to pack variables into a byte array, and unpack variables from a byte array.
This can be used in a variety of ways, the original intended usage being for networking and RPCs.
Currently this only supports floats, integers, strings, and booleans.

I used this method to send data between client and server for an auto updating tool years ago, and needed a similar use for networking in a Unity game so I wrote this. It uses nothing more than System.cs so it is applicable anywhere.

# Usage
Initialize your ByteMessage with

ByteMessage bytes = new ByteMessage();

Write variables to the message using

bytes.Write(myVariable)

Read variables from the message using:

int myInt = bytes.ReadInt();
float myFloat = bytes.ReadFloat();
bool myBool = bytes.ReadBool();
string myString = bytes.ReadString();

Get/Set/Reset the message using

byte[] myMessage = bytes.GetMessage();
bytes.SetMessage(myMessage);
bytes.ResetMessage();
