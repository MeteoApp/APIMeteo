using APIMeteo.Infrastructure.Database;
using APIMeteo.Interfaces.DataLayers;
using APIMeteo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace APIMeteo.Infrastructure.Datalayers
{
    public class AlertDatalayer : IAlertDatalayer
    {
        private readonly EntityFrameworkDbContext _context;
        public AlertDatalayer(EntityFrameworkDbContext context)
        {
            _context = context;
        }

        public async Task<Alert> CreateAlert(Alert alert)
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                EntityEntry entityEntryMeasure = _context.Entry(alert.Measure);
                entityEntryMeasure.State = EntityState.Unchanged;

                EntityEntry entityEntryRoom = _context.Entry(alert.Measure.Room);
                entityEntryRoom.State = EntityState.Unchanged;

                await _context.Alerts.AddAsync(alert);
                await _context.SaveChangesAsync();
                transaction.Commit();

                return alert;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Une erreur est survenue lors de la création de l'alerte." + ex.Message);
            }
        }

        public async Task<bool> DeleteAlert(int id)
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                if (_context.Alerts.Where(item => item.AlertId == id).Any())
                {
                    Alert alertBDD = _context.Alerts.Where(x => x.AlertId == id).Single();

                    EntityEntry entityEntryMeasure = _context.Entry(alertBDD.Measure);
                    entityEntryMeasure.State = EntityState.Unchanged;

                    EntityEntry entityEntryRoom = _context.Entry(alertBDD.Measure.Room);
                    entityEntryRoom.State = EntityState.Unchanged;

                    _context.Alerts.Remove(alertBDD);
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
                throw new Exception("Une erreur est survenue lors de la suppression de l'alerte." + ex.Message);
            }
        }

        public async Task<Alert> GetAlertById(int id)
        {
            return await _context.Alerts.Where(x => x.AlertId == id).Include(item => item.Measure).SingleAsync();
        }

        public async Task<IEnumerable<Alert>> GetAllAlerts()
        {
            return await _context.Alerts.Include(item => item.Measure).ToListAsync();
        }

        public async Task<Alert> UpdateAlert(Alert alert)
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                EntityEntry entityEntryMeasure = _context.Entry(alert.Measure);
                entityEntryMeasure.State = EntityState.Unchanged;

                EntityEntry entityEntryRoom = _context.Entry(alert.Measure.Room);
                entityEntryRoom.State = EntityState.Unchanged;

                _context.Alerts.Update(alert);
                await _context.SaveChangesAsync();
                transaction.Commit();

                return alert;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Une erreur est survenue lors de la mise à jour de l'alerte." + ex.Message);
            }
        }
    }
}