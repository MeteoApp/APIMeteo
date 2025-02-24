

using APIMeteo.CustomException;
using APIMeteo.Infrastructure.Database;
using APIMeteo.Interfaces.DataLayers;
using APIMeteo.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMeteo.Infrastructure.Datalayers
{
    public class RoomDatalayer : IRoomDatalayer
    {
        #region Properties
        private readonly EntityFrameworkDbContext _context;
        #endregion

        #region Constructor
        public RoomDatalayer(EntityFrameworkDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public async Task<Room> AddRoom(Room room)
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                await _context.Rooms.AddAsync(room);
                await _context.SaveChangesAsync();

                transaction.Commit();

                return room;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Erreur lors de l'ajout de la pièce : " + ex.Message);
            }
        }

        public async Task<bool> DeleteRoom(int id)
        {
            var transaction = _context.Database.BeginTransaction(); 

            try
            {
                if (await _context.Measures.Where(item => item.Room.RoomId == id).AnyAsync())
                {
                    throw new Exception("Impossible de supprimer la pièce car des mesures sont associées à cette pièce.");
                }

                if (await _context.Rooms.Where(item => item.RoomId == id).AnyAsync())
                {
                    Room roomBDD = await _context.Rooms.Where(x => x.RoomId == id).SingleAsync();

                    _context.Rooms.Remove(roomBDD);
                    await _context.SaveChangesAsync();
                    transaction.Commit();

                    return true;
                }
                else
                {
                    transaction.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Erreur lors de la suppression de la pièce : " + ex.Message);
            }
        }

        public async Task<Room> GetRoomById(int id)
        {
            if (await _context.Rooms.Where(item => item.RoomId == id).AnyAsync())
            {
                return await _context.Rooms.Where(x => x.RoomId == id).SingleAsync();
            }
            else
            {
                throw new NoResultException($"Aucune pièce trouvé avec l'id {id}");
            }

            
        }

        public async Task<Room> UpdateRoom(Room room)
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                if (await _context.Rooms.Where(item => item.RoomId == room.RoomId).AnyAsync())
                {
                    Room roomBDD = await _context.Rooms.Where(x => x.RoomId == room.RoomId).SingleAsync();

                    roomBDD.RoomName = room.RoomName;

                    _context.Entry(roomBDD).State = EntityState.Modified;   
                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    return room;
                }
                else
                {
                    throw new NoResultException($"Aucune pièce trouvé avec l'id {room.RoomId}");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Erreur lors de la mise à jour de la pièce : " + ex.Message);
            }
        }

        public async Task<List<Room>> GetAllRooms()
        {
            return await _context.Rooms.ToListAsync();
        }
        #endregion
    }
}