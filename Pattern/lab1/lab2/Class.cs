using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Organization : IStaff
    {
        public Guid id { get; private set; }
        public string name { get; protected set; }
        public string shortName { get; protected set; }
        public string address { get; protected set; }
        public DateTime timeStamp { get; protected set; }

        protected List<JobVacancy> JobVacancies = new();
        protected List<Employe> Emloyees = new();


        public Organization() { }
        public Organization(string name, string shortName, string address)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.shortName = shortName;
            this.address = address;
            this.timeStamp = DateTime.Now;
        }
        public Organization(Organization organization)
        {
            this.id = organization.id;
            this.name = organization.name;
            this.shortName = organization.shortName;
            this.address = organization.address;
            this.timeStamp = organization.timeStamp;
        }

        public List<JobVacancy> getJobVacancies()
        {
            return JobVacancies;
        }
        public List<Employe> getEmployees()
        {
            return Emloyees;
        }
        public List<string> getJobTitles()
        {
            List<string> list = new List<string>();
            foreach (var jv in JobVacancies)
            {
                list.Add(jv.JobTitle);
            }
            return list;
        }
        public int addJobVacancies(string title, string description)
        {
            var JV = new JobVacancy(title, description);
            JobVacancies.Add(JV);
            return 1;
        }

        public string printJobVacancies()
        {
            string str = "";
            foreach (var jv in JobVacancies)
            {
                str += jv.JobTitle + " ";
            }
            return str;
        }
        public bool delJobVacancies(Guid id)
        {
            JobVacancy del = new("1", "description");
            foreach (var jv in JobVacancies)
            {
                if (id == jv.id) del = jv;
            }
            JobVacancies.Remove(del);
            return true;
        }
        public void openJobVacancies(JobVacancy JV)
        {
            JobVacancies.Add(JV);
        }
        public bool closeJobVacancies(JobVacancy JV)
        {
            bool ret = JobVacancies.Remove(JV);
            return ret;
        }
        public Employe recruit(JobVacancy JV, string Person)
        {
            Employe employe = new(Person, JV);
            Emloyees.Add(employe);
            return employe;
        }
        public bool dismiss(Guid id, string Reason)
        {
            foreach (var emp in Emloyees)
            {
                if (emp.id == id)
                {
                    Emloyees.Remove(emp);
                    return true;
                }
            }
            return false;
        }

    }

    public class University : Organization
    {
        protected List<Faculty> faculties = new();
        public University() { }
        public University(University un) : base(un.name, un.shortName, un.address) { }
        public University(string name, string shortName, string address) : base(name, shortName, address) { }

        public int addFaculty(Faculty fac)
        {
            faculties.Add(fac);
            return faculties.Count;
        }
        public bool delFaculty(Guid id)
        {
            bool ret = false;
            foreach (var fac in faculties)
                if (fac.id == id)
                    ret = faculties.Remove(fac);

            return ret;
        }
        public bool updFaculty(Faculty newfac, Guid id)
        {
            bool ret = false;
            foreach (var fac in faculties)
                if (fac.id == id)
                    ret = faculties.Remove(fac);

            faculties.Add(newfac);
            return ret;
        }
        private bool verFaculty(Guid id)
        {
            foreach (var fac in faculties)
                if (fac.id == id)
                    return true;

            return false;
        }

        public void printInfo()
        {
            foreach (var fac in faculties)
                Console.WriteLine($"id: {fac.id}, name: {fac.name}\n");
        }
    }

    public class Faculty : Organization
    {
        protected List<Departament> departametns = new();
        public Faculty() { }
        public Faculty(Faculty faculty)
        {
            this.name = faculty.name;
            this.shortName = faculty.shortName;
            this.address = faculty.address;

        }
        public Faculty(string name, string shortname, string address)
        {
            this.name = name;
            this.shortName = shortname;
            this.address = address;
        }
        public int addDepartament(Departament dep)
        {
            departametns.Add(dep);
            return departametns.Count;
        }

        public bool gelDepartament(Departament dep)
        {
            return departametns.Remove(dep);
        }
        private bool verDepartament(Guid id)
        {
            foreach (var dep in departametns)
            {
                if (dep.id == id) return true;
            }
            return false;
        }
        public List<Departament> getDepartament()
        {
            return departametns;
        }
        public void printInfo()
        {
            Console.WriteLine("Info");
        }
    }

    interface IStaff
    {
        List<JobVacancy> getJobVacancies();
        List<Employe> getEmployees();
        List<string> getJobTitles();
        int addJobVacancies(string title, string description);
        string printJobVacancies();
        bool delJobVacancies(Guid id);
        void openJobVacancies(JobVacancy JV);
        bool closeJobVacancies(JobVacancy JV);
        Employe recruit(JobVacancy JV, string Person);
        bool dismiss(Guid id, string Reason);

    }
    public class Employe
    {
        public Guid id = Guid.NewGuid();
        public string name;
        public JobVacancy JV;

        public Employe(string name, JobVacancy jv)
        {
            this.name = name;
            this.JV = jv;
        }
    }
    public class Departament
    {
        public Guid id;
        public Departament()
        {
            id = Guid.NewGuid();
        }
    }
    public class JobVacancy
    {
        public string JobTitle;
        public string JobDescription;
        public Guid id;
        public JobVacancy(string JobTitle, string jobDescription)
        {
            this.JobTitle = JobTitle;
            id = Guid.NewGuid();
            JobDescription = jobDescription;
        }
    }



}
