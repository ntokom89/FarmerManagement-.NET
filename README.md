# Farmer Management Website

## Introduction

This readme shows instructions on how to operate the program 
in Visual studio

The program is connected to azure database. However in the zip file is the backpack and script of 
the local database for analysis and if the azure database does not work. The program will work the same 
with the local or azure database (Zeeshan, 2015).

## Connecting to the local database

To connect to a populated local database do the following steps.
1. Open Microsoft SQL Server Management Studio
2.Connect to your local computer's server.
3. Open Object Explorer.
4. Right click on Databases folder and click on Import Data-tier Application
5. Follow the steps specified on the import settings and import from the BACPAC file in the zip folder.
6.complete the import of the database.
7. On visual studio go to server explorer and click on connect to database symbol.
8. Enter the server name which is desktop server name.
9. Select the database called FarmerManagement_DB and click okay
10.Right click on the added database and click on Properties to find connection String 
11. Copy and paste the connection string into the local connection string in the DALCLass.cs file.


## Functions of the program
### User

The user who can be either farmer or employee can login to enter their 
perspective webpages. 
The login details for the employee are :
UserID : emp1000
Password : Ye67

The login details for the Farmer are :
UserID : fam1000
Password : Ye68

### Employee

The employee can view his main page. The employee can either add a farmer 
or view a list of products by each farmer. This is shown by the buttons (Zeeshan, 2015). Click 
on the List of products by farmers button to access the list. In the list page, you can go to add a farmer.
You can also sort the date and product type of the products by clicking on the blue inked labels (Microsoft, 2022).
The list is sorted by productType in accending order by default. Each row has a product ID, image, product name,
product description, product type, date, farmer name, product amount and price of the product (Zeeshan, 2015) (Saini, 2019).
The employee can also filter by name of the farmer by typing in the search bar the name and clicking on the search button.

### To add a farmer
Click on the add Farmer button. There you need to enter the userID, email. farmer name, farmer surname
and password. You need to ensure that the userID starts with 'fam' and then the 4 digits. A example of the 
data input is shown:

UserID : fam1005
Email : johnford@gmail.com
Farmer name: John
Farmer surname : Ford
Password: ford1234

Once entered, there will be a viewBag message to inform you that the 
insertion of farmer into the database is successful. If you type the userID wrong, 
you will be informed to change the userID. This can also occur if the userID typed already
exists in the database


### Farmer

The farmer can view his main page. The farmer can either view the list of products 
added by him/her  and/or add a product. Each row in the list consist of the product ID, image, product name,
product description, product type, date, product amount and price of the product. To acccess the list, click the List of products button(Microsoft, 2022) (Saini, 2019).

### To add a product
Click on the add product button. There you need to enter the product name, product description, product type, product amount, product price and image. A example of the 
data input is shown(Farias, 2011) (Khan, 2019):

product name: Apple
proudct description: Green Apple
product type : A
Product Amount : 4
Product Price : 50
file input: apple.jpg


Once the information is entered, there is a notification that the 
product is successfuly added to the database. There will be a error notified on the page
if the information entered is not valid (getridbug, 2022).
