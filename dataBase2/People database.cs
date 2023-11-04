using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataBase2
{
    internal class People_database
    {
        public int Personnel_number { get; set; }
        public string SurName { get; set; }
        Salary_database Salary_database;
        public People_database(int personnel_number,string surname)
        {
            this.Personnel_number = personnel_number;
            this.SurName = surname;
        }
    }
}
