C# MySQL database class
============================

A database class for C# with the MySQL database engine.

## To use the class
#### 1. Edit the 'connectionstring' in the Settings.settings file which is located in Properties folder.
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
db.bind("id", "1");
db.query("SELECT * FROM Persons WHERE firstname = @firstname AND id = @id");

// 2. Bind more parameters
db.bind(new string[] { "id", "67" });
string[] saPerson = db.row("SELECT * FROM persons WHERE id = @id");

// 3. Or just give the parameters to the method
string[] saPerson = db.row("SELECT * FROM persons WHERE id = @id", new string[] { "id", "67" });
```

#### Fetching Row:
This method always returns only 1 row ( string array)
```php
<?php
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
            if (deleted > 0) { 
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
It uses the database class I've created to execute the SQL queries.

## How to use SimpleORM
#### 1. First, create a new class.
#### 2. Extend your class to the base class SimpleORM and add the following fields to the class.
#### Example class :
```php
<?php
require_once("easyCRUD.class.php");
 
class YourClass  Extends Crud {
 
  # The table you want to perform the database actions on
  protected $table = 'persons';

  # Primary Key of the table
  protected $pk  = 'id';
  
}
```

## EasyCRUD in action.

#### Creating a new person
```php
<?php
// First we"ll have create the instance of the class
$person = new person();
 
// Create new person
$person->Firstname  = "Kona";
$person->Age        = "20";
$person->Sex        = "F";
$created            = $person->Create();
 
//  Or give the bindings to the constructor
$person  = new person(array("Firstname"=>"Kona","age"=>"20","sex"=>"F"));
$created = person->Create();
 
// SQL Equivalent
"INSERT INTO persons (Firstname,Age,Sex) VALUES ('Kona','20','F')"
```
#### Deleting a person
```php
<?php
// Delete person
$person->Id  = "17";
$deleted     = $person->Delete();
 
// Shorthand method, give id as parameter
$deleted     = $person->Delete(17);
 
// SQL Equivalent
"DELETE FROM persons WHERE Id = 17 LIMIT 1"
```
#### Saving person's data
```php
<?php
// Update personal data
$person->Firstname = "John";
$person->Age  = "20";
$person->Sex = "F";
$person->Id  = "4"; 
// Returns affected rows
$saved = $person->Save();
 
//  Or give the bindings to the constructor
$person = new person(array("Firstname"=>"John","age"=>"20","sex"=>"F","Id"=>"4"));
$saved = $person->Save();
 
// SQL Equivalent
"UPDATE persons SET Firstname = 'John',Age = 20, Sex = 'F' WHERE Id= 4"
```
#### Finding a person
```php
<?php
// Find person
$person->Id = "1";
$person->Find();

echo $person->firstname;
// Johny
 
// Shorthand method, give id as parameter
$person->Find(1); 
 
// SQL Equivalent
"SELECT * FROM persons WHERE Id = 1"
```
#### Getting all the persons
```php
<?php
// Finding all person
$persons = $person->all(); 
 
// SQL Equivalent
"SELECT * FROM persons 
```
