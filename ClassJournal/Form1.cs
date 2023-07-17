using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ClassJournal
{
    public partial class MainForm : Form
    {
        private List<Student> students;
        public string xmlFilePath = "students.xml";

        public MainForm()
        {
            InitializeComponent();
            LoadStudents();
        }

        public void LoadStudents()
        {
            if (File.Exists(xmlFilePath))
            {
               XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                using (FileStream fileStream = new FileStream(xmlFilePath, FileMode.Open))
                {
                    students = (List<Student>)serializer.Deserialize(fileStream);
                }
                UpdateListBox();
            }
            else
            {
                students = new List<Student>();
            }
        }

        private void UpdateListBox()
        {
            listBoxStudents.DataSource = null;
            listBoxStudents.DataSource = students.OrderBy(s => s.Name).ToList();
            listBoxStudents.DisplayMember = "Name";
        }

       

        

        private void SaveStudentsToXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
            using (FileStream fileStream = new FileStream(xmlFilePath, FileMode.Create))
            {
                serializer.Serialize(fileStream, students);
            }
        }

        private void buttonAdd_Click_1(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            DateTime birthDate = dateTimePickerBirthDate.Value;

            Student student = new Student
            {
                Name = name,
                BirthDate = birthDate
            };

            students.Add(student);
            UpdateStudentNumbers();
            UpdateListBox();
            SaveStudentsToXml();
        }

        private void buttonRemove_Click_1(object sender, EventArgs e)
        {
            if (listBoxStudents.SelectedItem != null)
            {
                Student selectedStudent = (Student)listBoxStudents.SelectedItem;
                students.Remove(selectedStudent);
                UpdateStudentNumbers();
                UpdateListBox();
                SaveStudentsToXml();
            }
        }
        private void UpdateStudentNumbers()
        {
            students = students.OrderBy(s => s.Name).ToList();

            for (int i = 0; i < students.Count; i++)
            {
                students[i].Number = i + 1;
            }
        }
     

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

      


    }
    [Serializable]
    public class Student
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
