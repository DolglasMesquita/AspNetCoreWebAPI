using SmartSchool.Helpers;
using SmartSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.Data
{
    public interface IRepository
    {
        public void Add<T>(T entity) where T : class;
        public void Update<T>(T entity) where T : class;
        public void Delete<T>(T entity) where T : class;
        public bool SaveChanges();

        Aluno[] GetAllAlunos(bool includeProfessor);
        Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false);
        Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor);
        Aluno GetAlunoById(int alunoId, bool includeProfessor);
        Professor[] GetAllProfessores(bool incluirAlunos);
        Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool incluirAlunos);
        Professor GetProfessorById(int professorId, bool incluirAluno);
    }
}
