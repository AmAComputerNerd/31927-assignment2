﻿using HotelSmartManagement.Common.Database.Misc;

namespace HotelSmartManagement.HotelManagement.MVVM.Models
{
    public class Announcement : IDatabaseObject
    {
#nullable disable // Reason: Model for EF - expected that these properties do not get assigned in the constructor.
        public Guid UniqueId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsResolved { get; set; }
        
#nullable enable // Reason: Model for EF - expected that these properties do not get assigned in the constructor.
    }
}
