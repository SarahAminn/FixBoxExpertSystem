
create table Companys(
Company_Id int IDENTITY not null primary key,
Company_name varchar(max),
Company_ErrNMsg bit not null,
company_WebSearch varchar(max)
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
QSP_Printer int not null foreign key references Printers(printer_Id)
);

create table PrintersErrNMsg (
PENM_Id int IDENTITY primary key not null,
PENM_CodeOrMsg varchar(max) not null,
PENM_Company int not null foreign key references Companys(Company_Id),
PENM_IType varchar(max) not null
);

create table QuestionsPrinters (
QPrinters_Id int IDENTITY not null primary key,
QPrinters_Question varchar(max) not null,
QPrinters_Type varchar(max) not null,
QPrinters_Order int not null,
QPrinters_IType varchar(max) not null,
QPrinters_QType varchar(max) not null
);

create table Choices (
choice_Id int IDENTITY primary key not null,
choice_ch varchar(max) not null,
choice_Question int not null foreign key references QuestionsPrinters(QPrinters_Id)
);

create table ChoiceSolutions (
CHSol_Id int IDENTITY not null primary key,
CHSol_Solution image not null,
CHSol_Order int not null,
CHSol_Company int not null foreign key references Companys(Company_Id),
CHSol_Printer int not null foreign key references Printers(printer_Id),
CHSol_Choice int not null foreign key references Choices(choice_Id)
);

create table ErrNMsSolutions (
ErrSols_Id int IDENTITY primary key not null ,
ErrSols_Solution image not null ,
ErrSols_Order int not null ,
ErrSols_CodeOMsg int Foreign key references PrintersErrNMsg (PENM_Id)

);

create table QuestionsSolutions (
QS_Id int IDENTITY not null primary key ,
QS_Solution image not null ,
QS_Order int not null ,
QS_Question int Foreign key references QuestionsPrinters (QPrinters_Id)

);