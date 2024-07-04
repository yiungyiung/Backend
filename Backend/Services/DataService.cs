
using Backend.Model;
using Backend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Backend.Services
{
    public class DataService : IDataService
    {

        private readonly ApplicationDbContext _context;
        public DataService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }
    }
}
