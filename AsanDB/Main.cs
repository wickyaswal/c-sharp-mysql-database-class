using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsanDB
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            
            // Create the instance 
            Db db = new Db();
           
            // Select
            dgv_persons.DataSource = db.query("select * FROM persons");

            // Quickly select a table
            dgv_persons.DataSource = db.table("persons"); 
        
            // Select Row
            db.bind("id", "1");
            string[] sperson = db.row("SELECT * FROM persons WHERE id = @id");

            // Select One cell
            string age = db.single("SELECT age FROM persons WHERE id = @id", new string[]{"id", "1"});

            // Select Column
            // Returns List<string>
            cb_column.DataSource = db.column("SELECT age FROM persons");

            // Delete, Update & Insert returns the affected rows

            // Delete
            int deleted = db.nQuery("DELETE FROM persons WHERE id = @id", new string[]{"id", "5"});
                
            // Do something with the data
            if (deleted > 0) { 
                // MessageBox.Show("Succesfully deleted the person !");
            }    

            // Update
            db.bind(new string[] { "id", "1" ,"name","jinx"});
                
            int updated = db.nQuery("UPDATE persons SET Firstname=@name WHERE id = @id");

            // Create/Insert
            db.bind(new string[] { "f", "test", "l", "test", "s", "F", "a", "33", "c","2" });

       //     int created = db.nQuery("INSERT INTO `persons` (`Firstname`, `Lastname`, `Sex`, `Age`, `City_id`) VALUES(@f,@l,@s,@a,@c)");
            



            // SimpleORM 

            Person person_a = new Person();
                
            // Save person details
            person_a._firstname = "GOD";
            person_a._lastname = "GOD";
            person_a._age = 20;

            // Parameter is the id of the person
            int save = person_a.save(1);
          
            // delete
            int del = person_a.delete(26);
           
            // Find / Select person
            person_a = (Person)person_a.find(25);

            // Create a new person
            
            person_a._firstname = "Vivek";
            person_a._lastname = "Aswal";
            person_a._age = 20;
            person_a._sex = "M";

            person_a.create();

            List<object> persons = new List<object>();
            
            Person person_b = new Person();
            person_b._firstname = "Kader";
            person_b._lastname = "Khan";
            person_b._age  = 65;
            person_b._sex = "M";

            persons.Add(person_a);
            persons.Add(person_b);

            int crea = person_a.create(persons);

            // Aggregates methods

            double d = person_a.min("age");

            double a = person_a.max("age");

            double c = person_a.avg("age");

            double e = person_a.count("age");

            Double f = person_a.sum("age");

            // SELECT All
            dgv_persons.DataSource = person_a.all();
           
        }

        public void Write(string s) {

            Console.WriteLine(s + "\n\r");

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
