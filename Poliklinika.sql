create table Поликлиника;
use Поликлиника;
go


CREATE TABLE Patients (
  patient_id INT PRIMARY KEY not null,
  [name] VARCHAR(255) not null,
  age INT not null
  gender VARCHAR(10) not null,
  [address] VARCHAR(255) not null,
  contact_number VARCHAR(20) not null
);

-- Создание таблицы "Doctors"
CREATE TABLE Doctors (
  doctor_id INT PRIMARY KEY not null,
  [name] VARCHAR(255) not null,
  specialization VARCHAR(255) not null,
  department VARCHAR(255) not null,
  contact_number VARCHAR(20) not null
);

-- Создание таблицы "Appointments"
CREATE TABLE Appointments (
  appointment_id INT PRIMARY KEY not null,
  patient_id INT not null FOREIGN KEY (patient_id) REFERENCES Patients(patient_id),
  doctor_id INT not null FOREIGN KEY (doctor_id) REFERENCES Doctors(doctor_id),
  appointment_time DATETIME not null,
);
