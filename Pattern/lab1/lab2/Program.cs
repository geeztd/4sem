using lab2;
Organization organization = new("name", "shortName", "address");
var JV = new JobVacancy("title1", "desc");
organization.openJobVacancies(JV);
organization.addJobVacancies("title2", "desc");
Console.WriteLine(organization.printJobVacancies());
organization.closeJobVacancies(JV);
Console.WriteLine(organization.printJobVacancies());
organization.recruit(JV, "Pers");
organization.getEmployees();

var JV2 = new JobVacancy("title21", "desc");

Faculty faculty = new("Faculty", "fac", "address");
faculty.openJobVacancies(JV2);
faculty.addJobVacancies("title22", "desc");
faculty.closeJobVacancies(JV2);
faculty.recruit(JV2, "Pers");
faculty.getEmployees();
faculty.addDepartament(new Departament());
faculty.getDepartament();
faculty.printInfo();


var JV3 = new JobVacancy("title31", "desc");

University university = new("University", "Univ", "address");
university.openJobVacancies(JV3);
university.addJobVacancies("title32", "desc");
university.closeJobVacancies(JV3);
university.recruit(JV3, "Pers");
university.getEmployees();
university.addFaculty(faculty);
university.printInfo();