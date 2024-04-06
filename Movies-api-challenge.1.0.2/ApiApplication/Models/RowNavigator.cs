namespace ApiApplication.Models
{
    public class RowNavigator
    {
        public RowNavigator(int totalSeats)
        {
            Current = 1;
            Total = totalSeats;
            AvailableSeats = 0;
        }

        public int Current { get; private set; }
        public int Total { get; init; }
        public int AvailableSeats { get; private set; }
        public int RemaingSeatsInRow => Total - Current;

        public void MarkAndMove(int count = 1)
        {
            AvailableSeats += count;
            Current += count;
        }
        public void Move(int count = 1)
        {
            Current += count;
            AvailableSeats = 0;
        }

        public int GetFirstAvailableSeat()
        {
            return Current - AvailableSeats + 1;
        }
    }
}
