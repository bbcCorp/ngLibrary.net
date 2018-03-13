using System.Collections.Generic;
using ngLibrary.Model;

namespace ngLibrary.Data
{
    public interface IBookRepository
    {
        int Add(Book book);
        bool Update(Book record);

        bool Delete(int id);

        Book GetByID(int id);

        IEnumerable<Book> GetAllBooks();


    }
}
