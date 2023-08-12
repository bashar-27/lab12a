using lab12a.Models.Services;
using lab12a.Models.Interfaces;
using lab12a.Models;
using lab12a.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace hotelTest
{
    public class DistrictManagerTests : Mock
    {
        [Fact]
        public async Task DistrictManager_CanCreateRoom()
        {
            // Arrange
            var districtManagerRole = new IdentityRole { Name = "District Manager" };
            var userManager = MockUserManager.GetMockUserManager<AppUser>();
            userManager.Setup(um => um.GetRolesAsync(It.IsAny<AppUser>()))
                       .ReturnsAsync(new List<string> { districtManagerRole.Name });

            var roomToCreate = new Room
            {
                Name = "NewRoom",
                Layout = 1
            };

            // Act
            var roomService = new RoomService(_db, userManager.Object); // Replace with your RoomService implementation
            var result = await roomService.CreateRoom(roomToCreate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("NewRoom", result.Name);
        }

    }

        public static class MockUserManager
        {
            public static Mock<UserManager<TUser>> GetMockUserManager<TUser>() where TUser : class
            {
                var store = new Mock<IUserStore<TUser>>();
                return new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            }
        }

    }
