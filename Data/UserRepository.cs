using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;   
            _mapper = mapper;
        }
        public async Task<IEnumerable<AppUser>> GetUserAsync()
        {
            return await _context.appUsers
            .Include(p => p.Images)
            .Include(x => x.Qas)
            .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.appUsers.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.appUsers.SingleOrDefaultAsync(x => x.userName == username);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(AppUser user)
        {
            _context.Entry(user).State = EntityState.Deleted;
        }

        public async Task<ActionResult<AppUser>> SaveAsync(AppUser user)
        {
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<PageList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            var query = _context.appUsers
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .AsNoTracking();

            return await PageList<MemberDto>.CreateAsync(query, userParams.pageNumber, userParams.PageSize);
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.appUsers
            .Where(x => x.userName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }
    }
}