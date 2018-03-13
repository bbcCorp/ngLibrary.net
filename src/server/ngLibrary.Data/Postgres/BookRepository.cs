using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using Npgsql;
using ngLibrary.Model;

namespace ngLibrary.Data.Postgres
{
    public class PgBookRepository : IBookRepository
    {
        private readonly ILogger<PgBookRepository> _logger;
        private string _connectionString;

        public PgBookRepository(ILogger<PgBookRepository> logger, IConfiguration config)
        {
            if (logger == null)
                throw new ArgumentNullException("Object implementing ILogger needed for object initialization");

            if (config == null)
                throw new ArgumentNullException("Object implementing IConfiguration needed for object initialization");

            _logger = logger;

            _connectionString = config["ConnectionStrings:ngLibraryDbConnection"];
        }

        public IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(this._connectionString);
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            this._logger.LogTrace(LoggingEvents.Trace, String.Format("Retrieving all batches information"));

            List<Book> result = new List<Book>();

            try
            {
                using (IDbConnection dbConnection = Connection)
                {

                    dbConnection.Open();

                    var sQuery = "SELECT * FROM ngLib.Books;";

                    result = dbConnection.Query<Book>(sQuery).ToList<Book>();

                }

                this._logger.LogTrace(LoggingEvents.Trace, String.Format("{0} books retrieved", result.Count));

            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, String.Format("ERROR: Book information could not be retrieved"));
            }


            return result;
        }

        public Book GetByID(int id)
        {
            this._logger.LogTrace(LoggingEvents.Trace, String.Format("Retrieving information for Book ID:{0}", id));

            Book retVal = null;

            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();

                    var sQuery = "SELECT * FROM ngLib.Books where id=@ID;";

                    retVal = dbConnection.Query<Book>(sQuery, new { ID = id }).Single();
                }

                this._logger.LogDebug(LoggingEvents.Debug, String.Format("Retrieved Book information for ID: {0}", id));
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, String.Format("ERROR: Unable to retrieve Book information for ID: {0} ", id));
            }

            return retVal;
        }

        public int Add(Book record)
        {
            _logger.LogTrace(LoggingEvents.Trace, String.Format("Inserting Book information :{0} with Title:{1}", record.Title, record.Title));

            int retVal = 0;

            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();

                    var sQuery = "INSERT INTO ngLib.Books (title, description, isbn, isbn13, authors, publisher, publication_year, created_date, created_by, update_tstamp)"
                           + " VALUES(@Title, @Description, @ISBN, @ISBN13, @Authors, @Publisher, @PublicationYear, @CreatedDate, @CreatedBy, @Timestamp);"
                           + " SELECT CAST(SCOPE_IDENTITY() as int)";

                    var BookID = dbConnection.Query<int>(sQuery, record).Single();

                    _logger.LogInformation(LoggingEvents.Debug, String.Format("Inserted Book information for ID: {0} - {1} ", BookID, record.Title));

                    retVal = BookID;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, String.Format("ERROR: Unable to insert Book information :{0} with Title:{1}", record.Title, record.Title));
                throw;
            }

            return retVal;
        }

        public bool Update(Book record)
        {
            _logger.LogTrace(LoggingEvents.Trace, String.Format("Updating Book information :{0}", record.Title));

            bool retVal = false;

            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();

                    var sQuery = "UPDATE ngLib.Books SET title=@Title, description=@Description, isbn=@ISBN, isbn13=@ISBN13, authors=@Authors, publisher=@Publisher, publication_year=@PublicationYear, update_tstamp=@Timestamp "
                           + " WHERE ID = @ID";

                    using (var dbtranx = dbConnection.BeginTransaction())
                    {
                        try
                        {

                            var affectedRows = dbConnection.Execute(sQuery, record, dbtranx);

                            dbtranx.Commit();
                            _logger.LogInformation(LoggingEvents.Debug, String.Format("Updated Book information for ID: {0} {1}", record.ID, record.Title));

                            if (affectedRows == 1)
                            {
                                retVal = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            dbtranx.Rollback();
                            _logger.LogError(LoggingEvents.Error, ex, $"Error while updating book ID: {record.ID}");
                            throw;
                        }
                    } // end of transaction
                } // end of connection
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, String.Format("ERROR: Unable to update Book information :{0} with Title:{1}", record.Title, record.Title));
                throw;
            }

            return retVal;
        }

        public bool Delete(int id)
        {
            this._logger.LogTrace(LoggingEvents.Trace, String.Format("Deleting Book Settings information for ID:{0}", id));

            var result = false;

            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();

                    var sQuery = "DELETE FROM ngLib.Books where id=@ID;";

                    var affectedrows = dbConnection.Execute(sQuery, new { ID = id });

                    this._logger.LogInformation(LoggingEvents.Critical, String.Format("{0} Book record deleted for ID:{1}", affectedrows, id));

                    if (affectedrows == 1)
                    {
                        result = true;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, String.Format("ERROR: Book information could not be deleted for ID: {0}", id));
                throw;
            }
        }


    }
}
