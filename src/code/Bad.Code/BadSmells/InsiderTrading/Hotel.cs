using System;
using System.Collections.Generic;
using System.Linq;

namespace Bad.Code.BadSmells.InsiderTrading
{
    public class HotelService
    {
        public decimal CalculatePriceOf(int roomId, int hotelId)
        {
            var hotel = FetchHotel(hotelId);

            var floors = hotel.Floors;

            var floor = floors.FirstOrDefault(f=>f.Rooms.Any(r=>r.Id == roomId));

            var room = floor.Rooms.FirstOrDefault(r => r.Id == roomId);

            switch (room.Price.Currency)
            {
                case "Rial":
                    return room.Price.Amount;
                case "Toman":
                    return room.Price.Amount * 10;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Hotel FetchHotel(int hotelId)
        {
            return new Hotel();
        }
    }
    internal class Hotel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Floor> Floors { get; set; }
    }

    internal class Floor
    {
        public List<Room> Rooms { get; set; }
    }

    internal class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoomType Type { get; set; }
        public Price Price { get; set; }
    }

    internal class Price
    {
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }

    internal enum RoomType
    {
        SingleBed,
        DoubleBed
    }
}
