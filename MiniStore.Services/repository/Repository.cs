using Microsoft.EntityFrameworkCore;
using MiniStore.Data;
using MiniStore.Services.Files;
using MiniStore.Services.repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.Repository
{
    // Generic repository implementation for CRUD operations
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context; // Database context
        private readonly DbSet<T> _dbSet; // Entity set representing the table in the database
        private string _path; // File path for logging errors

        // Constructor initializing the repository with the database context
        public Repository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); // Ensure context is not null
            _dbSet = _context.Set<T>(); // Initialize the entity set
            _path = string.Empty; // Initialize the file path
        }

        // Method to get the file path based on folder and file name
        public string GetPath(string folderName, string fileName)
        {
            var app = new App();
            _path = app.GetPathCurrentFolder(folderName, fileName); // Retrieve the path using a helper method
            return _path; // Return the path
        }

        // Retrieve all entities from the database asynchronously
        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                var data = await _dbSet.AsNoTracking().ToListAsync(); // Retrieve data without tracking changes
                return data; // Return the data
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Failed to retrieve data", ex); // Log the error if an exception occurs
                throw; // Rethrow the exception
            }
        }

        // Add a new entity to the database asynchronously
        public async Task<bool> Add(T entity)
        {
            if (entity == null) return false; // Return false if the entity is null

            try
            {
                await _dbSet.AddAsync(entity); // Add the entity to the set
                await _context.SaveChangesAsync(); // Save changes to the database
                return true; // Return true if successful
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Failed to add entity", ex); // Log the error if an exception occurs
                throw; // Rethrow the exception
            }
        }

        // Delete an existing entity from the database asynchronously
        public async Task<bool> Delete(T entity)
        {
            if (entity == null) return false; // Return false if the entity is null

            try
            {
                _dbSet.Remove(entity); // Remove the entity from the set
                await _context.SaveChangesAsync(); // Save changes to the database
                return true; // Return true if successful
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Failed to delete entity", ex); // Log the error if an exception occurs
                throw; // Rethrow the exception
            }
        }

        // Update an existing entity in the database asynchronously
        public async Task<bool> Update(T entity)
        {
            if (entity == null) return false; // Return false if the entity is null

            try
            {
                _dbSet.Update(entity); // Update the entity in the set
                await _context.SaveChangesAsync(); // Save changes to the database
                return true; // Return true if successful
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Failed to update entity", ex); // Log the error if an exception occurs
                throw; // Rethrow the exception
            }
        }

        // Log error details to a file asynchronously
        private async Task LogErrorAsync(string message, Exception ex)
        {
            var errorDetails = new StringBuilder(); // Initialize a StringBuilder for error details
            errorDetails.AppendLine($"{message}\n"); // Append the error message
            errorDetails.AppendLine($"Message: {ex.Message}\n"); // Append the exception message
            errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n"); // Append the stack trace
            errorDetails.AppendLine($"Source: {ex.Source}\n"); // Append the source of the exception
            errorDetails.AppendLine($"Time: {DateTime.Now}\n"); // Append the current time

            if (!string.IsNullOrEmpty(_path))
            {
                await File.AppendAllTextAsync(_path, errorDetails.ToString()); // Write the error details to the log file
            }
        }
    }
}
