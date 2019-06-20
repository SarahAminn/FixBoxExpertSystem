
create table Companys(
Company_Id int IDENTITY not null primary key,
Company_name varchar(max),
);

create table Printers(
printer_Id int Identity not null primary key,
printer_Type varchar(max) not null,
printer_Company int not null foreign key references Companys(Company_Id),
printer_model varchar(max) not null,
printer_Image image not null,
printer_Desc varchar(max) not null,
printers_IType varchar(max) not null
);

create table QuickSetupPrinters(
QSP_Id int Identity not null primary key,
QSP_QSetup image not null,
QSP_Company int not null foreign key references Companys(Company_Id),
QSP_Order int not null,
QSP_IType varchar(max) not null,
);

create table PrintersErrNMsg (
PENM_Id int IDENTITY primary key not null,
PENM_CodeOrMsg varchar(max) not null,
PENM_Solution image not null,
PENM_Company int not null foreign key references Companys(Company_Id),
PENM_IType varchar(max) not null
);

create table QuestionsPrinters (
QPrinters_Id int IDENTITY not null primary key,
QPrinters_Question varchar(max) not null,
QPrinters_Solution image not null,
QPrinters_Type varchar(max) not null,
QPrinters_Order int not null,
QPrinters_IType varchar(max) not null,
QPrinters_QType varchar(max) not null
);

