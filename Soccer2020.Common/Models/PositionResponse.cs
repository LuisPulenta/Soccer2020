using Soccer2020.Common.Models;

namespace Soccer2000.Common.Models
{
    public class PositionResponse
    {
        public UserResponse UserResponse { get; set; }

        public int? Points { get; set; }

        public int Ranking { get; set; }
    }
}