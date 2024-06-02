Create ABMCURSOS;

Use ABMCURSOS;

CREATE TABLE Curso (
    IdCurso INT PRIMARY KEY IDENTITY,
    NombreCurso NVARCHAR(100) NOT NULL,
    Profesor NVARCHAR(100) NOT NULL,
    CantidadAlumnos INT NOT NULL DEFAULT 0,
	Dia NVARCHAR(10) NOT NULL,
	Horario Time NOT NULL,
);

INSERT INTO Curso (NombreCurso, Profesor, CantidadAlumnos, Dia, Horario)
VALUES 
('Programacion V', 'Prof. Senatori', 5, 'Jueves', '18:00'),
('Ing. Software', 'Prof. Lamanna', 8, 'Martes', '18:30'),
('Teología', 'Prof. Sibilia', 28, 'Martes', '19:00'),
('Base de Datos', 'Prof. Graciela', 49, 'Martes', '1SS9:30'),
('Pract. Profesional', 'Prof. Fernández', 33, 'Lunes', '20:00');

select * from Curso;SS