using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using SecurityPe.Data;
using SecurityPe.Domain;

namespace SecurityPe.Services
{
    public class FileService
    {
        private ChatAppContext _context;
        public FileService(ChatAppContext context)
        {
            _context = context;
        }

        public async Task<int> SaveFilePathToDb(string filePath)
        {
            var storedFile = new StoredFile
            {
                FilePath = filePath
            };
            await _context.StoredFiles.AddAsync(storedFile);
            await _context.SaveChangesAsync(); 
            return _context.StoredFiles.Count();
        }

        public string GetFilePathById(int id)
        {
            return _context.StoredFiles.Where(file => file.Id == id).FirstOrDefault().FilePath;
        }
        public StoredFile GetStoredFileById(int id)
        {
            return _context.StoredFiles.Where(file => file.Id == id).FirstOrDefault();
        }
    }
}