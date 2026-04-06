using DomainEntities.DBEntities;
using Repositories.IRepositoryFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RepositoryFolder
{
    public class StudentSubjectRepository :Repository<StudentSubject>,IStudentSubjectRepository
    {
        public StudentSubjectRepository() { }
        public bool isThereAnyStudentInSubject(int subjectId)
        {
            var checkIfStudentExistInSubject = _context.StudentSubjects.Where(x => x.SubjectId == subjectId).Any();
            return checkIfStudentExistInSubject;

        }
    }
}
