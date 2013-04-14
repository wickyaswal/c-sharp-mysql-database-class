C# MySQL database class
============================

A C# database class for the MySQL database engine. 
The SimpleORM class has some methods like the ORM(Eloquent) of the php Laravel framework.

## To use the class

#### 0. Download the ADO.net drivers for MySQL here :
http://dev.mysql.com/downloads/connector/net/

Add the ' MySql.Data.MySqlClient' Namespace.

#### 1. Edit the 'connectionstring' with your database settings in the Settings.settings file which is located in Properties folder.
```
connectionstring:
Server=localhost;Database=testdb;Uid=yourdatabaseusername;Pwd=the password;
```
#### 2. Create the instance 
```
// The instance 
Db db = new Db();
```

## Examples
Below some examples of the basic functions of the database class. I've included a SQL dump so you can easily test the database
class functions. 
#### The persons table 
| id | firstname | lastname | sex | age
|:-----------:|:------------:|:------------:|:------------:|:------------:|
| 1       |        John |     Doe    | M | 19
| 2       |        Bob  |     Black    | M | 41
| 3       |        Zoe  |     Chan    | F | 20
| 4       |        Kona |     Khan    | M | 14
| 5       |        Kader|     Khan    | M | 56

#### Fetching everything from the table
```php
// Select
dgv_persons.DataSource = db.query("select * FROM persons");

// Quickly select a table
dgv_persons.DataSource = db.table("persons"); 
```
#### Fetching with Bindings (ANTI-SQL-INJECTION):
Binding parameters is the best way to prevent SQL injection. The class prepares your SQL query and binds the parameters
afterwards.

There are three different ways to bind parameters.
```php
// 1. Read friendly method  
db.bind("id", "1");
db.query("SELECT * FROM Persons WHERE firstname = @firstname AND id = @id");

// 2. Bind more parameters
db.bind(new string[] { "id", "1", "f", "John" });
string[] saPerson = db.row("SELECT * FROM persons WHERE id = @id AND firstname = @f");

// 3. Or just give the parameters to the method
string[] saPerson = db.row("SELECT * FROM persons WHERE id = @id", new string[] { "id", "1" });
```

#### Fetching Row:
This method always returns only 1 row ( string array)
```php
// Fetch a row
db.bind("id", "1");
string[] sperson = db.row("SELECT * FROM persons WHERE id = @id");
```
##### Result
| id | firstname | lastname | sex | age
|:-----------:|:------------:|:------------:|:------------:|:------------:|
| 1       |        John |     Doe    | M | 19
#### Fetching Single Value:
This method returns only one single value of a record.
```php
<?php
// Fetch one single value
string age = db.single("SELECT firstname FROM persons WHERE id = @id", new string[]{"id", "3"});
```
##### Result
|firstname
|:------------:
| Zoe
#### Fetching Column:
```php
// Select Column
// Returns List<string>
 cb_column.DataSource = db.column("SELECT age FROM persons");
```
##### Result
|firstname | 
|:-----------:
|        John 
|        Bob  
|        Zoe  
|        Kona 
|        Kader
### Delete / Update / Insert
When executing the delete, update, or insert statement by using the query method the affected rows will be returned.
```php
<?php

// Delete
int deleted = db.nQuery("DELETE FROM persons WHERE id = @id", new string[]{"id", "5"});

 // Do something with the data
if (deleted > 0) 
{ 
   MessageBox.Show("Succesfully deleted the person !");
}    

// Update
db.bind(new string[] { "id", "1" ,"name","jinx"});
           
int updated = db.nQuery("UPDATE persons SET Firstname=@name WHERE id = @id");

// Create/Insert
db.bind(new string[] { "f", "test", "l", "test", "s", "F", "a", "33"});

int created = db.nQuery("INSERT INTO `persons` (`Firstname`, `Lastname`, `Sex`, `Age`) VALUES(@f,@l,@s,@a)");
            
```
## Method parameters
Every method which executes a query has the optional parameter called bindings.

SimpleORM
============================
The SimpleORM is a class which you can use to easily execute basic SQL operations like(insert, update, select, delete) on your database. 

It's heavily inspired by the Eloquent class of the Laravel framework. 
It uses the same database class I've created to execute the SQL queries.

## How to use SimpleORM
#### 1. First, create a new class.
#### 2. Extend your class to the base class SimpleORM and set the following fields of the class in the constructor.
#### 3. For using SimpleORM you must specify the fields of the (database)table using getters and setters.
#### Example class :
```php
class Person : SimpleORM
    {
        private int id;
        private string firstname;
        private string lastname;
        private string sex;
        private int age;

        public int _id {
            get { return id;  }
            set { id = value; }
        }

        public string _firstname {
            get {   return firstname;  }
            set {   firstname = value; }
        }
        
        public string _lastname {
            get { return lastname;  }
            set { lastname = value; }
        }

        public string _sex {
            get { return sex;  }
            set { sex = value; }
        }

        public int _age {
            get { return age;  }
            set { age = value; }
        }

        public Person() 
        {
           // The table 
            table_ = "persons";
           // Primary key of the table
           pk_ = "id";
        }
     
    }
```

## SimpleORM in action.

#### Creating a new person
```php
// First we"ll have create the instance of the class
Person person_a = new Person();

// Create new person
person_a._firstname = "Vivek";
person_a._lastname = "Aswal";
person_a._age = 20;
person_a._sex = "M";

int created = person_a.create();
 
// SQL Equivalent
"INSERT INTO persons (Firstname,Lastname,Age,Sex) VALUES ('Vivek','Aswal','20','M')"
```
#### Mass create 
It's also possible to easily create more persons at a time.

```php

// The list with all the persons
List<object> persons = new List<object>();
            
// Person A
person_a._firstname = "Vivek";
person_a._lastname = "Aswal";
person_a._age = 20;
person_a._sex = "M";

// Person B
Person person_b = new Person();
person_b._firstname = "Kader";
person_b._lastname = "Khan";
person_b._age  = 65;
person_b._sex = "M";

// Add persons to List
persons.Add(person_a);
persons.Add(person_b);

// Create these persons
int crea = person_a.create(persons);
```

#### Deleting a person
```php
// Give id as parameter
int del = person_a.delete(2);
 
// SQL Equivalent
"DELETE FROM persons WHERE Id = 2 LIMIT 1"
```
#### Saving person's data
```php
<?php
// Update personal data
person_a._firstname = "GOD";
person_a._age = 20;
person_a._sex = 'F';

// Parameter is the id of the person
// Returns affected rows
int save = person_a.save(1);

// SQL Equivalent
"UPDATE persons SET Firstname = 'GOD',Age = 20, Sex = 'F' WHERE Id= 1"
```
#### Finding a person
```php

// Find person
// You'll have to explicit convert the result
// Parameter is id of the person
person_a = (Person)person_a.find(1);

Console.Write(person_a._firstname);
 
// SQL Equivalent
"SELECT * FROM persons WHERE Id = 1"
```
#### Getting all the persons
```php
<?php
// Finding all person
// Return type is Datatable

dgv_persons.DataSource = person_a.all();

// SQL Equivalent
"SELECT * FROM persons 
```

####  Aggregates methods
```php
 double min_age = person_a.min("age");

 double max_age = person_a.max("age");

 double avg_age = person_a.avg("age");

 double count_age = person_a.count("age");

 double sum_age   = person_a.sum("age");
```

####  To-do list 

- Create test-cases
- Specify return value for mass create method
- Update this file (A)
- Add comments 
- Add more methods :
  - Where()
  - Model relationships 
  - Eager loading
  - Lazy loading
