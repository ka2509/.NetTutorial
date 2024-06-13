using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.CommentD;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<Comment>> GetAllSync()
        {
            
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdSync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto updateCommentDto) 
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if(commentModel == null) {
                return null;
            }
            commentModel.Content = updateCommentDto.Content;
            commentModel.Title = updateCommentDto.Title;
            await _context.SaveChangesAsync();
            return commentModel;
        }
    }
}