using lab12a.Data;
using lab12a.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
namespace hotelTest
{
    public abstract class Mock :IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly AsyncInnContext _db;
        public Mock() {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new AsyncInnContext(
                  new DbContextOptionsBuilder<AsyncInnContext>()
                  .UseSqlite(_connection).Options);
            _db.Database.EnsureCreated();
                
        }

        protected async Task<Hotel> CreateHotelsandSave()
        {
            var hotel = new Hotel()
            {
                Name = "Test",
                City = "Amman",
                Country = "JOR",
                Phone = "07894",
                State = "bas",
                StreetAddress = "ST"
            };
            _db.hotels.Add(hotel);
            await _db.SaveChangesAsync();
            return hotel;
        }
        protected async Task<Amenities> CreateAmenities()
        {
            var amenities = new Amenities()
            {
                Name = "Test",
               

            };
            _db.amenities.Add(amenities);
            await _db.SaveChangesAsync();
            return amenities;
        }
        protected async Task<Room> CreateRoomAndSave()
        {
            var room = new Room()
            {
                Name = "Test",
                Layout = 4
            };
            _db.rooms.Add(room);
            await _db.SaveChangesAsync();
            return room;

        }
        public void Dispose()
        {
           _db?.Dispose();
            _connection?.Dispose();
        }

    }
}