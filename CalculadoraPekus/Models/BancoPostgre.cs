using System;
using System.Data;
using Npgsql;

namespace CalculadoraPekus.Models
{
    public class BancoPostgres
    {
        private NpgsqlConnection ConexaoBanco = new NpgsqlConnection();
        /// <summary>
        /// Procedure interna de abertura de conexao com banco de dados
        /// </summary>
        private void AbreBanco()
        {
            try
            {
                //ConexaoBanco = new NpgsqlConnection("Server=localhost;Port=5432;Database=db_telemetriamonitora"
                //                                        + ";User Id=postgres;Password=19752002;");
                ConexaoBanco = new NpgsqlConnection("Server=localhost"
                + ";Port=5432;Database=db_calculadora"
                + ";User Id=postgres;Password=19752002;");
                ConexaoBanco.Open();
            }
            catch
            { }
        }

        /// <summary>
        /// Procedure interna de fechamendo de conexão com banco
        /// </summary>
        private void FechaBanco()
        {
            try
            {
                ConexaoBanco.Close();
                ConexaoBanco.Dispose();
            }
            catch { }
        }

        /// <summary>
        /// Função de Seleção de Dados e preenchimento em um DataSet
        /// </summary>

        public DataSet SelecionaDadosDataSet(string SQL, string NomeDataSet, string NomeBanco = null)
        {
            DataSet Selecao = new DataSet(NomeDataSet);
            AbreBanco();
            NpgsqlCommand Comando = new NpgsqlCommand(SQL.ToUpper(), ConexaoBanco);
            NpgsqlDataAdapter Adaptador = new NpgsqlDataAdapter(Comando);
            Adaptador.Fill(Selecao, NomeDataSet);
            FechaBanco();
            return Selecao;
        }

        /// <summary>
        /// Função de Seleção de Dados em um DataTable
        /// </summary>

        public DataTable SelecionaDados(string SQL, string NomeBanco = null)
        {
            DataTable Selecao = new DataTable("SELECAO");
            AbreBanco();
            NpgsqlCommand Comando = new NpgsqlCommand(SQL, ConexaoBanco);
            NpgsqlDataAdapter Adaptador = new NpgsqlDataAdapter(Comando);
            Adaptador.Fill(Selecao);
            FechaBanco();
            return Selecao;
        }

        /// <summary>
        /// Função de Seleção de Dados com Passagem de parâmetros no SQL (evitar injeção SQL malicioso)
        /// onde Parâmetros devem ser passados com o separador | (Pipe)
        /// </summary>

        public DataTable SelecionaDados(string SQL, string PARAMETROS, string NomeBanco = null)
        {
            DataTable Selecao = new DataTable("SELECAO");
            try
            {
                AbreBanco();
                NpgsqlCommand Comando = new NpgsqlCommand(SQL.ToUpper(), ConexaoBanco);
                string[] aParametros = PARAMETROS.Split('|');
                foreach (string sParametros in aParametros)
                {
                    if (sParametros != "")
                    {
                        string[] aValor = sParametros.Split(',');
                        Comando.Parameters.Add(new NpgsqlParameter(aValor[0], aValor[1].ToUpper()));
                    }
                }
                NpgsqlDataAdapter Adaptador = new NpgsqlDataAdapter(Comando);
                Adaptador.Fill(Selecao);
            }
            catch { }
            FechaBanco();
            return Selecao;
        }

        /// <summary>
        /// Função geral de Inserção de Dados
        /// </summary>

        public int InsereDados(string SQL, string NomeBanco = null, bool Scalar = false)
        {
            int iRetorno = 0;
            try
            {
                AbreBanco();
                NpgsqlCommand Comando = new NpgsqlCommand(SQL, ConexaoBanco);
                iRetorno = Scalar == false ? Comando.ExecuteNonQuery() : Convert.ToInt32(Comando.ExecuteScalar());
            }
            catch (Exception ex) { }
            FechaBanco();
            return iRetorno;
        }



        /// <summary>
        /// Função geral de Atualização de Dados
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public int AtualizaDados(string SQL, string NomeBanco = null)
        {
            int iRetorno = 0;
            try
            {
                AbreBanco();
                NpgsqlCommand Comando = new NpgsqlCommand(SQL, ConexaoBanco);
                iRetorno = Comando.ExecuteNonQuery();
            }
            catch { }
            FechaBanco();
            return iRetorno;
        }

        /// <summary>
        ///  Função geral de Atualização de Dados
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="PARAMETROS"></param>
        /// <returns></returns>
        public int AtualizaDados(string SQL, string PARAMETROS, string NomeBanco = null)
        {
            int iRetorno = 0;
            try
            {
                AbreBanco();
                NpgsqlCommand Comando = new NpgsqlCommand(SQL.ToUpper(), ConexaoBanco);
                string[] aParametros = PARAMETROS.Split('|');
                foreach (string sParametros in aParametros)
                {
                    if (sParametros != "")
                    {
                        string[] aValor = sParametros.Split(',');
                        if (aValor[1].ToUpper() != "")
                            Comando.Parameters.Add(new NpgsqlParameter(aValor[0], aValor[1].ToUpper()));
                        else
                            Comando.Parameters.Add(new NpgsqlParameter(aValor[0], DBNull.Value));
                    }
                }
                iRetorno = Comando.ExecuteNonQuery();
            }
            catch { }
            FechaBanco();
            return iRetorno;
        }

        /// <summary>
        /// Função geral para Apagar Dados
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public int ApagarDados(string SQL, string NomeBanco = null)
        {
            int iRetorno = 0;
            try
            {
                AbreBanco();
                NpgsqlCommand Comando = new NpgsqlCommand(SQL, ConexaoBanco);
                iRetorno = Comando.ExecuteNonQuery();
            }
            catch { }
            FechaBanco();
            return iRetorno;
        }
    }
}
