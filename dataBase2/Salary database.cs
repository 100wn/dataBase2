using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataBase2
{
    internal class Salary_database
    {
        public int PersonalNumber { get; set; }
        public List<int> MonthlySalaries { get; set; }

        public Salary_database(int personalNumber)
        {
            this.PersonalNumber = personalNumber;
            MonthlySalaries = new List<int>(12);
        }
    }
}
