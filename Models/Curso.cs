using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ABMCURSOSALBARRACIN.Models;

public partial class Curso
{
    public int IdCurso { get; set; }

    [DisplayName("Curso")]
    public string NombreCurso { get; set; } = null!;

    public string Profesor { get; set; } = null!;

    [DisplayName("N° Alumnos")]
    [Range(0, 50, ErrorMessage = "La cantidad de alumnos debe ser entre 1 y 50.")]
    public int CantidadAlumnos { get; set; }

    [DisplayName("Día")]
    public string Dia { get; set; } = null!;

    public TimeSpan Horario { get; set; }

    public enum DiaSemana
    {
        Lunes,
        Martes,
        Miercoles,
        Jueves,
        Viernes
    }
}
