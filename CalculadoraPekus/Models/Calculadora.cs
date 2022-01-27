using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Npgsql;
using System.Data;
using System.ComponentModel.DataAnnotations;
using CalculadoraPekus.Models;


namespace CalculadoraPekus.Models
{
    public class Calculadora
    {
        public int idconta { get; set; }
        public float n1 { get; set; }
        public float n2 { get; set; }
        public string Operacao { get; set; }
        public float total { get; set; }
        public DateTime datahora { get; set; }


        public List<Calculadora> Informacoes()
        {
            List<Calculadora> calculos = new List<Calculadora>();
            var sql = "";

            sql = "select * from sc_calculadora.tb_contas";

            BancoPostgres banco = new BancoPostgres();
            DataTable Registros = banco.SelecionaDados(sql);

            if (Registros.Rows.Count != 0)
            {
                for (int Registro = 0; Registro < Registros.Rows.Count; Registro++)
                {
                    calculos.Add(new Calculadora
                    {
                        idconta = Convert.ToInt32(Registros.Rows[Registro]["idcontas"]),
                        n1 = Convert.ToInt32(Registros.Rows[Registro]["valor1"]),
                        n2 = Convert.ToInt32(Registros.Rows[Registro]["valor2"]),
                        Operacao= Convert.ToString(Registros.Rows[Registro]["operacao"]),
                        total =  Convert.ToInt32(Registros.Rows[Registro]["resultado"]),
                        datahora = Convert.ToDateTime(Registros.Rows[Registro]["datetime"]),
                    }
                    );
                }
            }
            return calculos;

        }
    }
}
