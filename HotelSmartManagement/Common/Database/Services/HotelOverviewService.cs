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
            var newEvent = new Event { Title = eventTitle, Description = description, AreaAffected = area, DateAffected = DateTime.Now };
            _eventRepository.Add(newEvent);
            _eventRepository.Save();

            return newEvent.UniqueId;
        }

        public Guid? NewItem(string name, int quantity)
        {
            var newItem = new InventoryItem { Name = name, Quantity = quantity };
            _inventoryItemRepository.Add(newItem);
            _inventoryItemRepository.Save();

            return newItem.UniqueId;
        }

        public Announcement? GetAnnouncement(string announcementTitle)
        {
            return  _announcementRepository.GetBy(announcement => announcement.Title == announcementTitle);
        }
        public Announcement? GetAnnouncement(Guid guid)
        {
            return _announcementRepository.GetById(guid);
        }
        public IEnumerable<Announcement> GetAllAnnouncements()
        {
            return _announcementRepository.GetAll();
        }

        public Event? GetEvent(Guid eventId)
        {
            return _eventRepository.GetById(eventId);
        }
        public Event? GetEvent(string eventTitle)
        {
            return _eventRepository.GetBy(eventT => eventT.Title == eventTitle);
        }
        public IEnumerable<Event> GetAllEvents()
        {
            return _eventRepository.GetAll();
        }

        public InventoryItem? GetInventoryItem(Guid inventoryItemId)
        {
            return _inventoryItemRepository.GetById(inventoryItemId);
        }
        public InventoryItem? GetInventoryItem(string itemName)
        {
            return _inventoryItemRepository.GetBy(inventoryItem => inventoryItem.Name == itemName);
        }
        public IEnumerable<InventoryItem> GetAllInventory()
        {
            return _inventoryItemRepository.GetAll();
        }

        public  void UpdateAnnouncement(Announcement announcement)
        {
            if (_announcementRepository.Contains(announcement))
            {
                _announcementRepository.Update(announcement);
            }
            else
            {
                _announcementRepository.Add(announcement);
            }
            _announcementRepository.Save();
        }
        public  void UpdateEvent(Event eventT)
        {
            if (_eventRepository.Contains(eventT))
            {
                _eventRepository.Update(eventT);
            }
            else
            {
                _eventRepository.Add(eventT);
            }
            _eventRepository.Save();
        }
        public  void UpdateInventoryItem(InventoryItem inventoryItem)
        {
            if (_inventoryItemRepository.Contains(inventoryItem))
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
        public void DeleteAllAnnouncements()
        {
            var allAnnouncements = _announcementRepository.GetAll();
            _announcementRepository.DeleteRange(allAnnouncements);
            _announcementRepository.Save();
        }
        public void DeleteEvent(Event eventT)
        {
            _eventRepository.Delete(eventT);
            _eventRepository.Save();
        }
        public void DeleteAllEvent()
        {
            var allEvent = _eventRepository.GetAll();
            _eventRepository.DeleteRange(allEvent);
            _eventRepository.Save();
        }
        public void DeleteInventoryItem(InventoryItem inventoryItem)
        {
            _inventoryItemRepository.Delete(inventoryItem);
            _inventoryItemRepository.Save();
        }
        public void DeleteAllInventoryItems()
        {
            var allInventoryItems = _inventoryItemRepository.GetAll();
            _inventoryItemRepository.DeleteRange(allInventoryItems);
            _inventoryItemRepository.Save();
        }
    }
}
