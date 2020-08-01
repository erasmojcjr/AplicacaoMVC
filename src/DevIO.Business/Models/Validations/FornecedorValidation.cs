using AppMvcBasica.Models;
using DevIO.Business.Models.Validations.Documentos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevIO.Business.Models.Validations
{
    class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("o camo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("o campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(f => f.TipoFornecedor == TipoFornecedor.PessoaFisica, () =>
            {
                RuleFor(f => f.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
                .WithMessage("o Campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(f => CpfValidacao.Validar(f.Documento)).Equal(true)
                .WithMessage("o documento fornecido é invalido.");
            });

            When(f => f.TipoFornecedor == TipoFornecedor.PessoaJuridica, () =>
            {
                RuleFor(f => f.Documento.Length).Equal(CnpjValidacao.TamanhoCnpj)
                .WithMessage("o Campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(f => CnpjValidacao.Validar(f.Documento)).Equal(true)
                .WithMessage("o documento fornecido é invalido.");

            });

        }
    }
}
