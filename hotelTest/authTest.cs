using lab12a.Models.Services;
using lab12a.Models.Interfaces;
using lab12a.Models;


namespace hotelTest
{
    public class UnitTest1 :Mock
    {
        [Fact]
        public async void addHotel()
        {
            var hotel = new Hotel()
            {
                Name = "Test Hotel",
                City = "Test City",
                StreetAddress = "Test Street",
                Phone = "0799",
                Country = "Test Country",
                State = "Test State"
            };

            var hotelServices = new HotelService(_db);
            var createdHotel = await hotelServices.CreateHotel(hotel);

            Assert.NotNull(createdHotel);
            Assert.NotEqual("Test Hotel", createdHotel.Phone);

        }
        [Fact]
        public async void TestGetHotels()
        {
            var hotelService = new HotelService(_db);
            var hotels = await hotelService.GetHotelAsync();

            Assert.NotNull(hotels);


        }

       
        
        [Fact]
        //Delete Hotel
        public async void DeleteHotelTest()
        {
            var hotel = await CreateHotelsandSave();
           
            var Hotel_Service = new HotelService(_db);
            var hotId = await Hotel_Service.GetHotelById(hotel.Id);
            var Deleted_Hotel = await Hotel_Service.Delete(hotId.Id);

            Assert.Equal(hotel.Id, Deleted_Hotel.Id);
            Assert.NotNull(Deleted_Hotel);
        }
        //Update Room
        [Fact]
        public async void updateTest()
        {
            var room = await CreateRoomAndSave();
            var roomService = new RoomService(_db);
            var update_room = await roomService.UpdateRoom(new Room
                {
                Name = "New update Name",
                Layout = 4
                },room.ID);
            var newRoom_update = await roomService.GetRoomById(room.ID);
            Assert.NotNull(newRoom_update);
            Assert.Equal("New update Name", newRoom_update.Name);   
            }
        }


    }
