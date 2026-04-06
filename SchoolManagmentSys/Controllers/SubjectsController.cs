using DomainEntities.DBEntities;
using Infrastructure.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.IRepositoryFolder;
using Repositories.RepositoryFolder;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IStudentSubjectRepository _studentSubjectRepository;
        public SubjectsController(ISubjectRepository subjectRepository, IStudentSubjectRepository studentSubjectRepository)
        {
            _subjectRepository = subjectRepository;
            _studentSubjectRepository = studentSubjectRepository;
        }
        public IActionResult GetAllSubjects() {

            var subjects = (from obj in _subjectRepository.GetAll()
                            select new SubjectDTO {
                                Id = obj.Id,
                                Name = obj.Name,


                            }).ToList();
            var subjectAsJson = JsonConvert.SerializeObject(subjects, Formatting.None, new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(subjectAsJson);
        }

        public IActionResult Add(SubjectDTO subjectDTO) {
            var subjectExist = _subjectRepository.Find(x => x.Name.ToUpper().Contains(subjectDTO.Name.ToUpper())).Any();
            if (subjectExist)
            {
                return Conflict("Subject Already Exist");

            }
            else
            {
                Subject subject = new Subject()
                {
                    Name = subjectDTO.Name
                };
                _subjectRepository.Add(subject);
                return Ok("Subject added");
            }









        }
       

        public IActionResult FindSubject(int id) {
            var isSubjectExist = _subjectRepository.Find(x => x.Id == id).FirstOrDefault();
            if (isSubjectExist != null)
            {
                return Ok(isSubjectExist);
            }
            else { 
                return NotFound("Subject Not Found");



            }
           



        }


        public IActionResult Edit(SubjectDTO subjectDTO) {
            var checkSub = _subjectRepository.Find(x=>x.Id!=subjectDTO.Id && x.Name.ToUpper().Contains(subjectDTO.Name.ToUpper())).
                Any();
            if (checkSub)
            {
                return Conflict("Subject Already Exist");

            }
            else
            {
                Subject subject = new Subject()
                {
                    Id = subjectDTO.Id,
                    Name = subjectDTO.Name,

                };
                _subjectRepository.Update(subject);
                return Ok("EditedSuccessfully");
            }
        
        
        }

        public IActionResult Delete(int id) {
            var subjects = _subjectRepository.Find(x => x.Id == id).FirstOrDefault();
            if (subjects != null)
            {
                var checkIfStudentExistInSubject = _studentSubjectRepository.isThereAnyStudentInSubject(id);
                if (checkIfStudentExistInSubject) {
                    return BadRequest("Can't Delete Subject Because There Are Students Enrolled In It");

                }
                _subjectRepository.Delete(subjects);
                return Ok("Deleted Success");
            }
            return NotFound("Subject Not Found");




        }


        public IActionResult GetAllUnrolledSubjects(long id) {
            var result = (from obj in _subjectRepository.Find(x => x.Id != 0, substd => substd.StudentSubjects)
                          where !obj.StudentSubjects.Any(ss => ss.StudentId == id)
                          select new SubjectDTO
                          {
                              Id = obj.Id,
                              Name = obj.Name

                          }).ToList();
            DisplayUnrolledSubDTO displayUnrolledSubDTO = new DisplayUnrolledSubDTO();
            displayUnrolledSubDTO.AllUnrolledSubs.AddRange(result);
            displayUnrolledSubDTO.StdId = id;
            return Ok(displayUnrolledSubDTO);
        
        
        
        }




    }
}
