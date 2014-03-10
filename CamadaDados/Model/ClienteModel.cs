using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;

namespace CamadaDados
{
    public class ClienteModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int idCliente { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        //[MaxLength(11, ErrorMessage = "CPF deve conter 11 digitos")]
        [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}\-?\d{2}$", ErrorMessage = "CPF inválido")]
        [CostumeValidateCPF(ErrorMessage = "CPF Inválido AAAAAAA")]
        public string CPF { get; set; }

       
    }


    public class CostumeValidateCPF : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;

           return CPF.ValidaCPF(value.ToString());
        }
    }

    public static class CPF
    {
        public static bool ValidaCPF(string cpf)
        {

            string valor = cpf.Replace(".", "");
            valor = valor.Replace("-", "");
            
            if (valor.Length != 11)
                return false;
 
            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;
 
            if (igual || valor == "12345678909")
                return false;
 
            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(
                valor[i].ToString());
 
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];
           
            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;
 
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];
 
            resultado = soma % 11;
 
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
 
            }
            else
                if (numeros[10] != 11 - resultado)
                    return false;
            return true;
 
        }
    }
}
