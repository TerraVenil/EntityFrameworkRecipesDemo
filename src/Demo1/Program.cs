using System.Linq;

namespace Demo1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new Entities())
            {
                var result = dbContext.LetterLanguageViews.ToList();
            }
        }
    }
}
