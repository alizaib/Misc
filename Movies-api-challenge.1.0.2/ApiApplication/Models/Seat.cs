namespace ApiApplication.Models
{
    public class Seat
    {
        public Seat(int rowNumber, int seatNumber)
        {
            RowNumber = rowNumber;
            SeatNumber = seatNumber;
        }
        public int SeatNumber { get; set; }
        public int RowNumber { get; set; }

        public static Seat None => new Seat(-1, -1);
    }
}
