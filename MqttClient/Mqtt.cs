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

        public uint EncodeRemainingLenght(uint b)
        {
            do
            {
                var encodedByte = b % 128;
                b = b / 128;
                if (b > 0)
                    encodedByte = encodedByte | 128;
                else
                    return encodedByte;
            } while (b > 0);
            return 0;
        }

        public int DecodeRemainingLenght(int b, out string error)
        {
            error = null;
            var multiplier = 1;
            var value = 0;
            do
            {
                value += (b & 127) * multiplier;
                multiplier *= 128;
                if (multiplier > 128 * 128 * 128)
                {
                    error = "Malformed Remaining Length";
                }
            } while ((b & 128) != 0);
            return value;
        }
    }
}
