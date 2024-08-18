using HotelSmartManagement.Common.Database.Repositories;
using HotelSmartManagement.Common.MVVM.Models;
using HotelSmartManagement.HotelOverview.MVVM.Models;

namespace HotelSmartManagement.Common.Database.Services
{
    public class HotelOverviewService
    {
        private readonly AnnouncementRepository _announcementRepository;
        private readonly EventRepository _eventRepository;
        private readonly InventoryItemRepository _inventoryItemRepository;

        public HotelOverviewService(AnnouncementRepository announcementRepository, EventRepository eventTRepository, InventoryItemRepository inventoryItemRepository)
        {
            // Repositories retrieved through DI.
            _announcementRepository = announcementRepository;
            _eventRepository = eventTRepository;
            _inventoryItemRepository = inventoryItemRepository;
        }
        public Guid? NewAnnouncement(string announcementTitle, string description) { return NewAnnouncement(announcementTitle, description, AnnouncementCategory.Other); }
        public Guid? NewAnnouncement(string announcementTitle, string description, AnnouncementCategory category)
        {
            var newAnnouncement = new Announcement { Title = announcementTitle, Description = description, DateCreated = DateTime.Now, Category = category, IsResolved = false };
            _announcementRepository.Add(newAnnouncement);
            _announcementRepository.Save();

            return newAnnouncement.UniqueId;
        }

        public Guid? NewEvent(string eventTitle, string description) { return NewEvent(eventTitle, description, Area.Other); }
        public Guid? NewEvent(string eventTitle, string description, Area area)
        {
            var newEvent = new Event { Title = eventTitle, Description = description, AreaAffected = area, DateCreated = DateTime.Now };
            _eventRepository.Add(newEvent);
            _eventRepository.Save();

            return newEvent.UniqueId;
        }

        public async Task<Announcement?> GetAnnouncement(string announcementTitle)
        {
            return await _announcementRepository.GetBy(announcement => announcement.Title == announcementTitle);
        }
        public async Task<Announcement?> GetAnnouncement(Guid guid)
        {
            return await _announcementRepository.GetById(guid);
        }

        public async Task<Event?> GetEvent(Guid eventId)
        {
            return await _eventRepository.GetById(eventId);
        }
        public async Task<Event?> GetEvent(string eventTitle)
        {
            return await _eventRepository.GetBy(eventT => eventT.Title == eventTitle);
        }

        public async Task<InventoryItem?> GetInventoryItem(Guid inventoryItemId)
        {
            return await _inventoryItemRepository.GetById(inventoryItemId);
        }
        public async Task<InventoryItem?> GetInventoryItem(string itemName)
        {
            return await _inventoryItemRepository.GetBy(inventoryItem => inventoryItem.Name == itemName);
        }

        public async void UpdateAnnouncement(Announcement announcement)
        {
            if (!await _announcementRepository.Contains(announcement))
            {
                _announcementRepository.Update(announcement);
            }
            else
            {
                _announcementRepository.Add(announcement);
            }
            _announcementRepository.Save();
        }
        public async void UpdateEvent(Event eventT)
        {
            if (!await _eventRepository.Contains(eventT))
            {
                _eventRepository.Update(eventT);
            }
            else
            {
                _eventRepository.Add(eventT);
            }
            _eventRepository.Save();
        }
        public async void UpdateInventoryItem(InventoryItem inventoryItem)
        {
            if (!await _inventoryItemRepository.Contains(inventoryItem))
            {
                _inventoryItemRepository.Update(inventoryItem);
            }
            else
            {
                _inventoryItemRepository.Add(inventoryItem);
            }
            _inventoryItemRepository.Save();
        }
    
        public void DeleteAnnouncement(Announcement announcement)
        {
            _announcementRepository.Delete(announcement);
            _announcementRepository.Save();
        }
        public async void DeleteAllAnnouncements()
        {
            var allAnnouncements = await _announcementRepository.GetAll().ToListAsync();
            _announcementRepository.DeleteRange(allAnnouncements);
            _announcementRepository.Save();
        }
        public void DeleteEvent(Event eventT)
        {
            _eventRepository.Delete(eventT);
            _eventRepository.Save();
        }
        public async void DeleteAllEvent()
        {
            var allEvent = await _eventRepository.GetAll().ToListAsync();
            _eventRepository.DeleteRange(allEvent);
            _eventRepository.Save();
        }
        public void DeleteInventoryItem(InventoryItem inventoryItem)
        {
            _inventoryItemRepository.Delete(inventoryItem);
            _inventoryItemRepository.Save();
        }
        public async void DeleteAllInventoryItems()
        {
            var allInventoryItems = await _inventoryItemRepository.GetAll().ToListAsync();
            _inventoryItemRepository.DeleteRange(allInventoryItems);
            _inventoryItemRepository.Save();
        }
    }
}
