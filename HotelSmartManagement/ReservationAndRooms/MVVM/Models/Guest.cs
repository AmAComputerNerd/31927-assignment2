using HotelSmartManagement.Common.Database.Misc;
using HotelSmartManagement.Common.MVVM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSmartManagement.ReservationAndRooms.MVVM.Models
{
    public class Guest : IDatabaseObject
    {
        [Key]
        public Guid UniqueId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Tier { get; set; }

        public DateTime CreationDate { get; set; }

        public int Stays { get; set; }

        [InverseProperty("Guest")]
        public Reservation? Reservation { get; set; }

        [NotMapped]
        public string TierBackground
        {
            get { return Tier ?? ""; }
            set
            {
                switch(value)
                {
                    case "Bronze":
                        TierBackground = "Brown";
                        return;

                    case "Silver":
                        TierBackground = "Silver";
                        return;

                    case "Gold":
                        TierBackground = "Gold";
                        return;

                    case "Platinum":
                        TierBackground = "Aquamarine";
                        return;

                    default:
                        return;
                }
            }
        }

        [NotMapped]
        public string TotalStays => $"{Stays} stays";

        [NotMapped]
        public string FullName => FirstName + " " + LastName;
    }
}
