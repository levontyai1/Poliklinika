EXEC sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"
EXEC sp_MSforeachtable @command1 = "DROP TABLE ?"

create database Poliklinika

use Poliklinika

CREATE TABLE [Пациенты] (
	[id Пациента] INT NOT NULL IDENTITY UNIQUE,
	[Фамилия] NVARCHAR(255) NOT NULL,
	[Имя] NVARCHAR(255) NOT NULL,
	[Отчество] NVARCHAR(255) NOT NULL,
	[Дата рождения] DATE NOT NULL,
	[Серия Номер Паспорта] NVARCHAR(255) NOT NULL,
	[Адрес] NVARCHAR(255) NOT NULL,
	[id Пола] INT NOT NULL,
	[Телефон] NVARCHAR(255) NOT NULL,
	PRIMARY KEY([id Пациента])
);
GO

CREATE TABLE [Врач] (
	[id Врача] INT NOT NULL IDENTITY UNIQUE,
	[Фамилия] NVARCHAR(255) NOT NULL,
	[Имя] NVARCHAR(255) NOT NULL,
	[Отчество] NVARCHAR(255) NOT NULL,
	[Дата рождения] DATE NOT NULL,
	[id категории] INT NOT NULL,
	PRIMARY KEY([id Врача])
);
GO

CREATE TABLE [Полис Пациента] (
	[id Полиса_Запись] INT NOT NULL IDENTITY UNIQUE,
	[id Компании] INT NOT NULL,
	[Дата выдачи] DATE NOT NULL,
	[Номер полиса] NVARCHAR(255) NOT NULL,
	[id Пациента] INT NOT NULL UNIQUE,
	PRIMARY KEY([id Полиса_Запись])
);
GO

CREATE TABLE [Кампания выдав Полиса] (
	[id Компании] INT NOT NULL IDENTITY UNIQUE,
	[Название] NVARCHAR(255) NOT NULL,
	PRIMARY KEY([id Компании])
);
GO

CREATE TABLE [Пол] (
	[id пола] INT NOT NULL,
	[Наименование] NVARCHAR(255) NOT NULL,
	PRIMARY KEY([id пола])
);
GO

CREATE TABLE [Специализация] (
	[id Специал] INT NOT NULL IDENTITY UNIQUE,
	[Наименование] NVARCHAR(255) NOT NULL,
	[Нормативы] NVARCHAR(255) NOT NULL,
	PRIMARY KEY([id Специал])
);
GO

CREATE TABLE [Отделение] (
	[id Отделения] INT NOT NULL IDENTITY UNIQUE,
	[Наименование] NVARCHAR(255) NOT NULL,
	PRIMARY KEY([id Отделения])
);
GO

CREATE TABLE [Категория врача] (
	[id категории] INT NOT NULL IDENTITY UNIQUE,
	[Наименование] NVARCHAR(255) NOT NULL,
	PRIMARY KEY([id категории])
);
GO

CREATE TABLE [Специализация врача] (
	[id записи] INT NOT NULL IDENTITY UNIQUE,
	[id Врача] INT NOT NULL,
	[id Специал] INT NOT NULL,
	[id Отделения] INT NOT NULL,
	PRIMARY KEY([id записи])
);
GO

CREATE TABLE [Мед. карта пациента] (
	[id Карты] INT NOT NULL IDENTITY UNIQUE,
	[Номер карты] NVARCHAR(255) NOT NULL,
	[id Пациента] INT NOT NULL UNIQUE,
	PRIMARY KEY([id Карты])
);
GO

CREATE TABLE [День недели] (
	[id дня] INT NOT NULL IDENTITY UNIQUE,
	[Наименование] NVARCHAR(255) NOT NULL,
	PRIMARY KEY([id дня])
);
GO

CREATE TABLE [Запись в медкарте] (
	[id Записи] INT NOT NULL IDENTITY UNIQUE,
	[id Карты] INT NOT NULL,
	[id Врача] INT NOT NULL,
	[id Диагноза] INT NOT NULL,
	[Назначения] NVARCHAR(255) NOT NULL,
	PRIMARY KEY([id Записи])
);
GO

CREATE TABLE [Диагноз] (
	[id Диагноза] INT NOT NULL IDENTITY UNIQUE,
	[Код Диагноза] NVARCHAR(255) NOT NULL,
	[Наименование] NVARCHAR(255) NOT NULL,
	PRIMARY KEY([id Диагноза])
);
GO

CREATE TABLE [Время приема] (
	[id Времени] INT NOT NULL IDENTITY UNIQUE,
	[id записи ] INT NOT NULL,
	[Время приема] TIME NOT NULL,
	[id Дня] INT NOT NULL,
	[id Пациента] INT NOT NULL,
	PRIMARY KEY([id Времени])
);
GO

ALTER TABLE [Пациенты]
ADD FOREIGN KEY([id Пола]) REFERENCES [Пол]([id пола])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Мед. карта пациента]
ADD FOREIGN KEY([id Пациента]) REFERENCES [Пациенты]([id Пациента])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Полис Пациента]
ADD FOREIGN KEY([id Пациента]) REFERENCES [Пациенты]([id Пациента])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Врач]
ADD FOREIGN KEY([id категории]) REFERENCES [Категория врача]([id категории])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Специализация врача]
ADD FOREIGN KEY([id Отделения]) REFERENCES [Отделение]([id Отделения])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Специализация врача]
ADD FOREIGN KEY([id Врача]) REFERENCES [Врач]([id Врача])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Специализация врача]
ADD FOREIGN KEY([id Специал]) REFERENCES [Специализация]([id Специал])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Время приема]
ADD FOREIGN KEY([id Дня]) REFERENCES [День недели]([id дня])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Полис Пациента]
ADD FOREIGN KEY([id Компании]) REFERENCES [Кампания выдав Полиса]([id Компании])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Запись в медкарте]
ADD FOREIGN KEY([id Диагноза]) REFERENCES [Диагноз]([id Диагноза])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Запись в медкарте]
ADD FOREIGN KEY([id Карты]) REFERENCES [Мед. карта пациента]([id Карты])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Время приема]
ADD FOREIGN KEY([id записи ]) REFERENCES [Специализация врача]([id записи])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Запись в медкарте]
ADD FOREIGN KEY([id Врача]) REFERENCES [Врач]([id Врача])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Время приема]
ADD FOREIGN KEY([id Пациента]) REFERENCES [Пациенты]([id Пациента])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO