using APIMeteo.Infrastructure.Database;
using APIMeteo.Interfaces.DataLayers;
using APIMeteo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace APIMeteo.Infrastructure.Datalayers
{
    public class MeasureDataLayer : IMeasuresDatalayer
    {
        private readonly EntityFrameworkDbContext _context;
        public MeasureDataLayer(EntityFrameworkDbContext context)
        {
            _context = context;
        }

        public async Task<Measure> CreateMeasure(Measure measure)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                EntityEntry entityEntryRoom = _context.Entry(measure.Room);
                entityEntryRoom.State = EntityState.Unchanged;

                await _context.Measures.AddAsync(measure);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return measure;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Une erreur est survenue lors de la création de la mesure." + ex.Message);
            }
        }

        public async Task<bool> DeleteMeasure(int id)
        {
            var transaction = _context.Database.BeginTransaction(); 

            try
            {
                if (await _context.Measures.Where(item => item.MeasureId == id).AnyAsync())
                {
                    Measure measureBDD = await _context.Measures.Where(x => x.MeasureId == id).SingleAsync();

                    EntityEntry entityEntryRoom = _context.Entry(measureBDD.Room);
                    entityEntryRoom.State = EntityState.Unchanged;

                    _context.Measures.Remove(measureBDD);
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
                throw new Exception("Erreur lors de la suppression de la mesure : " + ex.Message);
            }
        }

        public async Task<List<Measure>> GetAllMeasures()
        {
            return await _context.Measures.Include(item => item.Room).ToListAsync();
        }

        public Task<Measure> GetMeasureById(int id)
        {
            return _context.Measures.Where(x => x.MeasureId == id).Include(item => item.Room).SingleAsync();
        }

        public async Task<List<Measure>> GetMeasuresByRoom(int roomId)
        {
            return await _context.Measures.Where(x => x.Room.RoomId == roomId).Include(item => item.Room).ToListAsync();
        }

        public async Task<List<Measure>> GetLastMeasureInRoom()
        {
            return await _context.Measures.Include(m => m.Room).GroupBy(m => m.Room.RoomId).Select(g => g.OrderByDescending(m => m.Date).FirstOrDefault()).ToListAsync();
        }

        public async Task<Measure> UpdateMeasure(Measure measure)
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                Measure measureBDD = await _context.Measures.Where(item => item.MeasureId == measure.MeasureId).SingleAsync();

                measureBDD.Altitude = measure.Altitude;
                measureBDD.Temperature = measure.Temperature;
                measureBDD.Humidity = measure.Humidity;
                measureBDD.Pressure = measure.Pressure;
                measureBDD.Room = measure.Room;

                EntityEntry entityEntryRoom = _context.Entry(measureBDD.Room);
                entityEntryRoom.State = EntityState.Unchanged;

                _context.Measures.Update(measure);
                _context.SaveChanges();
                transaction.Commit();

                return measure;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Erreur lors de la mise à jour de la mesure : " + ex.Message);
            }
        }
    }
}