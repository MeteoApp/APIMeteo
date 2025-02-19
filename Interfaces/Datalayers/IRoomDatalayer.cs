using APIMeteo.Models;

namespace APIMeteo.Interfaces.DataLayers
{
    public interface IRoomDatalayer
    {
        /// <summary>
        /// Get all rooms
        /// </summary>
        /// <returns></returns>
        Task<List<Room>> GetAllRooms();

        /// <summary>
        /// Get room by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>room object</returns>
        Task<Room> GetRoomById(int id);

        /// <summary>
        /// Add room
        /// </summary>
        /// <param name="room">room data</param>
        /// <returns>room object</returns>
        Task<Room> AddRoom(Room room);

        /// <summary>
        /// Update room
        /// </summary>
        /// <param name="room">room data to update</param>
        /// <returns>room object</returns>
        Task<Room> UpdateRoom(Room room);

        /// <summary>
        /// Delete room
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true or false</returns>
        Task<bool> DeleteRoom(int id);
    }
}