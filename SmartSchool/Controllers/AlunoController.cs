﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Data;
using SmartSchool.Dtos;
using SmartSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repository.GetAllAlunos(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repository.GetAlunoById(id, true);
            if (aluno == null) return BadRequest("Aluno não encontrado");
            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
        }

        [HttpGet("disciplina/{disciplinaId}")]
        public IActionResult GetByDisciplina(int disciplinaId)
        {
            var alunos = _repository.GetAllAlunosByDisciplinaId(disciplinaId, true);
            if (alunos == null) return BadRequest("Aluno não encontrado");
             
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repository.Add(aluno);
            if (_repository.SaveChanges())
            {
                return Created($"/api/aluno/{aluno.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repository.GetAlunoById(id, true);
            if (alu == null) return BadRequest("Aluno não encontrado");

            _repository.Update(aluno);
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repository.GetAlunoById(id, true);
            if (alu == null) return BadRequest("Aluno não encontrado");

            _repository.Update(aluno);
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repository.GetAlunoById(id, true);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _repository.Delete(aluno);
            if (_repository.SaveChanges())
            {
                return Ok("Aluno removido");
            }
            return BadRequest("Aluno não removido");

        }
    }
}
