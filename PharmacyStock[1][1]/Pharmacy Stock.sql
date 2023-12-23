use PharmacyStock
create table employees(
Name nvarchar(50)  not null,
NationalID char(14)  primary key not null,
phone char(11)  not null,
Email nvarchar(50) not null,
DateOfBirth Date not null,
Address nvarchar(200) not null,
position nvarchar(20) not null,
Password nvarchar(MAX) not null  ,
image nvarchar(MAX)
)
create table Products(
code int primary key,
Name nvarchar(50) not null,
Price float not null,
Quantity int  not null,
)
create table Suppliers(
ID int primary key,
Name nvarchar(50) not null,
Addres nvarchar(200) null,
Phone char(11) unique not null,
Email nvarchar(50)
)
create table PurchasesBill(
ID int primary key identity(1,1),
SupplierID int not null,
TransactionDate Date not null,
ContactPerson nvarchar(50) not null,
ContactPersonPhone char(11) null,
EmployeeNationalID char(14),
TotalPriceBeforeDiscount float,
Discount float,
TotalPriceAfterDiscount float,
foreign key (EmployeeNationalID) references employees(NationalID),
foreign key (SupplierID) references Suppliers(ID)
)
create table purchesBillDetails(
Productcode int not null,
ProductName nvarchar(50),
PurchesBillID int not null,
ProductPrice float not null,
Quantity int not null,
TotalPrice float not null,
ID int primary key  identity(1,1),
foreign key (Productcode) references Products(code),
foreign key (PurchesBillID) references PurchasesBill(ID)
)
create table SalesBill(
ID int not null primary key identity(1,1),
SaleDate Date not null,
TatalPriceBeforeDiscount float not null,
discount float not null,
TotalPriceAfterDiscount float not null,
EmployeeNationalID char(14),
foreign key (EmployeeNationalID) references employees(NationalID)
)
create table SaleBillDetails(
ProductCode int not null,
ProductName nvarchar(50),
SaleBillID int not null,
ProductPrice float not null,
Quantity int not null,
TotalPrice float not null,
ID int not null primary key identity(1,1),
foreign key (ProductCode) references Products(code),
foreign key (SaleBillID) references SalesBill(ID)
)
create table Emp_Sup(
SuplierID int ,
EmployeeID char(14),
TransactionDate Date not null,
TransactionID int ,
foreign key (EmployeeID) references employees(NationalID),
foreign key (SuplierID) references Suppliers(ID),
foreign key (TransactionID) references PurchasesBill(ID),
primary key(SuplierID,EmployeeID,TransactionID)
)
delete from SalesBill