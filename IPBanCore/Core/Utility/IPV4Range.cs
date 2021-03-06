﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalRuby.IPBanCore
{
    /// <summary>
    /// Range of ipv4 addresses
    /// </summary>
    public struct IPV4Range : IComparable<IPV4Range>
    {
        /// <summary>
        /// Begin ip
        /// </summary>
        public uint Begin;

        /// <summary>
        /// End ip
        /// </summary>
        public uint End;

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (int)(Begin + End);
            }
        }

        /// <summary>
        /// Check for equality
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>True if equal, false otherwise</returns>

        public override bool Equals(object obj)
        {
            if (!(obj is IPV4Range range))
            {
                return false;
            }
            return Begin == range.Begin && End == range.End;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="range">IPAddressRange</param>
        /// <exception cref="InvalidOperationException">Invalid address family</exception>
        public IPV4Range(IPAddressRange range)
        {
            if (range.Begin.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
            {
                throw new InvalidOperationException("Wrong address family for an ipv4 range");
            }
            Begin = range.Begin.ToUInt32();
            End = range.End.ToUInt32();
        }

        /// <summary>
        /// Conver to an ip address range
        /// </summary>
        /// <returns>IPAddressRange</returns>
        public IPAddressRange ToIPAddressRange() => new IPAddressRange(Begin.ToIPAddress(), End.ToIPAddress());

        /// <summary>
        /// IComparer against another IPV4Range
        /// </summary>
        /// <param name="other">Other range</param>
        /// <returns></returns>
        public int CompareTo(IPV4Range other)
        {
            int cmp = End.CompareTo(other.Begin);
            if (cmp < 0)
            {
                return cmp;
            }
            cmp = Begin.CompareTo(other.End);
            if (cmp > 0)
            {
                return cmp;
            }

            // inside range
            return 0;
        }
    }
}
