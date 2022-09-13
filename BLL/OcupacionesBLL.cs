using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using GestionPrestamos2022.DAL;
using GestionPrestamos2022.Models;

namespace GestionPrestamos2022.BLL
{
    public class OcupacionesBLL{
        private Contexto _contexto;

        public OcupacionesBLL(Contexto contexto){
            _contexto = contexto;
        }
        public bool Existe(int ocupacionId){
            return _contexto.Ocupaciones.Any( o => o.OcupacionId == ocupacionId);
        }
        private bool Insertar(Ocupaciones ocupacion){
            _contexto.Ocupaciones.Add(ocupacion);
            return _contexto.SaveChanges()>0;
        }
    private bool Modificar(Ocupaciones ocupacion){
        _contexto.Entry(ocupacion).State =  EntityState.Modified;
        return _contexto.SaveChanges()>0;

    }
    public bool Guardar(Ocupaciones ocupacion){
        if(!Existe(ocupacion.OcupacionId))
        return this.Insertar(ocupacion);
        else
        return this.Modificar(ocupacion);
    }
    public bool Eliminar(Ocupaciones ocupacion){
        _contexto.Entry(ocupacion).State = EntityState.Deleted;
        return _contexto.SaveChanges()>0;
    }
    public Ocupaciones? Buscar(int ocupacionId){
        return _contexto.Ocupaciones
        .Where(o => o.OcupacionId == ocupacionId)
        .AsNoTracking()
        .SingleOrDefault(); 
    }
    public List<Ocupaciones> GetList(Expression<Func<Ocupaciones, bool>> Criterio){
            return _contexto.Ocupaciones
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
    }

 }

}