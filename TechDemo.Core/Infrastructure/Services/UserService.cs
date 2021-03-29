using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechDemo.Core.Domain;

namespace TechDemo.Core.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IAppContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileHelper;

        public UserService(IAppContext context, IMapper mapper, IFileService fileHelper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _fileHelper = fileHelper ?? throw new ArgumentNullException(nameof(fileHelper));
        }

        public async Task<IEnumerable<UserVm>> GetAllAsync()
        {
            var users = await _context
                .Users
                .ProjectTo<UserVm>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return users;
        }

        public async Task<int> CreateAsync(CreateUserVm model)
        {
            var photo = await _fileHelper.SaveFile(model.PhotoFile);

            var user = new User
            {
                Name = model.Name,
                Password = model.Password,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                Photo = photo
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
            
        }

        public async Task DeleteAsync(int id)
        {
            var userToDelete = (await _context.Users.Where(u=> u.Id == id).FirstAsync());
                

            _context.Users.Remove(userToDelete);

            _fileHelper.RemovePhoto(userToDelete.Photo);

            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(UpdateUserVm model)
        {
            var userToUpdate = (await _context.Users.FindAsync(model.Id))
                ?? throw new UserNotFoundException(model.Id);           

            userToUpdate.Name = model.Name;
            userToUpdate.Email = model.Email;
            userToUpdate.Password = model.Password;
            userToUpdate.DateOfBirth = model.DateOfBirth;

            if (model.PhotoFile != null)
            {
                _fileHelper.RemovePhoto(userToUpdate.Photo);
                var newPhoto = await _fileHelper.SaveFile(model.PhotoFile);
                userToUpdate.Photo = newPhoto;
            }


            await _context.SaveChangesAsync();
        }

        public async Task<UserVm> GetByIdAsync(int id)
        {
            return (await _context
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<UserVm>(_mapper.ConfigurationProvider)
                .FirstAsync());
        }
    }
}
