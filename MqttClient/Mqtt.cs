using System;
using System.Collections.Generic;
using System.Text;

namespace MqttClient
{
    /// <summary>
    /// ControlPacketType
    /// </summary>
    public enum ControlPacketType
    {
        RESERVEDSTART = 0,
        CONNECT = 1,
        CONNACK = 2,
        PUBLISH = 3,
        PUBACK = 4,
        PUBREC = 5,
        PUBREL = 6,
        PUBCOMP = 7,
        SUBSCRIBE = 8,
        SUBACK = 9,
        UNSUBSCRIBE = 10,
        UNSUBACK = 11,
        PINGREQ = 12,
        PINGRESP = 14,
        RESERVEDEND = 15
    }

    /// <summary>
    /// DUP = Duplicate delivery of a PUBLISH Control Packet
    /// QoS = PUBLISH Quality of Service
    /// RETAI? = PUBLISH Retain flag
    /// </summary>
    public class Mqtt
    {
        public Mqtt() { }

        public byte[] EncodeRemainingLength(uint[] inBytes)
        {
            List<byte> outBytes = new List<byte>();
            foreach (var b in inBytes)
            {
                var x = b;
                do
                {
                    var encodedByte = x % 128;
                    x /= 128;
                    if (x > 0)
                        encodedByte = encodedByte | 128;
                    outBytes.Add((byte)encodedByte);
                } while (x > 0);
            }
            return outBytes.ToArray();
        }

        public int DecodeRemainingLength(byte[] inBytes, out string error)
        {
            List<long> outBytes = new List<long>();
            error = null;
            var multiplier = 1;
            int lenght = 0;
            foreach (var b in inBytes)
            {
                do
                {
                    lenght += (b & 127) * multiplier;
                    multiplier *= 128;
                    if (multiplier > 128 * 128 * 128)
                    {
                        error = "Malformed Remaining Length";
                        return lenght;
                    }
                } while ((b & 128) != 0);
            }
            return lenght;
        }
    }
}
