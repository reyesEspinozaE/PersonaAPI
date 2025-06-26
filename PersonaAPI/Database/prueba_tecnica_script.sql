-- Database: prueba_tecnica_datasys

-- DROP DATABASE IF EXISTS prueba_tecnica_datasys;

CREATE DATABASE prueba_tecnica_datasys
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Spanish_Spain.1252'
    LC_CTYPE = 'Spanish_Spain.1252'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

-- Table Persona

create table personas (
id_persona serial primary key,
nombre varchar(100),
apellido varchar(100),
fecha_nacimiento timestamp,
email varchar(255),
telefono varchar(25),
direccion varchar(500),
fecha_registro timestamp default current_timestamp,

-- Validación de email
    constraint chk_email_format check (email ~* '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$')
);
