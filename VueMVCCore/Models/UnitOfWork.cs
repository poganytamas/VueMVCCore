namespace VueMVCCore.Models
{
    using System;

    public class UnitOfWork : IDisposable
    {
        private readonly TestDataContext _context;
        public UnitOfWork(TestDataContext context)
        {
            _context = context;
            // repository init with context here
        }

        // repositories as properties here

        public int Complete() {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
