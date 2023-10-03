using InterviewService.Models;
using InterviewService.MongoDbSettings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Authentication;

namespace InterviewService.DAL
{
    public interface IBookingSlotDAL
    {
        public Task<BookingSlot> AddBookingslot(BookingSlot bookingSlot);
        public List<BookingSlot> GetAllBookingslot();
        public List<BookingSlot> GetAllBookingslotById(string id);
        //public bool UpdateBookingslot(BookingSlot bookingSlot);
        public bool DeleteBookingslot(string id);
    }

    public class BookingslotDAL : IBookingSlotDAL
    {
        private readonly IMongoCollection<BookingSlot> _bookingslots;

        public BookingslotDAL(IOptions<InterviewDBSettings> settings)
        {
            //var client = new MongoClient(settings.Value.ConnectionString);
            //var database = client.GetDatabase("BookingSlotDB");
            //_bookingslots = database.GetCollection<BookingSlot>("Bookingslots");
            string connectionString =
             @"mongodb://authserviceaccount:0IDEE5BVMQjOwuWdUXmf9RLgY0okH5pUqmSIdVHyxLBGuVakgKrA19HgSCt6WcTEvIVdKfAe6GC1ACDbRUcAfQ==@authserviceaccount.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@authserviceaccount@"; MongoClientSettings settings1 = MongoClientSettings.FromUrl(
            new MongoUrl(connectionString)
          );
            settings1.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings1);


            mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("UserDb");//.Value.DatabaseName);
            _bookingslots = database.GetCollection<BookingSlot>("Bookingslots");
        }

        public async Task<BookingSlot> AddBookingslot(BookingSlot bookingSlot)
        {
            try
            {
                await _bookingslots.InsertOneAsync(bookingSlot);
                return bookingSlot; // The ID will be updated in the timeSlot object
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }



        public bool DeleteBookingslot(string id)
        {
            _bookingslots.DeleteOneAsync(x => x.BookingID.ToString() == id);
            return true;
        }

        public List<BookingSlot> GetAllBookingslotById(string id)
        {
            List<BookingSlot> bookingslotList = _bookingslots.FindAsync(x => x.BookingID.Equals(id)).Result.ToList();
            return bookingslotList;
        }
        public List<BookingSlot> GetAllBookingslot()
        {
            List<BookingSlot> bookingslotList = _bookingslots.FindAsync(x => true).Result.ToList();
            return bookingslotList;
        }


        //public bool UpdateBookingslot(BookingSlot bookingSlot)
        //{
        //    TimeSlot existingSlot = _bookingslots.Find(x => x.BookingID.Equals(bookingSlot.Id)).FirstOrDefaultAsync().Result;
        //    existingSlot.StartTime = bookingSlot.;
        //    existingSlot.EndTime = bookingSlot.EndTime;
        //    return true;
        //}
    }
}
