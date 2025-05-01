using Library_Management_System.Data;
using Library_Management_System.DataAccessLayer.Interfaces;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Library_Management_System.DataAccessLayer.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookResponse> GetAllRecord()
        {

            BookResponse response = new BookResponse();
            response.IsSuccess = true;
            response.Message = "Data Fetch Successfully ";
            try
            {

                response.Data = new List<Book>();
                response.Data = await _context.Books.ToListAsync();
                if (response.Data.Count == 0)
                {
                    response.Message = "No record Found";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs:" + ex.Message;

            }
            return response;

        }
        public async Task<BookResponse> GetRecordById(int Id)
        {
            BookResponse response = new BookResponse
            {
                IsSuccess = true,
                Message = "Data fetched successfully",
             
            };

            try
            {
                var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == Id);
                if (book != null)
                {
                    response.SingleData= book;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No record found";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception occurred: " + ex.Message;
            }

            return response;
        }

        public async Task<BookResponse> InsertRecord(Book request)
        {
            BookResponse response = new BookResponse();
            response.IsSuccess = true;
            response.Message = "Data Successfully inserted";
            try
            {
                await _context.Books.AddAsync(request);
                await _context.SaveChangesAsync();
                response.Data = new List<Book> { request };
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs:" + ex.Message;

            }
            return response;
        }


        public async Task<BookResponse> UpdateRecordById(Book request)
        {
            BookResponse response = new BookResponse();
            response.IsSuccess = true;
            response.Message = "Updated record successfully by ID";

            try
            {
                var existingBook = await _context.Books.FindAsync(request.Id);
                if (existingBook == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Record with given ID not found";
                    return response;
                }

                // Update properties
                existingBook.Title = request.Title;
                existingBook.Author = request.Author;
                existingBook.ISBN = request.ISBN;
                existingBook.PublishedDate = request.PublishedDate;

                await _context.SaveChangesAsync();
                response.Data = new List<Book> { existingBook };
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception occurred: " + ex.Message;
            }

            return response;
        }

        public async Task<BookResponse> DeleteRecordById(int id)
        {
            BookResponse response = new BookResponse
            {
                IsSuccess = true,
                Message = "Record deleted successfully"
            };

            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid ID, no record found to delete";
                    return response;
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs: " + ex.Message;
            }

            return response;
        }
    }
}