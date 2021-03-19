create database sonja
go
use sonja
go
create table frends
(
	 		name			varchar(40)
	,		adresse         varchar(80)
	,		geburztag       datetime
	,		bemerkung		text
)
go

create table bekanntschaften
(
	 		name			varchar(40)
	,		nachname		varchar(40)
	,		adresse         varchar(80)
	,		geburztag       datetime
	,		bemerkung		text
)
go

create table sonjaInput
(
			input		text
	,		output		text
)
go
create table termine
(
			datum		datetime
	,		termin		text
)
go
create table keydown
(			
			taste		char
	,		combo		varchar			(10)
	,		program		varchar			(40)

)
go
create table kunden
(
		 	name			varchar(40)
	,		adresse         varchar(80)
	,		geburztag       datetime
	,		bemerkung		text
)
go
