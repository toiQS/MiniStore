using Microsoft.EntityFrameworkCore;
using MiniStore.Data;
using MiniStore.Services.Files;
using MiniStore.Services.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.Repository
{
    // Generic repository implementation for CRUD operations
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        private string _path;

        public Repository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
            _path = string.Empty;
        }

        // Method to get the file path based on folder and file name
        public string GetPath(string folderName, string fileName)
        {
            var app = new App();
            _path = app.GetPathCurrentFolder(folderName, fileName);
            return _path;
        }

        // Retrieve all entities from the database
        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                var data = await _dbSet.AsNoTracking().ToListAsync();
                return data ?? Enumerable.Empty<T>();
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Failed to retrieve data", ex);
                throw;
            }
        }

        // Add a new entity to the database
        public async Task<bool> Add(T entity)
        {
            if (entity == null) return false;

            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Failed to add entity", ex);
                throw;
            }
        }

        // Delete an existing entity from the database
        public async Task<bool> Delete(T entity)
        {
            if (entity == null) return false;

            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Failed to delete entity", ex);
                throw;
            }
        }

        // Update an existing entity in the database
        public async Task<bool> Update(T entity)
        {
            if (entity == null) return false;

            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Failed to update entity", ex);
                throw;
            }
        }

        // Log error details to a file
        private async Task LogErrorAsync(string message, Exception ex)
        {
            var errorDetails = new StringBuilder();
            errorDetails.AppendLine($"{message}\n");
            errorDetails.AppendLine($"Message: {ex.Message}\n");
            errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
            errorDetails.AppendLine($"Source: {ex.Source}\n");
            errorDetails.AppendLine($"Time: {DateTime.Now}\n");

            if (!string.IsNullOrEmpty(_path))
            {
                await File.AppendAllTextAsync(_path, errorDetails.ToString());
            }
        }
    }
}
