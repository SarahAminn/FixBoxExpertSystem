alter table Companys
add Company_ErrNMsg bit 

alter table Companys
add company_WebSearch varchar(max)

alter table QuestionsPrinters
add QPrinters_ConCh int foreign key references Choices(choice_Id)