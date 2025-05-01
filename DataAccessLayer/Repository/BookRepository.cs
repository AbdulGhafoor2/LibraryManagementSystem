using Dapper;
using System.Data;
using Library_Management_System.Models;
using Library_Management_System.DataAccessLayer.Interfaces;

namespace Library_Management_System.DataAccessLayer.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IDbConnection _db;

        public BookRepository(IDbConnection db)
        {
            _db = db;
        }


        public async Task<BookResponse> GetAllRecord()
        {

            BookResponse response = new BookResponse();
            response.IsSuccess = true;
            response.Message = "Data Fetch Successfully ";
            try
            {

                var query = "SELECT * FROM Books";
                var result = (await _db.QueryAsync<Book>(query)).ToList();

                response.IsSuccess = true;
                response.Message = result.Count == 0 ? "No record Found" : "Data Fetched Successfully";
                response.Data = result;
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
                var query = "SELECT * FROM Books WHERE Id = @Id";
                var book = await _db.QueryFirstOrDefaultAsync<Book>(query, new { Id = Id });

                response.IsSuccess = book != null;
                response.Message = book != null ? "Data fetched successfully" : "No record found";
                response.SingleData = book;
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
            try
            {
                var query = @"INSERT INTO Books (Title, Author, ISBN, PublishedDate)
                              VALUES (@Title, @Author, @ISBN, @PublishedDate);
                              SELECT LAST_INSERT_ID();";

                var id = await _db.ExecuteScalarAsync<int>(query, request);
                request.Id = id;

                response.IsSuccess = true;
                response.Message = "Data successfully inserted";
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
            try
            {
                var query = @"UPDATE Books SET 
                              Title = @Title, 
                              Author = @Author, 
                              ISBN = @ISBN, 
                              PublishedDate = @PublishedDate
                              WHERE Id = @Id";

                var rowsAffected = await _db.ExecuteAsync(query, request);

                if (rowsAffected == 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Record with given ID not found";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "Updated record successfully";
                    response.Data = new List<Book> { request };
                }
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
            BookResponse response = new BookResponse();
            
            try
            {
               var query = "DELETE FROM Books WHERE Id = @Id";
                var rowsAffected = await _db.ExecuteAsync(query, new { Id = id });

                if (rowsAffected == 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid ID, no record found to delete";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "Record deleted successfully";
                }
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