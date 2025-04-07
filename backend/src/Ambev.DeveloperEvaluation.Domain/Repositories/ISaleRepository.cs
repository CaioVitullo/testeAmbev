using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        /// <summary>
        /// Adds a new sale to the repository.
        /// </summary>
        /// <param name="sale">The sale entity to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(SaleServiceDto sale);

        /// <summary>
        /// Retrieves a sale by its identifier.
        /// </summary>
        /// <param name="saleNumber">The unique identifier of the sale.</param>
        /// <returns>The sale entity.</returns>
        Task<SaleServiceDto> GetByIdAsync(int saleNumber);

        /// <summary>
        /// Retrieves all sales from the repository.
        /// </summary>
        /// <returns>A list of all sales.</returns>
        Task<IEnumerable<SaleServiceDto>> GetAllAsync();

        /// <summary>
        /// Updates an existing sale in the repository.
        /// </summary>
        /// <param name="sale">The sale entity to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(SaleServiceDto sale);

        /// <summary>
        /// Deletes a sale by its identifier.
        /// </summary>
        /// <param name="saleNumber">The unique identifier of the sale to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(int saleNumber);

        /// <summary>
        /// Provides a queryable interface for sales.
        /// </summary>
        /// <returns>An IQueryable collection of sales.</returns>
        IQueryable<SaleServiceDto> Query();
    }
}