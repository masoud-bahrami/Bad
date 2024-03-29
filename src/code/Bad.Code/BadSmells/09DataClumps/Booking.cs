﻿using System;

namespace Bad.Code.BadSmells._09DataClumps
{
    public class Booking
    {
        public Booking(int bookingId, int roomId, DateTime? from, DateTime? to)
        {
            BookingId = bookingId;
            RoomId = roomId;
            From = from;
            To = to;
        }
        public int BookingId { get; private set; }
        public int RoomId { get; private set; }
        public DateTime? From { get; private set; }
        public DateTime? To { get; private set; }
    }
}