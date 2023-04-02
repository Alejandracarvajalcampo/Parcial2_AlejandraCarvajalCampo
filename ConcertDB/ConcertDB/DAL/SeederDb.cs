using ConcertDB.DAL.Entities;

namespace ConcertDB.DAL
{
    public class SeederDb
    {
        private readonly DatabaseContext _context;

        public SeederDb(DatabaseContext context) 
        {
            _context = context;
        
        }

        public async Task SeeederAsync() 
        {
        await _context.Database.EnsureCreatedAsync();
            await PopulateTicketsAsync();

            await _context.SaveChangesAsync();

        }

        private async Task PopulateTicketsAsync()
        {
            if (!_context.Tickets.Any())
            {
                for (int i = 1; i <= 10; i++)
                {
                    _context.Tickets.Add(new Tickets { UseDate = null, IsUsed = false, EntranceGate = null });
                }
                
            }

        }
    }
}
