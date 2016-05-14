using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
