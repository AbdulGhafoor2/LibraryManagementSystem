using Library_Management_System.Models;

namespace Library_Management_System.DataAccessLayer.Interfaces
{
    public interface IBookRepository
    {
        public Task<BookResponse> GetAllRecord();
        public Task<BookResponse> GetRecordById(int id);
        public Task<BookResponse> InsertRecord(Book request);
        public Task<BookResponse> UpdateRecordById(Book request);
        public Task<BookResponse> DeleteRecordById(int id);
    }
}
