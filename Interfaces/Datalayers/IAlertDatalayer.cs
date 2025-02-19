using APIMeteo.Models;

namespace APIMeteo.Interfaces.DataLayers
{
    public interface IAlertDatalayer
    {
        /// <summary>
        /// Get all alerts
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Alert>> GetAllAlerts();

        /// <summary>
        /// Get alert by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Alert> GetAlertById(int id);

        /// <summary>
        /// Create alert
        /// </summary>
        /// <param name="alert"></param>
        /// <returns></returns>
        Task<Alert> CreateAlert(Alert alert);

        /// <summary>
        /// Update alert
        /// </summary>
        /// <param name="alert"></param>
        /// <returns></returns>
        Task<Alert> UpdateAlert(Alert alert);

        /// <summary>
        /// Delete alert
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAlert(int id);
    }
}