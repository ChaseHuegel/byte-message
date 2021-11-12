# byte-message / byte-packet
Update: A revised class "Packet.cs" has been written for my Swordfish library. See Packet.cs and PacketExample.cs (or the code below)

Easy to use tool to (un)pack variables in a byte array. Intended for sending data in networked applications and games.

This class allows you to pack and unpack variables into/from a byte array.
This can be used in a variety of ways, the original intended usage being for networking and RPCs.

The class supports int, float, bool, and string and simple to expand other types.

# Usage
```csharp
public byte[] CreatePacket()
{
    Packet packet = new Packet();

    //	Write to the packet
    packet.Write(x);
    packet.Write(y);
    packet.Write(z);

    //	Calling pack insert the packet's length to the beginning and return a byte array
    //	This operation intended for determining whether an incoming packet is complete.
    //	I.E. after receiving a packet, call packet.ReadInt() to get the packet's length
    //	If you don't need to worry about incomplete packets, don't bother calling Pack()
    //  You can call packet.GetBytes() to get the byte array without packing
    return packet.Pack();
}

public void ReceivePacket(Packet packet)
{
    //	If packed, the packet stores its length at the start..
    int packetLength = packet.ReadInt();

    //	...	Do stuff to confirm the packet is complete

    //	Alternatively packet.Unpack() will just discard the length value
    packet.Unpack();
    //	...	 Or don't bother packing in the first place

    //	Read the packet!
    //	It is important that a packet is read in the same order it was written
    x = packet.ReadFloat();
    y = packet.ReadFloat();
    z = packet.ReadFloat();
}

public void PacketBuilderExample()
{
    //  Methods related to manipulating the packet are also builders
    //  There is additionally a static Create() method for cleaner instantiation

    byte[] junkData = new byte[10];
    byte[] moreData = new byte[2];

    byte[] data = Packet.Create().Assign(junkData).Append(moreData).Write(true).Write(24).Write(25);

    Packet packet = new Packet();
    packet.Write(7.11f)
        .Write("Hello")
        .Write("Fizz")
        .Write(25)
        .Write("Buzz")
        .Pack();
}
```
