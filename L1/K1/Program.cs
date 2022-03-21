using System;
using System.Collections.Generic;
using System.IO;

namespace K1
{
    class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Group { get; set; }
        public List<int> Credits { get; set; }

        public Student(string name, string surname, string group, List<int> credits)
        {
            Name = name;
            Surname = surname;
            Group = group;
            Credits = new List<int>(credits);
        }

        public int Sum(int ii)
        {
            return Credits.Count > ii ? Credits[ii] + Sum(ii+1) : 0;
        }

        public static bool operator >(Student a, Student b)
        {
            int groupCompare = a.Group.CompareTo(b.Group);
            if (groupCompare == 0)
            {
                return a.Surname.CompareTo(b.Surname) > 0;
            } else
            {
                return groupCompare > 0;
            }
        }
        public static bool operator <(Student a, Student b)
        {
            return !(a > b && a == b);
        }

        public static bool operator ==(Student a, Student b)
        {
            return a.Group.Equals(b.Group) && a.Surname.Equals(b.Surname);
        }
        public static bool operator !=(Student a, Student b)
        {
            return !(a == b);
        }
    }

    class Faculty
    {
        public string Name { get; set; }
        public int CreditLimit { get; set; }
        public List<Student> Students { get; set; }

        public Faculty(string name, int creditLimit, List<Student> students)
        {
            Name = name;
            CreditLimit = creditLimit;
            Students = new List<Student>(students);
        }

        public void Sort()
        {
            for (int i = 0; i < Students.Count-1; i++)
            {
                for (int j = i+1; j < Students.Count; j++)
                {
                    if (Students[i] > Students[j])
                    {
                        Student tmp = Students[i];
                        Students[i] = Students[j];
                        Students[j] = tmp;
                    }
                }
            }
        }

        public static bool operator >(Faculty a, Faculty b)
        {
            return a.Students.Count > b.Students.Count;
        }
        public static bool operator <(Faculty a, Faculty b)
        {
            return a.Students.Count < b.Students.Count;
        }

        public static bool operator ==(Faculty a, Faculty b)
        {
            return a.Students.Count == b.Students.Count;
        }
        public static bool operator !=(Faculty a, Faculty b)
        {
            return a.Students.Count != b.Students.Count;
        }
    }

    static class InOutUtils
    {
        public static Faculty ReadFaculty(string FileName)
        {
            using (StreamReader reader = new StreamReader(FileName))
            {
                string[] firstLine = reader.ReadLine().Split(',');
                string facultyName = firstLine[0].Trim();
                int creditLimit = int.Parse(firstLine[1].Trim());
                List<Student> students = new List<Student>();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    string surname = parts[0].Trim();
                    string name = parts[1].Trim();
                    string group = parts[2].Trim();
                    List<int> credists = new List<int>();
                    for (int i = 3; i < parts.Length; i++)
                    {
                        credists.Add(int.Parse(parts[i].Trim()));
                    }
                    students.Add(new Student(name, surname, group, credists));
                }

                return new Faculty(facultyName, creditLimit, students);
            }
        }

        public static void PrintFaculty(Faculty faculty, string fileName, string header)
        {
            using (StreamWriter writer = File.AppendText(fileName))
            {
                writer.WriteLine(new string('-', 63));
                writer.WriteLine("| {0, -59} |", header);
                writer.WriteLine(new string('-', 63));
                if (faculty.Students.Count == 0)
                {
                    writer.WriteLine("| {0, -59} |", "Nėra");
                } else
                {
                    writer.WriteLine("| {0, -15} | {1, -15} | {2, -8} | {3, 10} |", "Pavardė", "Vardas", "Grupė", "Kreditų suma");
                    writer.WriteLine(new string('-', 63));
                    foreach (var s in faculty.Students)
                    {
                        writer.WriteLine("| {0, -15} | {1, -15} | {2, -8} | {3, 12} |", s.Surname, s.Name, s.Group, s.Sum(0));
                    }
                }
                writer.WriteLine(new string('-', 63));
                writer.WriteLine();
            }
        }
    }

    static class TaskUtils
    {
        public static Faculty Select(Faculty faculty)
        {
            List<Student> FilteredStudents = new List<Student>();
            foreach (var student in faculty.Students)
            {
                if (student.Sum(0) > faculty.CreditLimit)
                {
                    FilteredStudents.Add(student);
                }
            }
            return new Faculty(faculty.Name, faculty.CreditLimit, FilteredStudents);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string outputFilename = "Rezultatai.txt";
            var faculty1 = InOutUtils.ReadFaculty("IF.csv");
            var faculty2 = InOutUtils.ReadFaculty("EL.csv");

            faculty1.Sort();
            faculty2.Sort();

            var overLimitStudents1 = TaskUtils.Select(faculty1);
            var overLimitStudents2 = TaskUtils.Select(faculty2);

            overLimitStudents1.Sort();
            overLimitStudents2.Sort();

            if (File.Exists(outputFilename))
            {
                File.Delete(outputFilename);
            }

            InOutUtils.PrintFaculty(faculty1, outputFilename, faculty1.Name);
            InOutUtils.PrintFaculty(faculty2, outputFilename, faculty2.Name);

            using (StreamWriter writer = File.AppendText(outputFilename))
            {
                if (overLimitStudents1 > overLimitStudents2)
                {
                    writer.WriteLine("'{0}' fakultetas turi daugiau viršijančių kreditų limitą studentų kiekį.", overLimitStudents1.Name);
                }
                else if (overLimitStudents1 < overLimitStudents2)
                {
                    writer.WriteLine("'{0}' fakultetas turi daugiau viršijančių kreditų limitą studentų kiekį.", overLimitStudents2.Name);
                }
                else
                {
                    writer.WriteLine("Abu fakultetai turi po lygiai viršijančių kreditų limitą studentų kiekį.");
                }
                writer.WriteLine();
            }

            InOutUtils.PrintFaculty(overLimitStudents1, outputFilename, String.Format("Studentai kurie viršija limitą iš '{0}'", faculty1.Name));
            InOutUtils.PrintFaculty(overLimitStudents2, outputFilename, String.Format("Studentai kurie viršija limitą iš '{0}'", faculty2.Name));
        }
    }
}
